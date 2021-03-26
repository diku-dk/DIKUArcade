using DIKUArcade.Timers;

namespace DIKUArcade.Events
{
    /// <summary>
    /// Default implementation of GameEventBus (see below) which uses GameEventType
    /// instead of a generic enum event type.
    /// </summary>
    public interface ITimedGameEventBus
    {
        void RegisterTimedEvent(GameEvent gameEvent, TimePeriod timePeriod);

        /// <summary>
        /// Reset the time period of specified game event.
        /// If event does not already exist in the event bus it is added.
        /// Search is done through the event's Id.
        /// </summary>
        void AddOrResetTimedEvent(GameEvent gameEvent, TimePeriod timePeriod);

        /// <summary>
        /// Cancel the TimedEvent with the given id and remove it from the EventBus.
        /// returns false if event was not contained, and true otherwise.
        /// </summary>
        bool CancelTimedEvent(uint eventId);

        /// <summary>
        /// If event with the given id is contained, reset its time period to the provided TimePeriod.
        /// return false if event was not contained, and true otherwise.
        /// </summary>
        bool ResetTimedEvent(uint eventId, TimePeriod timePeriod);

        /// <summary>
        /// 
        /// </summary>
        bool HasTimedEvent(uint eventId);
    }
}
