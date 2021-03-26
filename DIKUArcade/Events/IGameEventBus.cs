namespace DIKUArcade.Events
{
    /// <summary>
    /// Default implementation of GameEventBus (see below) which uses GameEventType
    /// instead of a generic enum event type.
    /// </summary>
    public interface IGameEventBus
    {
        void Subscribe(GameEventType eventType, IGameEventProcessor gameEventProcessor);
        void Unsubscribe(GameEventType eventType, IGameEventProcessor gameEventProcessor);
        void RegisterEvent(GameEvent gameEvent);
    }
}
