namespace DIKUArcade.GUI {
    /// <summary>
    /// Aspect ratio for a DIKUArcade.Window object, defining
    /// width as a function of height.
    /// </summary>
    public enum WindowAspectRatio {
        /// <summary>
        /// Specifies that window dimensions are customly defined,
        /// and do not use any aspect ratio.
        ///</summary>
        Aspect_Custom,

        /// <summary>
        /// Use a quadratic window aspect ratio where width = height.
        ///</summary>
        Aspect_1X1,

        /// <summary>
        /// Use a classic film window aspect ratio where width = 3/2 * height.
        ///</summary>
        Aspect_3X2,

        /// <summary>
        /// Use a standard monitor aspect ratio where width = 4/3 * height.
        ///</summary>
        Aspect_4X3,

        /// <summary>
        /// Use a HD video aspect ratio where width = 16/9 * height.
        ///</summary>
        Aspect_16X9
    }
}
