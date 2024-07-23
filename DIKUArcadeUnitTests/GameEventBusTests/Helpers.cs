namespace DIKUArcadeUnitTests.GameEventBusTests;

public struct SomeGameEvent {
    public int Value { get; set; }

    public SomeGameEvent(int value) {
        Value = value;
    }
}

public struct OtherGameEvent {
    public int Value { get; set; }

    public OtherGameEvent(int value) {
        Value = value;
    }
}

public class Listener {
    public int SomeCount { get; set; } = 0;
    public int OtherCount { get; set; } = 0;
    public void SomeListener(SomeGameEvent gameEvent) {
        SomeCount++;
    }
    public void OtherListener(OtherGameEvent gameEvent) {
        OtherCount++;
    }
}