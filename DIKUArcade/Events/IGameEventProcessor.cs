namespace DIKUArcade.Events
{
    /// <summary>
    /// Interface for any class which needs to receive events from an event bus.
    /// </summary>
    public interface IGameEventProcessor
    {
        void ProcessEvent(GameEvent gameEvent);
    }
}
