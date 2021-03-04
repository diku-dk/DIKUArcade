using DIKUArcade.GUI;
using DIKUArcade.Timers;

namespace DIKUArcade {
    public abstract class DIKUGame {
        protected Window window;
        private GameTimer gameTimer;

        public DIKUGame(WindowArgs windowArgs) {
            window = new Window(windowArgs);
        }

        public abstract void Update();

        public abstract void Render();

        public void Run() {
            System.Console.WriteLine("Game.Run()");
            gameTimer = new GameTimer(30, 30);

            while (window.IsRunning()) {
                gameTimer.MeasureTime();
                window.PollEvents();

                while (gameTimer.ShouldUpdate()) {
                    Update();
                }

                if (gameTimer.ShouldRender()) {
                    window.Clear();
                    Render();
                    window.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {

                }
            }
        }
    }
}
