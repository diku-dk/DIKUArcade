using System.Diagnostics;

namespace DIKUArcade.Timers {

    /// <summary>
    /// Static timer initialized on engine startup. Can be used for
    /// animations based on static, discrete time intervals.
    /// </summary>
    public class StaticTimer {
        private static Stopwatch timer;
        private static bool paused;

        static StaticTimer() {
            StaticTimer.timer = new Stopwatch();
            StaticTimer.timer.Start();
            StaticTimer.paused = false;
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

        /// <summary>
        /// Get the number of elapsed minutes since application start
        /// </summary>
        /// <returns></returns>
        public static double GetElapsedMinutes() {
            return StaticTimer.timer.ElapsedMilliseconds / 60000.0;
        }

        public static void RestartTimer() {
            StaticTimer.timer.Restart();
        }

        public static void PauseTimer() {
            if (!StaticTimer.paused) {
                StaticTimer.timer.Stop();
                StaticTimer.paused = true;
            }
        }

        public static void ResumeTimer() {
            if (StaticTimer.paused) {
                StaticTimer.timer.Start();
                StaticTimer.paused = false;
            }
        }
    }
}