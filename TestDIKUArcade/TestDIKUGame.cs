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

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            //Console.WriteLine($"TestKeyEvents.KeyHandler({action}, {key})");
            if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Num_1:
                        window.SetClearColor(128, 52, 43, 0);
                        break;
                    case KeyboardKey.Num_2:
                        window.SetClearColor(28, 108, 218, 50);
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

        }


        // static testing method
        public static void MainFunction() {
            var windowArgs = new WindowArgs() {
                Title = "TestKeyEvents"
            };

            var game = new TestDIKUGame(windowArgs);
            game.Run();
        }
    }
}
