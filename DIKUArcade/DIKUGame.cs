using System;
using DIKUArcade.GUI;
using DIKUArcade.Timers;

namespace DIKUArcade {
    /// <summary>
    /// Abstract base class for any DIKUArcade game.
    /// </summary>
    public abstract class DIKUGame {
        protected Window window;
        private GameTimer gameTimer;

        /// <summary>
        /// The exact amount of captured updates in the last second.
        /// Can be used for framerate independent calculations.
        /// </summary>
        public static int Timestep { get; private set; }

        public DIKUGame(WindowArgs windowArgs) {
            window = new Window(windowArgs);
        }

        /// <summary>
        /// Override this method to update game logic.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Override this method to render game entities.
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// Enter the game loop and run the game.
        /// This method will never return.
        /// </summary>
        public void Run() {
            System.Console.WriteLine("Game.Run()");
            gameTimer = new GameTimer(30, 30);

            try
            {
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
                        Timestep = gameTimer.CapturedUpdates;
                    }
                }

                window.DestroyWindow();
            }
            catch(Exception ex) {
                Console.WriteLine("DIKUArcade.DIKUGame caught an exception. See message below:" + Environment.NewLine);
                Console.WriteLine(ex);

                Console.WriteLine(Environment.NewLine + "Terminating program...");
                Environment.Exit(1);
            }
        }
    }
}
