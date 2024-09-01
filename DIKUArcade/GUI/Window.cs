namespace DIKUArcade.GUI;

using System;
using System.Numerics;
using DIKUArcade.Graphics;
using DIKUArcade.Input;

/// <summary>
/// Represents a graphical window in the DIKUArcade game engine.
/// Manages window creation, rendering, and input handling.
/// </summary>
public class Window : IDisposable {
    private readonly KeyTransformer transformer = new KeyTransformer();
    private readonly Lowlevel.Window window;
    private bool isRunning = true;
    private Action<KeyboardAction, KeyboardKey> keyHandler = (k, a) => { };

    /// <summary>
    /// Gets the width of the window.
    /// </summary>
    public int Width { get => window.Width; }

    /// <summary>
    /// Gets the height of the window.
    /// </summary>
    public int Height { get => window.Height; }

    /// <summary>
    /// Gets or sets the camera associated with this window.
    /// </summary>
    public Camera Camera { get; set; }

    /// <summary>
    /// Gets the size of the window as a <see cref="Vector2"/>.
    /// </summary>
    public Vector2 WindowSize {
        get => new Vector2(Width, Height);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/> class with specified arguments.
    /// </summary>
    /// <param name="windowArgs">
    /// The arguments for creating the window, such as title, width, and height.
    /// </param>
    public Window(WindowArgs windowArgs) {
        window = new Lowlevel.Window(windowArgs.Title, windowArgs.Width, windowArgs.Height);
        Camera = new Camera(Width, Height);
    }

    private void InternalKeyHandler(Lowlevel.InternalEvent ev) {
        var classified = Lowlevel.toKeyboardEvent(ev);

        if (classified.IsIgnore) {
            return;
        } else if (classified.IsQuit) {
            CloseWindow();
            return;
        } else if (classified is Lowlevel.ClassifiedEvent<Tuple<Lowlevel.KeyAction, Lowlevel.KeyboardKey>>.React e) {
            var (action, key) = e.Item;
            keyHandler(transformer.TransformAction(action), transformer.TransformKey(key));
            return;
        }

        throw new ArgumentException("Invalid argument provided", nameof(ev));
    }

    /// <summary>
    /// Attaches the specified key event handler method to this window object.
    /// All key inputs will thereafter be directed to this key handler.
    /// </summary>
    /// <param name="keyHandler">
    /// The method to handle key events.
    /// </param>
    internal void SetKeyEventHandler(Action<KeyboardAction, KeyboardKey> keyHandler) {
        this.keyHandler = keyHandler;
    }

    /// <summary>
    /// Checks if the window is still running.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the window is running; otherwise, <c>false</c>.
    /// </returns>
    public bool IsRunning() {
        return isRunning;
    }

    /// <summary>
    /// Polls events from the window and processes them using the internal key handler.
    /// </summary>
    public void PollEvents() {
        window.PollEvents(InternalKeyHandler);
    }

    private void Cleanup() {
        window.Cleanup();
    }

    /// <summary>
    /// Releases the resources used by the <see cref="Window"/> class.
    /// </summary>
    public void Dispose () {
        Cleanup();
    }

    /// <summary>
    /// Renders the contents of the window using the specified renderer.
    /// </summary>
    /// <param name="renderer">
    /// The method used to render the content of the window.
    /// </param>
    public void Render(Action<WindowContext> renderer) {
        window.Render(lowlevel => {
            renderer(new WindowContext(lowlevel, Camera, this));
        });
    }

    /// <summary>
    /// Finalizer for the <see cref="Window"/> class. Ensures resources are cleaned up if Dispose is not called.
    /// </summary>
    ~Window() {
        Cleanup();
    }

    /// <summary>
    /// Closes the window and stops the game loop. Subsequent calls to <see cref="IsRunning"/> will return <c>false</c>.
    /// </summary>
    public void CloseWindow() {
        window.HideWindow();
        isRunning = false;
    }

    /// <summary>
    /// Sets the clear color of the window.
    /// </summary>
    /// <param name="r">
    /// The red component of the color.
    /// </param>
    /// <param name="g">
    /// The green component of the color.
    /// </param>
    /// <param name="b">
    /// The blue component of the color.
    /// </param>
    public void SetClearColor(byte r, byte g, byte b) {
        window.SetClearColor(r, g, b);
    }

    /// <summary>
    /// Clears the window using the current clear color.
    /// </summary>
    public void Clear() {
        window.Clear();
    }
}
