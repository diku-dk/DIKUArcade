namespace DIKUArcade.Timers;

public struct TimePeriod {

    // A TimeSpan will internally be represented as an amount of milliseconds.
    private readonly System.Int64 value;

    private TimePeriod(System.Int64 value) {
        this.value = value;
    }

    public static TimePeriod NewMilliseconds(System.Int64 value) {
        if (value < 0) { throw new System.ArgumentOutOfRangeException("value cannot be negative."); }
        return new TimePeriod(value);
    }

    public static TimePeriod NewSeconds(System.Double value) {
        if (value < 0.0) { throw new System.ArgumentOutOfRangeException("value cannot be negative."); }
        return new TimePeriod((System.Int64)System.Math.Floor(value * 1000));
    }

    public static TimePeriod NewMinutes(System.Double value) {
        if (value < 0.0) { throw new System.ArgumentOutOfRangeException("value cannot be negative."); }
        return new TimePeriod((System.Int64)System.Math.Floor(value * 60000));
    }

    public System.Int64 ToMilliseconds() => value;
}
