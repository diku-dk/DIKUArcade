using DIKUArcade.Timers;

namespace DIKUArcade.Events.Generic
{
    /// <summary>
    /// Generic version of the DIKUArcade.Events.TimedGameEvent struct.
    /// Represents a GameEvent together with an expiration time.
    /// When a TimedGameEvent has expired it is ready for processing by a GameEventBus.
    /// </summary>
    /// <typeparam name="EventT">Enumeration type representing type of game events.</typeparam>
    public struct TimedGameEvent<EventT> where EventT : System.Enum
    {
        /// <summary>
        /// The GameEvent<EventT> which this object wraps around.
        /// </summary>
        public GameEvent<EventT> GameEvent { get; private set; }

        private readonly TimePeriod timeSpan;
        private readonly long timeOfCreation;

        public TimedGameEvent(TimePeriod timeSpan, GameEvent<EventT> gameEvent) {
            this.timeSpan = timeSpan;
            GameEvent = gameEvent;

            timeOfCreation = StaticTimer.GetElapsedMilliseconds();
        }

        /// <summary>
        /// Measure time and check if the event is ready for processing.
        /// </summary>
        public bool HasExpired() {
            var now = StaticTimer.GetElapsedMilliseconds();
            return (now - timeOfCreation) > timeSpan.ToMilliseconds();
        }

        /// <summary>
        /// Measure time and check if the event is ready for processing,
        /// but where current timestamp is provided in milliseconds.
        /// This is useful if checking multiple TimedEvents in sequence without
        /// having to get current timestamp for each one.
        /// </summary>
        public bool HasExpired(long currentTime) {
            return (currentTime - timeOfCreation) > timeSpan.ToMilliseconds();
        }
    }
}
