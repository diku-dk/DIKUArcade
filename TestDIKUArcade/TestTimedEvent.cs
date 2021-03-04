using System;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;

namespace TestDIKUArcade {
    public class TestTimedEvent : DIKUGame, IGameEventProcessor<object>
    {
        private System.Random random;
        private GameEventBus<object> eventBus;

        public TestTimedEvent(WindowArgs windowArgs) : base(windowArgs)
        {
            window.SetKeyEventHandler(KeyHandler);
            random = new Random();

            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.TimedEvent });

            eventBus.Subscribe(GameEventType.TimedEvent, this);
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Num_1:
                        window.SetClearColor(128, 52, 43, 0);
                        break;
                    case KeyboardKey.Num_2:
                        window.SetClearColor(28, 108, 218, 50);
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
            var e = new GameEvent<object>();
            e.Message = "This is a timed event!";
            e.Id = id;
            e.EventType = GameEventType.TimedEvent;
            eventBus.RegisterTimedEvent(GameEventType.TimedEvent, e, TimePeriod.NewSeconds(1.0));
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
                Title = "TestTimedEvent"
            };

            var game = new TestTimedEvent(windowArgs);
            game.Run();
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType != GameEventType.TimedEvent) {
                Console.WriteLine($"Incorrect type of event ({eventType})");
            }
            Console.WriteLine(gameEvent.Message);
        }
    }
}
