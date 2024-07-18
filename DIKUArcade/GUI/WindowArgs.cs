namespace DIKUArcade.GUI;

/// <summary>
/// Arguments for constructing a DIKUArcade.Window object.
/// Use this class to set fundamental properties of the window.
/// </summary>
public struct WindowArgs {
    public WindowArgs() { }

    /* Globalisation settings */

    /* Basic window properties */
    public string Title { get; set; } = "DIKUArcade";
    public int Width { get; set; } = 500;
    public int Height { get; set; } = 500;

    /// <summary>
    /// Specify window aspect ratio. If this value is something else than `WindowAspectRatio.Aspect_Custom`,
    /// then the width of the window will be calculated automatically based on the height.
    /// </summary>
    public WindowAspectRatio AspectRatio { get; set; } = WindowAspectRatio.Aspect_Custom; // Do something with these?

    /* Graphical properties */
    public bool FullScreen { get; set; } = false; // Do something with these?
    public bool Resizable { get; set; } = true; // Do something with these?
}

