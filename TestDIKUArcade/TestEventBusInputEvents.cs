using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DIKUArcade;
using DIKUArcade.EventBus;

namespace TestDIKUArcade {
    public class TestEventBusInputEvents : IGameEventProcessor<object> {
        private Window win;
        private GameEventBus<object> bus;

        public void GameLoop() {
            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();
                bus.ProcessEvents();
                win.SwapBuffers();
            }
        }

        public TestEventBusInputEvents() {
            win = new Window("TestEventBusInputEvents", 500, AspectRatio.R1X1);
            win.SetClearColor(Color.ForestGreen);

            // just listen to input events for now
            bus = new GameEventBus<object>();
            bus.InitializeEventBus(new List<GameEventType>() {GameEventType.InputEvent});
            bus.Subscribe(GameEventType.InputEvent, this);

            win.RegisterEventBus(bus);
        }

        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            if (type != GameEventType.InputEvent) {
                throw new InvalidEnumArgumentException("type must be a GameEventType.InputEvent!");
            }
            if (gameEvent.Parameter1 == "KEY_RELEASE") {
                return;
            }

            switch (gameEvent.Message) {
            case "KEY_ESCAPE":
                win.CloseWindow();
                break;
            case "KEY_LEFT":
                win.SetClearColor(Color.Crimson);
                break;
            case "KEY_RIGHT":
                win.SetClearColor(Color.ForestGreen);
                break;
            case "KEY_F12":
                Console.WriteLine("F12 pressed");
                win.SaveScreenShot();
                break;
            default:
                break;
            }
        }
    }
}