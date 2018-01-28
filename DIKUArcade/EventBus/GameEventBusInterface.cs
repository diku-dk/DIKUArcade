using System.Collections.Generic;

namespace DIKUArcade.EventBus
{
    public interface IGameEventBus<T>
    {
        void Subscribe(GameEventType eventType, IGameEventProcessor<T> gameEventProcessor);
        void Unsubscribe(GameEventType eventType, IGameEventProcessor<T> gameEventProcessor);
        void RegisterEvent(GameEvent<T> gameEvent);
        void ProcessEvents(IEnumerable<GameEventType> processOrder);
        void ProcessEvents();
        void BreakProcessing();
        void ResetBreakProcessing();
        void Flush();
    }
}