namespace TestDIKUArcade.TimedEventTest;
using System;

public struct GameEvent {

    public readonly string Message {
        get;
    }
    public GameEvent(string message) {
        Message = message;
    }
}