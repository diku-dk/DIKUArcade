namespace DIKUArcade.Events;

using DIKUArcade.Timers;

/// <summary>
/// Represents a GameEvent that is used internally in the GameEventBus to handle when a time event
/// should be executed.
/// </summary>
internal class TimedGameEvent {

    /// <summary>
    /// The GameEvent which this object wraps around.
    /// </summary>
    internal object GameEvent { get; private set; }
    private readonly TimePeriod timeSpan;
    private long timeOfCreation;

    internal TimedGameEvent(object arg, TimePeriod timeSpan) {
        this.timeSpan = timeSpan;
        GameEvent = arg;
        timeOfCreation = StaticTimer.GetElapsedMilliseconds();
    }
    
    /// <summary>
    /// Check if the event is ready for processing from a given time.
    /// </summary>
    /// <param name="currentTimeMs">
    /// Some time in milliseconds
    /// </param>
    /// <returns>
    /// True if the event is ready to be processed else false.
    /// </returns>
    internal bool HasExpired(long currentTimeMs) {
        return (currentTimeMs - timeOfCreation) > timeSpan.ToMilliseconds();
    }

    /// <summary>
    /// Check if the event is ready for processing from the current timestamp.
    /// </summary>
    /// <returns>
    /// True if the event is ready to be processed else false.
    /// </returns>
    internal bool HasExpired() {
        return HasExpired(StaticTimer.GetElapsedMilliseconds());
    }

    /// <summary>
    /// Reset the creation time of the object rather than having to make a new instance.
    /// </summary>
    internal void ResetTime() {
        timeOfCreation = StaticTimer.GetElapsedMilliseconds();
    }
}