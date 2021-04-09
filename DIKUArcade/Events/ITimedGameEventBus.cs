using DIKUArcade.Timers;

namespace DIKUArcade.Events
{
    /// <summary>
    /// Interface for an event bus which can handle timed events.
    /// </summary>
    public interface ITimedGameEventBus
    {
        /// <summary>
        /// Register an event within the event bus, which is to be processed only after
        /// the specified TimePeriod has passed.
        /// If the provided GameEvent has its id set, then search the event bus for an
        /// already existing event with that id. If such an event is already contained,
        /// perform no action.
        /// </summary>
        void RegisterTimedEvent(GameEvent gameEvent, TimePeriod timePeriod);

        /// <summary>
        /// Reset the time period of specified game event.
        /// If event does not already exist in the event bus it is added.
        /// Search is done through the event's Id.
        /// </summary>
        void AddOrResetTimedEvent(GameEvent gameEvent, TimePeriod timePeriod);

        /// <summary>
        /// Cancel the TimedEvent with the given id and remove it from the EventBus.
        /// Returns true if such an event was found and removed, and false otherwise.
        /// </summary>
        bool CancelTimedEvent(uint eventId);

        /// <summary>
        /// If event with the given id is contained, reset its time period to the provided TimePeriod.
        /// Returns true if the event exists, and false otherwise.
        /// </summary>
        bool ResetTimedEvent(uint eventId, TimePeriod timePeriod);

        /// <summary>
        /// Searches the event bus for a timed event with the specified id.
        /// Returns true if such an event exists, and false otherwise.
        /// </summary>
        bool HasTimedEvent(uint eventId);
    }
}
