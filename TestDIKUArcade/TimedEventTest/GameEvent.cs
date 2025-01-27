using System;

namespace TestDIKUArcade.TimedEventTest;

public struct GameEvent {
    
    public readonly string Message { get; }
    public GameEvent(string message) {
        Message = message;
    }
}