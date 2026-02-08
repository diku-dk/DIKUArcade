namespace DIKUArcade.Timers;

using System;
using System.Diagnostics;
using System.Threading;

/// <summary>
/// A class for scheduling updates and renders in a game.
///
/// Updates are scheduled on a fixed frequency, by always calculating the next time based on the previous.
/// As such, even if the actual update happens a little bit into one period, it doesn't affect the next one.
///
/// If rendering falls more than one frame behind, the missed frames are simply skipped. By contrast,
/// updates will not be skipped, and will be executed as fast as possible until they catch up to the time.
///
/// </summary>
public class GameTimer {
    private long nextUpdate;
    private long nextRender;
    private long nextReset;
    private long updatePeriod; // milliseconds
    private long renderPeriod; // milliseconds

    /// <summary>
    /// Get the last observed UPS count
    /// </summary>
    public int CapturedUpdates {
        get; private set;
    }
    /// <summary>
    /// Get the last observed FPS count
    /// </summary>
    public int CapturedFrames {
        get; private set;
    }

    private int updates;
    private int frames;

    private uint desiredMaxFPS;

    private Stopwatch stopwatch;

    public GameTimer() : this(30, 30) { }

    public GameTimer(uint ups, uint fps = 0) {
        desiredMaxFPS = fps;

        updatePeriod = (long) (1000f / ups);
        renderPeriod = (long) (1000f / fps);

        stopwatch = new Stopwatch();
        stopwatch.Start();

        long elapsed = stopwatch.ElapsedMilliseconds;
        nextUpdate = elapsed + updatePeriod;
        nextRender = elapsed + renderPeriod;
        nextReset = elapsed + 1000; // reset is always once per second

        frames = 0;
        updates = 0;
        CapturedFrames = 0;
        CapturedUpdates = 0;
    }

    public bool ShouldUpdate() {
        var update = stopwatch.ElapsedMilliseconds >= nextUpdate;
        if (update) {
            updates++;
            nextUpdate += updatePeriod;
        }
        return update;
    }

    public bool ShouldRender() {
        if (desiredMaxFPS < 1) {
            return true;
        }
        var now = stopwatch.ElapsedMilliseconds;
        var update = now >= nextRender;
        if (update) {
            frames++;
            while (now >= nextRender) {
                // unlike with game updates, we don't want to rush and render a whole bunch of frames
                // if we're behind, so we set the next render time to next one from the current time
                nextRender += renderPeriod;
            }
        }
        return update;
    }

    /// <summary>
    /// Count the number of updates and frames each second.
    /// This information can be used to update game logic in any way desireable.
    /// </summary>
    public bool ShouldReset() {
        var now = stopwatch.ElapsedMilliseconds;
        var reset = now >= nextReset;
        if (reset) {
            // while (now >= nextRender) {
            //     // skip missed resets, as we want this to run as close to every second as possible
            //     nextRender += 1000;
            // }
            nextReset += 1000;
            CapturedUpdates = updates;
            CapturedFrames = frames;
            updates = 0;
            frames = 0;
        }
        return reset;
    }

    /// <summary>
    /// Sleeps until it's time to perform the next action so the OS can do other things in the meantime.
    /// Might sleep a bit too long (don't we all sometimes), but probably not so much that it's a problem.
    /// The next update times are calculated by the previous time and the period, so the average update
    /// frequency over a period should be accurate.
    /// </summary>
    public void Yield() {
        var nextAction = Math.Min(Math.Min(nextRender, nextUpdate), nextReset);
        // return immediately if we have pending actions
        if (stopwatch.ElapsedMilliseconds >= nextAction) {
            return;
        }

        int timeDelta = (int) (nextAction - stopwatch.ElapsedMilliseconds);
        Thread.Sleep(timeDelta);
    }
}