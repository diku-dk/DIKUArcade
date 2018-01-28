using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIKUArcade.EventBus
{
    public class GameEventBus<T> : IGameEventBus<T>
    {
        private Dictionary<GameEventType, ICollection<IGameEventProcessor<T>>> _eventProcessors;
        private Dictionary<GameEventType, GameEventQueue<GameEvent<T>>> _eventQueues;
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
            if (processOrder == null) return;

            Parallel.ForEach<GameEventType>(processOrder, new Action<GameEventType, ParallelLoopState>((eventType, loopState) =>
            {
                if (_eventQueues != null)
                {
                    var currentEvent = _eventQueues[eventType].Dequeue();
                    if (_eventProcessors != null)
                        foreach (var eventProcessor in _eventProcessors[eventType])
                        {
                            eventProcessor.ProcessEvent(eventType, currentEvent);
                            if(_breakExecution)
                                loopState.Break();
                        }
                }
            }
                ));
        }

        public void ProcessEvents()
        {
            if (_eventQueues != null) ProcessEvents(_eventQueues.Keys);
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