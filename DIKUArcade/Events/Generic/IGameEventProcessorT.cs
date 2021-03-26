namespace DIKUArcade.Events.Generic
{
    /// <summary>
    /// Generic equivalent of the DIKUArcade.Events.IGameEventProcessor interface.
    /// Interface for any class which needs to receive events from an event bus.
    /// </summary>
    /// <typeparam name="EventT">Enumeration type representing type of game events.</typeparam>
    public interface IGameEventProcessor<EventT> where EventT : System.Enum
    {
        void ProcessEvent(GameEvent<EventT> gameEvent);
    }
}
