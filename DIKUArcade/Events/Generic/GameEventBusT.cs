using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIKUArcade.Timers;

namespace DIKUArcade.Events.Generic
{
    /// <summary>
    /// Generic version of the DIKUArcade.Events.GameEventBus class, which uses the generic type EventT
    /// as the underlying event type enum.
    /// GameEventBus is the core module for processing events in the DIKUArcade game engine. Modules can register events and
    /// add them to the queues. Events are distinguished by event types to improve processing performance. Event processor
    /// can register/subscribe themself to receive events of a certain event type. For a single event, all processors are
    /// called with this event (broadcast semantic).
    /// </summary>
    /// <typeparam name="EventT">Enumeration type representing type of game events.</typeparam>
    public class GameEventBus<EventT> : IGameEventBus<EventT>, ITimedGameEventBus<EventT>,
        IGameEventBusController<EventT> where EventT : System.Enum
    {
        private bool _initialized = false;

        /// <summary>
        /// Dictionary of registered event processors for a given game event type.
        /// </summary>
        private Dictionary<EventT, ICollection<IGameEventProcessor<EventT>>> _eventProcessors;
        /// <summary>
        /// Dictionary of game event queues for different game event types.
        /// </summary>
        private Dictionary<EventT, GameEventQueue<GameEvent<EventT>>> _eventQueues;
        /// <summary>
        /// Stops processing the pipeline, e.g. needed due real-time constraints.
        /// </summary>
        private bool _breakExecution = false;

        /// <summary>
        /// List of events which must be processed after a specified time interval has passed.
        /// We use a double-buffered system.
        /// </summary>
        private List<TimedGameEvent<EventT>>[] _timedEventLists;
        private int _activeTimedEventList = 0;
        private int _inactiveTimedEventList = 1;

        private void SwapTimedEventLists() {
            _activeTimedEventList = (_activeTimedEventList + 1) % 2;
            _inactiveTimedEventList = (_inactiveTimedEventList + 1) % 2;
        }


        /// <summary>
        /// Initialized the event bus to handle the specified event types.
        /// An exception is thrown if called on an already initialized GameEventBus.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void InitializeEventBus(ICollection<EventT> eventTypeList)
        {
            if (_initialized) {
                throw new InvalidOperationException("GameEventBus is already initialized!");
            }

            _eventProcessors= new Dictionary<EventT, ICollection<IGameEventProcessor<EventT>>>();
            _eventQueues= new Dictionary<EventT, GameEventQueue<GameEvent<EventT>>>();

            if (eventTypeList != null) {
                foreach (var eventType in eventTypeList)
                {
                    _eventProcessors.Add(eventType, new List<IGameEventProcessor<EventT>>());
                    _eventQueues.Add(eventType, new GameEventQueue<GameEvent<EventT>>());
                }
            }

            _timedEventLists = new List<TimedGameEvent<EventT>>[2] {
                new List<TimedGameEvent<EventT>>(),
                new List<TimedGameEvent<EventT>>()
            };

            _initialized = true;
        }

        public void Subscribe(EventT eventType, IGameEventProcessor<EventT> gameEventProcessor)
        {
            if (gameEventProcessor == default(IGameEventProcessor<EventT>))
                throw new ArgumentNullException("Parameter gameEventProcessor must not be null.");

            try
            {
                _eventProcessors?[eventType].Add(gameEventProcessor);
            }
            catch (Exception e)
            {
                throw new Exception($"Could not subscribe event processor. Check eventType! {e}");
            }
        }

        public void Unsubscribe(EventT eventType, IGameEventProcessor<EventT> gameEventProcessor)
        {
            if (gameEventProcessor == default(IGameEventProcessor<EventT>))
                throw new ArgumentNullException("Parameter gameEventProcessor must not be null.");

            try
            {
                _eventProcessors?[eventType].Remove(gameEventProcessor);
            }
            catch (Exception e)
            {
                throw new Exception($"Could not unsubsribe event processor. Check eventType or processor is unregistered! {e}");
            }
        }


        #region TIMED_EVENTS

        public void RegisterTimedEvent(GameEvent<EventT> gameEvent, TimePeriod timePeriod)
        {
            // do not insert already registered events:
            if (gameEvent.Id != default(uint)) {
                if (_timedEventLists[_activeTimedEventList].Exists(e => e.GameEvent.Id == gameEvent.Id)) {
                    return;
                }
            }
            _timedEventLists[_activeTimedEventList].Add(new TimedGameEvent<EventT>(timePeriod, gameEvent));
        }

        public void AddOrResetTimedEvent(GameEvent<EventT> gameEvent, TimePeriod timePeriod) {
            if (gameEvent.Id != default(uint)) {
                // search for an item which matches the Id of the specified event
                var search = _timedEventLists[_activeTimedEventList].FindIndex(e => e.GameEvent.Id == gameEvent.Id);

                if (search >= 0) {
                    // event with Id already exists, so we reset its time period
                    _timedEventLists[_activeTimedEventList][search] =
                        new TimedGameEvent<EventT>(timePeriod, _timedEventLists[_activeTimedEventList][search].GameEvent);
                    return;
                }
            }
            // input event does not have an Id, or it has an Id but does not exist in list.
            // In either case, we add it.
            _timedEventLists[_activeTimedEventList].Add(new TimedGameEvent<EventT>(timePeriod, gameEvent));
        }

        public bool CancelTimedEvent(uint eventId) {
            bool cancelled = false;
            _timedEventLists[_inactiveTimedEventList].Clear();
            foreach (var e in _timedEventLists[_activeTimedEventList]) {
                if (e.GameEvent.Id != eventId) {
                    _timedEventLists[_inactiveTimedEventList].Add(e);
                } else {
                    cancelled = true;
                }
            }

            // swap the timed-event lists
            SwapTimedEventLists();
            return cancelled;
        }

        public bool ResetTimedEvent(uint eventId, TimePeriod timePeriod) {
            var search = _timedEventLists[_activeTimedEventList].FindIndex(e => e.GameEvent.Id == eventId);
            if (search >= 0) {
                _timedEventLists[_activeTimedEventList][search] =
                    new TimedGameEvent<EventT>(timePeriod, _timedEventLists[_activeTimedEventList][search].GameEvent);
                return true;
            }
            return false;
        }

        public bool HasTimedEvent(uint eventId) {
            return _timedEventLists[_activeTimedEventList].FindIndex(e => e.GameEvent.Id == eventId) >= 0;
        }

        #endregion // TIMED_EVENTS


        public void RegisterEvent(GameEvent<EventT> gameEvent)
        {
            try
            {
                _eventQueues?[gameEvent.EventType].Enqueue(gameEvent);
            }
            catch (Exception e)
            {
                throw new Exception($"Could not register event. Did you Initialize the EventBus with {e.Message}");
            }
        }

        private void ProcessTimedEvents() {
            _timedEventLists[_inactiveTimedEventList].Clear();

            var currentTime = Timers.StaticTimer.GetElapsedMilliseconds();
            foreach (var e in _timedEventLists[_activeTimedEventList]) {
                if (e.HasExpired(currentTime)) {
                    RegisterEvent(e.GameEvent);
                } else {
                    _timedEventLists[_inactiveTimedEventList].Add(e);
                }
            }
            SwapTimedEventLists();
        }

        public void ProcessEvents(IEnumerable<EventT> processOrder) {
            if(processOrder==default(IEnumerable<EventT>)) {
                throw new ArgumentNullException();
            }

            ProcessTimedEvents();

            Parallel.ForEach<EventT>(processOrder, new Action<EventT, ParallelLoopState>(
                (eventType, loopState) => {
                    if (_eventQueues != null) {
                        while (!_eventQueues[eventType].IsEmpty()) {
                            var currentEvent = _eventQueues[eventType].Dequeue();
                            if (currentEvent.To != default(IGameEventProcessor<EventT>))
                            {
                                currentEvent.To.ProcessEvent(currentEvent);
                            }
                            else if (_eventProcessors != null)
                            {
                                foreach (var eventProcessor in _eventProcessors[eventType]) {
                                    eventProcessor.ProcessEvent(currentEvent);
                                    
                                    if (_breakExecution) loopState.Break();
                                }
                            }
                        }
                    }
            }));

            // semantic of Parallel.ForEach is it blocks until all parallel threads are finished
        }

        public void ProcessEventsSequentially(IEnumerable<EventT> processOrder) {
            if(processOrder==default(IEnumerable<EventT>)) {
                throw new ArgumentNullException();
            }

            ProcessTimedEvents();

            foreach(EventT eventType in processOrder) {
                if (_eventQueues != null) {
                    while (!_eventQueues[eventType].IsEmpty()) {
                        var currentEvent = _eventQueues[eventType].Dequeue();
                        if (currentEvent.To != default(IGameEventProcessor<EventT>))
                        {
                            currentEvent.To.ProcessEvent(currentEvent);
                        }
                        else if (_eventProcessors != null) {
                            foreach (var eventProcessor in _eventProcessors[eventType]) {
                                eventProcessor.ProcessEvent(currentEvent);
                                if (_breakExecution) return;
                            }
                        }
                    }
                }
            }
        }

        public void ProcessEvents()
        {
            if (_eventQueues != null) ProcessEvents(_eventQueues.Keys);
        }

        public void ProcessEventsSequentially()
        {
            if (_eventQueues != null) ProcessEventsSequentially(_eventQueues.Keys);
        }

        public void BreakProcessing()
        {
            _breakExecution = true;
        }

        public void ResetBreakProcessing()
        {
            _breakExecution = false;
        }

        public void Flush()
        {
            BreakProcessing();

            if (_eventQueues == null) return;
            foreach (var eventType in _eventQueues.Keys)
            {
                _eventQueues[eventType].Flush();
            }
        }
    }
}
