using DIKUArcade.Timers;

namespace DIKUArcade.Events {
    /// <summary>
    /// Represents a GameEvent together with an expiration time.
    /// When a TimedGameEvent has expired it is ready for processing by a GameEventBus.
    /// </summary>
    public struct TimedGameEvent
    {
        /// <summary>
        /// The GameEvent which this object wraps around.
        /// </summary>
        public GameEvent GameEvent { get; private set; }

        private readonly TimePeriod timeSpan;
        private readonly long timeOfCreation;

        public TimedGameEvent(TimePeriod timeSpan, GameEvent gameEvent) {
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
        public bool HasExpired(long currentTimeMs) {
            return (currentTimeMs - timeOfCreation) > timeSpan.ToMilliseconds();
        }
    }
}
