using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DIKUArcade.Input {
    /// <summary>
    /// Interface for key transformers, ie. classes which transform OpenTK key events
    /// into a DIKUArcade-unified interface. Specializations may support globalization
    /// such as different keyboard layouts.
    /// </summary>
    public interface IKeyTransformer {
        public KeyboardKey TransformKey(Keys key);
    }
}
