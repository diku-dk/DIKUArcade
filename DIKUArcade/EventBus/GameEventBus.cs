using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIKUArcade.EventBus
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

        public void InitializeEventBus(ICollection<GameEventType> eventTypeList)
        {
            _eventProcessors= new Dictionary<GameEventType, ICollection<IGameEventProcessor<T>>>();
            _eventQueues= new Dictionary<GameEventType, GameEventQueue<GameEvent<T>>>();

            if (eventTypeList != null)
                foreach (var eventType in eventTypeList)
                {
                    _eventProcessors.Add(eventType, new List<IGameEventProcessor<T>>());
                    _eventQueues.Add(eventType, new GameEventQueue<GameEvent<T>>());
                }
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

        public void RegisterEvent(GameEvent<T> gameEvent)
        {
            try
            {
                _eventQueues?[gameEvent.EventType].Enqueue(gameEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Could not register event. {e}");
            }
        }

        public void ProcessEvents(IEnumerable<GameEventType> processOrder)
        {
            if(processOrder==default(IEnumerable<GameEventType>))
                throw new ArgumentNullException();

            Parallel.ForEach<GameEventType>(processOrder, new Action<GameEventType, ParallelLoopState>((eventType, loopState) =>
            {
                if (_eventQueues != null)
                {
                    while (!_eventQueues[eventType].IsEmpty())
                    {
                        var currentEvent = _eventQueues[eventType].Dequeue();
                        if (_eventProcessors != null)
                            foreach (var eventProcessor in _eventProcessors[eventType])
                            {
                                eventProcessor.ProcessEvent(eventType, currentEvent);
                                if (_breakExecution)
                                    loopState.Break();
                            }
                    }
                }
            }
                ));

            // semantic of Parallel.ForEach is it blocks until all parallel threads are finished
        }

        public void ProcessEventsSequentially(IEnumerable<GameEventType> processOrder)
        {
            if(processOrder==default(IEnumerable<GameEventType>))
                throw new ArgumentNullException();
            
            foreach(GameEventType eventType in processOrder)
                {
                    if (_eventQueues != null)
                    {
                        while (!_eventQueues[eventType].IsEmpty())
                        {
                            var currentEvent = _eventQueues[eventType].Dequeue();
                            if (_eventProcessors != null)
                                foreach (var eventProcessor in _eventProcessors[eventType])
                                {
                                    eventProcessor.ProcessEvent(eventType, currentEvent);
                                    if (_breakExecution)
                                        return;
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