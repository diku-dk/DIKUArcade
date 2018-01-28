namespace DIKUArcade.EventBus
{
    public interface IGameEventProcessor<T>
    {
        void ProcessEvent(GameEventType eventType, GameEvent<T> gameEvent);
    }
}