using System;
using DIKUArcade;
using DIKUArcade.Input;

namespace TestDIKUArcade {
    public class TestKeyEvents
    {
        private static Window window;

        private static void KeyHandler(KeyboardAction action, KeyboardKey key) {
            Console.WriteLine($"TestKeyEvents.KeyHandler({action}, {key})");
            if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Num_1:
                        window.SetClearColor(128, 52, 43);
                        break;
                    case KeyboardKey.Num_2:
                        window.SetClearColor(28, 108, 218);
                        break;
                    case KeyboardKey.Escape:
                        window.CloseWindow();
                        break;
                }
            }
        }

        internal static void MainFunction()
        {
            var windowArgs = new WindowArgs() {
                Title = "TestKeyEvents"
            };
            window = new Window(windowArgs);

            window.SetKeyEventHandler(KeyHandler);

            while(window.IsRunning()) {
                window.PollEvents();
                window.Clear();
                window.SwapBuffers();
            }
        }
    }
}
