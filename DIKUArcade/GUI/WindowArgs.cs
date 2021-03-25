using DIKUArcade.Input;

namespace DIKUArcade.GUI {
    /// <summary>
    /// Arguments for constructing a DIKUArcade.Window object.
    /// Use this class to set fundamental properties of the window.
    /// </summary>
    public class WindowArgs {
        /* Globalisation settings */
        public KeyboardLayout KeyboardLayout { get; set; } = KeyboardLayout.Danish;

        /* Basic window properties */
        public string Title { get; set; } = "DIKUArcade";
        public uint Width { get; set; } = 500U;
        public uint Height { get; set; } = 500U;

        /// <summary>
        /// Specify window aspect ratio. If this value is something else than `WindowAspectRatio.Aspect_Custom`,
        /// then the width of the window will be calculated automatically based on the height.
        /// </summary>
        public WindowAspectRatio AspectRatio { get; set; } = WindowAspectRatio.Aspect_Custom;

        /* Graphical properties */
        public bool FullScreen { get; set; } = false;
        public bool Resizable { get; set; } = true;
    }
}
