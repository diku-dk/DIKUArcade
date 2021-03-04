using DIKUArcade.Timers;

namespace DIKUArcade.Events {
    /// <summary>
    /// Represents a GameEvent together with an expiration time.
    /// When a TimedGameEvent has expired it is ready for processing by a GameEventBus.
    /// </summary>
    public struct TimedGameEvent<T> {
        public GameEvent<T> GameEvent { get; private set; }

        private readonly TimePeriod timeSpan;
        private readonly long timeOfCreation;

        public TimedGameEvent(TimePeriod timeSpan, GameEvent<T> gameEvent) {
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

        public bool HasExpired(long currentTime) {
            return (currentTime - timeOfCreation) > timeSpan.ToMilliseconds();
        }
    }
}
