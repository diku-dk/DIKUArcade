using System;

namespace DIKUArcade.Timers {
    public struct TimeSpan {

        // A TimeSpan will internally be represented as an amount of milliseconds.
        private readonly System.Int64 value;

        private TimeSpan(System.Int64 value) {
            this.value = value;
        }

        public static TimeSpan NewMilliseconds(System.Int64 value) {
            if (value < 0) { throw new System.ArgumentOutOfRangeException("value cannot be negative."); }
            return new TimeSpan(value);
        }

        public static TimeSpan NewSeconds(System.Double value) {
            if (value < 0.0) { throw new System.ArgumentOutOfRangeException("value cannot be negative."); }
            return new TimeSpan((System.Int64)System.Math.Floor(value * 1000));
        }

        public static TimeSpan NewMinutes(System.Double value) {
            if (value < 0.0) { throw new System.ArgumentOutOfRangeException("value cannot be negative."); }
            return new TimeSpan((System.Int64)System.Math.Floor(value * 60000));
        }

        public System.Int64 ToMilliseconds() => value;
    }
}
