namespace DIKUArcade.Events.Generic
{
    public interface IGameEventProcessor<EventT> where EventT : System.Enum
    {
        void ProcessEvent(GameEvent<EventT> gameEvent);
    }
}
