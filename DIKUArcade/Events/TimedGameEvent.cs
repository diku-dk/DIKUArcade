namespace DIKUArcade.Events;

using DIKUArcade.Timers;

/// <summary>
/// Represents a timed event used within the GameEventBus. This class wraps a game event and manages
/// its timing, ensuring that the event is only triggered when it has expired based on the specified time period.
/// </summary>
internal class TimedGameEvent {

    /// <summary>
    /// The game event that this object encapsulates and will be triggered when the event expires.
    /// </summary>
    internal object GameEvent { get; private set; }

    private readonly TimePeriod timeSpan;
    private long timeOfCreation;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimedGameEvent"/> class.
    /// </summary>
    /// <param name="arg">
    /// The game event to be triggered.
    /// </param>
    /// <param name="timeSpan">
    /// The duration after which the event should be considered expired.
    /// </param>
    internal TimedGameEvent(object arg, TimePeriod timeSpan) {
        this.timeSpan = timeSpan;
        GameEvent = arg;
        timeOfCreation = StaticTimer.GetElapsedMilliseconds();
    }
    
    /// <summary>
    /// Determines if the event has expired based on the provided current time.
    /// </summary>
    /// <param name="currentTimeMs">
    /// The current time in milliseconds.
    /// </param>
    /// <returns>
    /// True if the event has expired; otherwise, false.
    /// </returns>
    internal bool HasExpired(long currentTimeMs) {
        return (currentTimeMs - timeOfCreation) > timeSpan.ToMilliseconds();
    }

    /// <summary>
    /// Determines if the event has expired based on the current system time.
    /// </summary>
    /// <returns>
    /// True if the event has expired; otherwise, false.
    /// </returns>
    internal bool HasExpired() {
        return HasExpired(StaticTimer.GetElapsedMilliseconds());
    }

    /// <summary>
    /// Resets the creation time of the event to the current system time, effectively restarting its timer.
    /// </summary>
    internal void ResetTime() {
        timeOfCreation = StaticTimer.GetElapsedMilliseconds();
    }
}
