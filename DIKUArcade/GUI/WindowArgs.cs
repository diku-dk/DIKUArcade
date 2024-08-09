namespace DIKUArcade.GUI;

/// <summary>
/// Arguments for constructing a DIKUArcade.Window object.
/// Use this struct to set fundamental properties of the window.
/// </summary>
public struct WindowArgs {
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowArgs"/> struct with default values.
    /// </summary>
    public WindowArgs() { }
    
    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    /// <value>
    /// The title of the window. Defaults to "DIKUArcade".
    /// </value>
    public string Title { get; set; } = "DIKUArcade";

    /// <summary>
    /// Gets or sets the width of the window.
    /// </summary>
    /// <value>
    /// The width of the window in pixels. Defaults to 500.
    /// </value>
    public int Width { get; set; } = 500;

    /// <summary>
    /// Gets or sets the height of the window.
    /// </summary>
    /// <value>
    /// The height of the window in pixels. Defaults to 500.
    /// </value>
    public int Height { get; set; } = 500;

}
