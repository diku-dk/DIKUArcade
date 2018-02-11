using System.Diagnostics;

namespace DIKUArcade.Timers {

    /// <summary>
    /// Static timer initialized on engine startup. Can be used for
    /// animations based on static, discrete time intervals.
    /// </summary>
    public class StaticTimer {
        private static Stopwatch timer;

        static StaticTimer() {
            StaticTimer.timer = new Stopwatch();
            StaticTimer.timer.Start();
        }

        /// <summary>
        /// Get the number of elapsed milliseconds since application start
        /// </summary>
        public static long GetElapsedMilliseconds() {
            return StaticTimer.timer.ElapsedMilliseconds;
        }

        /// <summary>
        /// Get the number of elapsed seconds since application start
        /// </summary>
        public static double GetElapsedSeconds() {
            return StaticTimer.timer.ElapsedMilliseconds / 1000.0;
        }
    }
}