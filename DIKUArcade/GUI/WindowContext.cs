namespace DIKUArcade.GUI;

using DIKUArcade.Graphics;

/// <summary>
/// Contains the context for rendering in a DIKUArcade window.
/// Provides access to low-level drawing context, camera, and window properties.
/// </summary>
public readonly struct WindowContext {
    /// <summary>
    /// Gets the low-level drawing context used for rendering.
    /// </summary>
    /// <value>
    /// The low-level drawing context.
    /// </value>
    internal readonly Lowlevel.DrawingContext LowlevelContext { get; }

    /// <summary>
    /// Gets the camera associated with this rendering context.
    /// </summary>
    /// <value>
    /// The camera used for rendering.
    /// </value>
    public readonly Camera Camera { get; }

    /// <summary>
    /// Gets the window associated with this rendering context.
    /// </summary>
    /// <value>
    /// The window in which rendering occurs.
    /// </value>
    public readonly Window Window { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowContext"/> struct.
    /// </summary>
    /// <param name="lowlevelContext">The low-level drawing context.</param>
    /// <param name="camera">The camera used for rendering.</param>
    /// <param name="window">The window in which rendering occurs.</param>
    internal WindowContext(Lowlevel.DrawingContext lowlevelContext, Camera camera, Window window) {
        LowlevelContext = lowlevelContext;
        Camera = camera;
        Window = window;
    }
}
