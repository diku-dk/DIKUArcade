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

}

