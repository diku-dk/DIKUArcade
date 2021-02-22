using System;

namespace DIKUArcade.Timers {
    public struct TimedEvent {

        public string message { get; set; }
        public string string1 { get; set; }
        public string string2 { get; set; }
        public object object1 { get; set; }

        /// <summary>
        /// Time span that must elapse before this event should be activated,
        /// stored in milliseconds.
        /// </summary>
        private double timeSpanMilliseconds;
        private double timeOfCreation;

        public TimedEvent(TimeSpanType timeSpanType, int timeSpan, string message,
            string parameter1 = "", string parameter2 = "", object object1 = null) {
            this.message = message;
            this.string1 = parameter1;
            this.string2 = parameter2;
            this.object1 = object1;

            timeOfCreation = 0.0;
            timeSpanMilliseconds = (double) timeSpan;
            switch (timeSpanType) {
            case TimeSpanType.Milliseconds:
                break;
            case TimeSpanType.Seconds:
                timeSpanMilliseconds *= 1000.0;
                break;
            case TimeSpanType.Minutes:
                timeSpanMilliseconds *= 60000.0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Reset this object's timer
        /// </summary>
        public void ResetTimer() {
            timeOfCreation = StaticTimer.GetElapsedMilliseconds();
        }

        /// <summary>
        /// Check if the specified time span has elapsed
        /// </summary>
        public bool HasExpired() {
            var nowTime = StaticTimer.GetElapsedMilliseconds();
            return nowTime - timeOfCreation > timeSpanMilliseconds;
        }
    }
}