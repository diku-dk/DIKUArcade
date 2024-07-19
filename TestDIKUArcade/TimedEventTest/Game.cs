namespace TestDIKUArcade.TimedEventTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using System;
using System.Collections.Generic;

public class Game : DIKUGame, IGameEventProcessor {

    private Random random;
    private GameEventBus eventBus;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        random = new Random();

        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.TimedEvent });

        eventBus.Subscribe(GameEventType.TimedEvent, this);
    }

    private void AddTimedEvent(uint id = 1) {
        Console.WriteLine($"AddTimedEvent({id})");
        var e = new GameEvent {
            Message = "This is a timed event!",
            Id = id,
            EventType = GameEventType.TimedEvent
        };
        eventBus.RegisterTimedEvent(e, TimePeriod.NewSeconds(1.0));
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.T:
                AddTimedEvent();
                break;
            case KeyboardKey.Y:
                AddTimedEvent((uint) random.Next());
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render() { }

    public override void Update() {
        eventBus.ProcessEvents();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        var eventType = gameEvent.EventType;
        if (eventType != GameEventType.TimedEvent) {
            Console.WriteLine($"Incorrect type of event ({eventType})");
        }
        Console.WriteLine(gameEvent.Message);
    }
}