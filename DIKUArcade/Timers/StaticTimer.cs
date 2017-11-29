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

        public static double GetCurrentTimeFrame() {
            return StaticTimer.timer.ElapsedMilliseconds;
        }

        public static void ResetTimer() {
            StaticTimer.timer.Restart();
        }
    }
}