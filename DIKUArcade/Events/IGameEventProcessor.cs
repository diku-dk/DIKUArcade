namespace DIKUArcade.Events
{
    public interface IGameEventProcessor<T>
    {
        void ProcessEvent(GameEvent<T> gameEvent);
    }
}
