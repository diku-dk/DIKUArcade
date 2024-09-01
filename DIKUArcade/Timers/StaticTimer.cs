namespace DIKUArcade.Timers;

using System.Diagnostics;

/// <summary>
/// Static timer initialized on engine startup. Can be used for
/// animations based on static, discrete time intervals.
/// </summary>
public class StaticTimer {
    private static Stopwatch timer;
    private static bool paused;

    static StaticTimer() {
        timer = new Stopwatch();
        timer.Start();
        paused = false;
    }

    /// <summary>
    /// Get the number of elapsed milliseconds since application start
    /// </summary>
    public static long GetElapsedMilliseconds() {
        return timer.ElapsedMilliseconds;
    }

    /// <summary>
    /// Get the number of elapsed seconds since application start
    /// </summary>
    public static double GetElapsedSeconds() {
        return timer.ElapsedMilliseconds / 1000.0;
    }

    /// <summary>
    /// Get the number of elapsed minutes since application start
    /// </summary>
    /// <returns></returns>
    public static double GetElapsedMinutes() {
        return timer.ElapsedMilliseconds / 60000.0;
    }

    public static void RestartTimer() {
        timer.Restart();
    }

    public static void PauseTimer() {
        if (!paused) {
            timer.Stop();
            paused = true;
        }
    }

    public static void ResumeTimer() {
        if (paused) {
            timer.Start();
            paused = false;
        }
    }
}