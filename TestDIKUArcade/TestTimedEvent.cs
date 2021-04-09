using System;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;

namespace TestDIKUArcade {
    public class TestTimedEvent : DIKUGame, IGameEventProcessor
    {
        private System.Random random;
        private GameEventBus eventBus;

        public TestTimedEvent(WindowArgs windowArgs) : base(windowArgs)
        {
            window.SetKeyEventHandler(KeyHandler);
            random = new Random();

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.TimedEvent });

            eventBus.Subscribe(GameEventType.TimedEvent, this);
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Num_1:
                        window.SetClearColor(128, 52, 43);
                        break;
                    case KeyboardKey.Num_2:
                        window.SetClearColor(28, 108, 218);
                        break;
                    case KeyboardKey.T:
                        AddTimedEvent();
                        break;
                    case KeyboardKey.Y:
                        AddTimedEvent((uint)random.Next());
                        break;
                    case KeyboardKey.Escape:
                        window.CloseWindow();
                        break;
                }
            }
        }

        private void AddTimedEvent(uint id = 1)
        {
            Console.WriteLine($"AddTimedEvent({id})");
            var e = new GameEvent {
                Message = "This is a timed event!",
                Id = id,
                EventType = GameEventType.TimedEvent
            };
            eventBus.RegisterTimedEvent(e, TimePeriod.NewSeconds(1.0));
        }

        public override void Render()
        {

        }

        public override void Update()
        {
            eventBus.ProcessEvents();
        }


        // static testing method
        public static void MainFunction() {
            var windowArgs = new WindowArgs() {
                Title = "TestTimedEvent",
                Resizable = false
            };

            var game = new TestTimedEvent(windowArgs);
            game.Run();
        }

        public void ProcessEvent(GameEvent gameEvent)
        {
            var eventType = gameEvent.EventType;
            if (eventType != GameEventType.TimedEvent) {
                Console.WriteLine($"Incorrect type of event ({eventType})");
            }
            Console.WriteLine(gameEvent.Message);
        }
    }
}
