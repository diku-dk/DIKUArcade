using DIKUArcade.Timers;

namespace DIKUArcade.Events.Generic
{
    /// <summary>
    /// Generic equivalent of the DIKUArcade.Events.ITimedGameEventBus interface.
    /// The timed event types are of the specified generic type.
    /// </summary>
    /// <typeparam name="EventT">Enumeration type representing type of game events.</typeparam>
    public interface ITimedGameEventBus<EventT> where EventT : System.Enum
    {
        void RegisterTimedEvent(GameEvent<EventT> gameEvent, TimePeriod timePeriod);
        void AddOrResetTimedEvent(GameEvent<EventT> gameEvent, TimePeriod timePeriod);
        bool CancelTimedEvent(uint eventId);
        bool ResetTimedEvent(uint eventId, TimePeriod timePeriod);
        bool HasTimedEvent(uint eventId);
    }
}
