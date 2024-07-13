namespace DIKUArcade.GUI;

using System;
using DIKUArcade.Input;


/// <summary>
/// This class represents a graphical window in the DIKUArcade game engine.
/// </summary>
public class Window : IDisposable {
    private KeyTransformer transformer = new KeyTransformer();
    private Lowlevel.Window window;
    private bool isRunning = true;
    private Action<KeyboardAction,KeyboardKey> keyHandler = (k, a) => { };

    public Window(WindowArgs windowArgs) {
        window = new Lowlevel.Window(windowArgs.Title, windowArgs.Width, windowArgs.Height);
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
        GC.SuppressFinalize(this);
    }

    public void Render(Action<WindowContext> renderer) {
        window.Render(lowlevel => renderer(new WindowContext(lowlevel)));
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
}