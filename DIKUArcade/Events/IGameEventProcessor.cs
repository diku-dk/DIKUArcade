namespace DIKUArcade.Events
{
    public interface IGameEventProcessor
    {
        void ProcessEvent(GameEvent gameEvent);
    }
}
