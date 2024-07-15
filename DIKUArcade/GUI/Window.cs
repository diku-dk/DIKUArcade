namespace DIKUArcade.GUI;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using DIKUArcade.Entities;
using DIKUArcade.Input;


/// <summary>
/// This class represents a graphical window in the DIKUArcade game engine.
/// </summary>
public class Window : IDisposable {
    private readonly KeyTransformer transformer = new KeyTransformer();
    private readonly Lowlevel.Window window;
    private bool isRunning = true;
    private Action<KeyboardAction,KeyboardKey> keyHandler = (k, a) => { };
    public int Width { get => window.Width; }
    public int Height { get => window.Height; }
    public Vector2 WindowSize {
        get => new Vector2(Width, Height);
    }
    public WindowContext? WindowContext { get; private set; }

    public Window(WindowArgs windowArgs) {
        window = new Lowlevel.Window(windowArgs.Title, windowArgs.Width, windowArgs.Height);
    }

    public Matrix3x2 Matrix(Vector2 extent) {
        return new Matrix3x2(
            Width, 0.0f,
            0.0f, -Height,
            0.0f, Height - (extent.Y * Height)
        );
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
    /// Attach the specified keyHandler method argument to this window object.
    /// All key inputs will thereafter be directed to this keyHandler.
    /// </summary>
    public void SetKeyEventHandler(Action<KeyboardAction, KeyboardKey> keyHandler) {
        this.keyHandler = keyHandler;
    }

    /// <summary>
    /// Check if the Window is still running.
    /// </summary>
    public bool IsRunning() {
        return isRunning;
    }

    public void PollEvents() {
        window.PollEvents(InternalKeyHandler);
    }

    private void Cleanup() {
        window.Cleanup();
    }

    public void Dispose () {
        Cleanup();
    }

    public void Render(Action renderer) {
        window.Render(lowlevel => {
            WindowContext = new WindowContext(lowlevel, window.Width, window.Height);
            renderer();
            WindowContext = null;
        });
    }

    ~Window() {
        Cleanup();
    }

    /// <summary>
    /// Sets the window running variable to false such that calls to
    /// `IsRunning()` afterwards will return false. This will allow one
    /// to exit the game loop.
    /// </summary>
    public void CloseWindow() {
        window.HideWindow();
        isRunning = false;
    }

    public void SetClearColor(int r, int g, int b) {
        window.SetClearColor(r, g, b);
    }

    public void Clear() {
        window.Clear();
    }

    internal Vector2 MeasureText(string text, Lowlevel.Font font) {
        return window.MeasureText(text, font);
    }
}