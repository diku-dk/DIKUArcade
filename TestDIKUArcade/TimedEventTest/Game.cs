namespace TestDIKUArcade.TimedEventTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using System;
using System.Collections.Generic;

public class Game : DIKUGame {

    private readonly Random random = new Random();
    private readonly GameEventBus eventBus = new GameEventBus();
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        eventBus.Subscribe<GameEvent>(ProcessEvent);
    }

    private void AddTimedEventZero() {
        ulong id = 0;
        var e = new GameEvent("This is a timed event!");
        eventBus.AddOrResetTimedEvent(e, id, TimePeriod.NewSeconds(1.0));
        Console.WriteLine($"AddOrResetTimedEvent({id})");
    }

    private void AddTimedEvent() {
        var e = new GameEvent("This is a timed event!");
        var id = eventBus.RegisterTimedEvent(e, TimePeriod.NewSeconds(1.0));
        Console.WriteLine($"AddTimedEvent({id})");
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.T:
                AddTimedEventZero();
                break;
            case KeyboardKey.Y:
                AddTimedEvent();
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render(WindowContext context) { }

    public override void Update() {
        eventBus.ProcessEvents();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        Console.WriteLine(gameEvent.Message);
    }
}