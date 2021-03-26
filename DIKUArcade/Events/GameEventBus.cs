using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIKUArcade.Timers;

namespace DIKUArcade.Events
{
    /// <summary>
    /// GameEventBus is the core module for processing events in the DIKUArcade game engine. Modules can register events and
    /// add them to the queues. Events are distinguished by event types to improve processing performance. Event processor
    /// can register/subscribe themself to receive events of a certain event type. For a single event, all processors are
    /// called with this event (broadcast semantic).
    /// </summary>
    /// <typeparam name="T">Parameter type of game entities.</typeparam>
    public class GameEventBus<T> : IGameEventBus<T>, IGameEventBusController<T>
    {
        private bool _initialized = false;

        /// <summary>
        /// Dictionary of registered event processors for a given game event type.
        /// </summary>
        private Dictionary<GameEventType, ICollection<IGameEventProcessor<T>>> _eventProcessors;
        /// <summary>
        /// Dictionary of game event queues for different game event types.
        /// </summary>
        private Dictionary<GameEventType, GameEventQueue<GameEvent<T>>> _eventQueues;
        /// <summary>
        /// Stops processing the pipeline, e.g. needed due real-time constraints.
        /// </summary>
        private bool _breakExecution = false;

        /// <summary>
        /// List of events which must be processed after a specified time interval has passed.
        /// We use a double-buffered system.
        /// </summary>
        private int _activeTimedEventList = 0;
        private int _inactiveTimedEventList = 1;
        private List<TimedGameEvent<T>>[] _timedEventLists;

        private void SwapTimedEventLists() {
            _activeTimedEventList = (_activeTimedEventList + 1) % 2;
            _inactiveTimedEventList = (_inactiveTimedEventList + 1) % 2;
        }


        /// <summary>
        /// Initialized the event bus to handle the specified event types.
        /// An exception is thrown if called on an already initialized GameEventBus.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void InitializeEventBus(ICollection<GameEventType> eventTypeList)
        {
            if (_initialized) {
                throw new InvalidOperationException("GameEventBus is already initialized!");
            }

            _eventProcessors= new Dictionary<GameEventType, ICollection<IGameEventProcessor<T>>>();
            _eventQueues= new Dictionary<GameEventType, GameEventQueue<GameEvent<T>>>();

            if (eventTypeList != null) {
                foreach (var eventType in eventTypeList)
                {
                    _eventProcessors.Add(eventType, new List<IGameEventProcessor<T>>());
                    _eventQueues.Add(eventType, new GameEventQueue<GameEvent<T>>());
                }
            }

            _timedEventLists = new List<TimedGameEvent<T>>[2] {
                new List<TimedGameEvent<T>>(),
                new List<TimedGameEvent<T>>()
            };

            _initialized = true;
        }

        public void Subscribe(GameEventType eventType, IGameEventProcessor<T> gameEventProcessor)
        {
            if (gameEventProcessor == default(IGameEventProcessor<T>))
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

        public void Unsubscribe(GameEventType eventType, IGameEventProcessor<T> gameEventProcessor)
        {
            if (gameEventProcessor == default(IGameEventProcessor<T>))
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

        public void RegisterTimedEvent(GameEvent<T> gameEvent, TimePeriod timePeriod)
        {
            // do not insert already registered events:
            if (gameEvent.Id != default(uint)) {
                if (_timedEventLists[_activeTimedEventList].Exists(e => e.GameEvent.Id == gameEvent.Id)) {
                    return;
                }
            }
            _timedEventLists[_activeTimedEventList].Add(new TimedGameEvent<T>(timePeriod, gameEvent));
        }

        /// <summary>
        /// Reset the time period of specified game event.
        /// If event does not already exist in the event bus it is added.
        /// Search is done through the event's Id.
        /// </summary>
        public void AddOrResetTimedEvent(GameEvent<T> gameEvent, TimePeriod timePeriod) {
            if (gameEvent.Id != default(uint)) {
                // search for an item which matches the Id of the specified event
                var search = _timedEventLists[_activeTimedEventList].FindIndex(e => e.GameEvent.Id == gameEvent.Id);

                if (search >= 0) {
                    // event with Id already exists, so we reset its time period
                    _timedEventLists[_activeTimedEventList][search] =
                        new TimedGameEvent<T>(timePeriod, _timedEventLists[_activeTimedEventList][search].GameEvent);
                    return;
                }
            }
            // input event does not have an Id, or it has an Id but does not exist in list.
            // In either case, we add it.
            _timedEventLists[_activeTimedEventList].Add(new TimedGameEvent<T>(timePeriod, gameEvent));
        }

        /// <summary>
        /// Cancel the TimedEvent with the given id and remove it from the EventBus.
        /// returns false if event was not contained, and true otherwise.
        /// </summary>
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

        /// <summary>
        /// If event with the given id is contained, reset its time period to the provided TimePeriod.
        /// return false if event was not contained, and true otherwise.
        /// </summary>
        public bool ResetTimedEvent(uint eventId, TimePeriod timePeriod) {
            var search = _timedEventLists[_activeTimedEventList].FindIndex(e => e.GameEvent.Id == eventId);
            if (search >= 0) {
                _timedEventLists[_activeTimedEventList][search] =
                    new TimedGameEvent<T>(timePeriod, _timedEventLists[_activeTimedEventList][search].GameEvent);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasTimedEvent(uint eventId) {
            return _timedEventLists[_activeTimedEventList].FindIndex(e => e.GameEvent.Id == eventId) >= 0;
        }

        #endregion // TIMED_EVENTS


        public void RegisterEvent(GameEvent<T> gameEvent)
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

        public void ProcessEvents(IEnumerable<GameEventType> processOrder) {
            if(processOrder==default(IEnumerable<GameEventType>)) {
                throw new ArgumentNullException();
            }

            ProcessTimedEvents();

            Parallel.ForEach<GameEventType>(processOrder, new Action<GameEventType, ParallelLoopState>(
                (eventType, loopState) => {
                    if (_eventQueues != null) {
                        while (!_eventQueues[eventType].IsEmpty()) {
                            var currentEvent = _eventQueues[eventType].Dequeue();
                            if (currentEvent.To != default(IGameEventProcessor<T>))
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

        public void ProcessEventsSequentially(IEnumerable<GameEventType> processOrder) {
            if(processOrder==default(IEnumerable<GameEventType>)) {
                throw new ArgumentNullException();
            }

            foreach(GameEventType eventType in processOrder) {
                if (_eventQueues != null) {
                    while (!_eventQueues[eventType].IsEmpty()) {
                        var currentEvent = _eventQueues[eventType].Dequeue();
                        if (_eventProcessors == null) continue;

                        foreach (var eventProcessor in _eventProcessors[eventType]) {
                            eventProcessor.ProcessEvent(currentEvent);
                            if (_breakExecution) return;
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
