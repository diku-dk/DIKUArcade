using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;

namespace TestDIKUArcade {
    public class TestDIKUGame : DIKUGame
    {
        public TestDIKUGame(WindowArgs windowArgs) : base(windowArgs)
        {
            window.SetKeyEventHandler(KeyHandler);
        }

        private bool f1Pressed = false;

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            //Console.WriteLine($"TestKeyEvents.KeyHandler({action}, {key})");
            if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Num_1:
                        window.SetClearColor(128, 52, 43);
                        break;
                    case KeyboardKey.Num_2:
                        window.SetClearColor(28, 108, 218);
                        break;
                    case KeyboardKey.F1:
                        f1Pressed = true;
                        break;
                    case KeyboardKey.Escape:
                        window.CloseWindow();
                        break;
                }
            }
        }

        public override void Render()
        {

        }

        public override void Update()
        {
            if (f1Pressed) throw new ArithmeticException("You pressed F1.");
        }


        // static testing method
        public static void MainFunction() {
            var windowArgs = new WindowArgs() {
                Title = "TestDIKUGame"
            };

            var game = new TestDIKUGame(windowArgs);
            game.Run();
        }
    }
}
