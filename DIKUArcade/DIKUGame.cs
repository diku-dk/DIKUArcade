namespace DIKUArcade;

using System;
using DIKUArcade.GUI;
using DIKUArcade.Timers;
using DIKUArcade.Input;

/// <summary>
/// Abstract base class for any DIKUArcade game.
/// Provides the basic structure and game loop for running a game.
/// </summary>
public abstract class DIKUGame {
    /// <summary>
    /// The game window used for rendering the game.
    /// </summary>
    protected Window window;

    private GameTimer? gameTimer;

    /// <summary>
    /// The exact amount of captured updates in the last second.
    /// This can be used for frame-rate independent calculations.
    /// </summary>
    public static int Timestep { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DIKUGame"/> class.
    /// Sets up the game window using the provided window arguments.
    /// </summary>
    /// <param name="windowArgs">The arguments specifying the window's configuration.</param>
    public DIKUGame(WindowArgs windowArgs) {
        window = new Window(windowArgs);
        window.SetKeyEventHandler(KeyHandler);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="DIKUGame"/> class.
    /// Disposes of the game window when the game instance is destroyed.
    /// </summary>
    ~DIKUGame() {
        window.Dispose();
    }

    /// <summary>
    /// Override this method to implement the logic that updates the game state.
    /// This method is called at a regular interval defined by the game timer.
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// Override this method to implement the rendering of game entities.
    /// This method is called to draw the game state onto the screen.
    /// </summary>
    /// <param name="context">The context of the window used for rendering.</param>
    public abstract void Render(WindowContext context);

    /// <summary>
    /// Override this method to implement keyboard events.
    /// </summary>
    /// <param name="action">The keyboard action being performed, i.e. press or release.</param>
    /// <param name="key">The keyboard key being performed.</param>
    public abstract void KeyHandler(KeyboardAction action, KeyboardKey key);

    /// <summary>
    /// Starts the game loop and runs the game.
    /// This method will enter an infinite loop, continuously updating and rendering the game.
    /// The method will not return until the game is closed.
    /// </summary>
    public void Run() {
        gameTimer = new GameTimer(30, 30);

        try {
            while (window.IsRunning()) {
                gameTimer.MeasureTime();
                window.PollEvents();

                while (gameTimer.ShouldUpdate()) {
                    Update();
                }

                if (gameTimer.ShouldRender()) {
                    window.Render(Render);
                }

                if (gameTimer.ShouldReset()) {
                    Timestep = gameTimer.CapturedUpdates;
                }
            }
            window.CloseWindow();
        }
        catch(Exception ex) {
            Console.WriteLine("DIKUArcade.DIKUGame caught an exception. See message below:" + Environment.NewLine);
            Console.WriteLine(ex);

            Console.WriteLine(Environment.NewLine + "Terminating program...");
            Environment.Exit(1);
        }

        gameTimer = null;
    }
}
