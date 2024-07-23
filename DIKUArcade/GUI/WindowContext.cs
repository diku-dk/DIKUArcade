namespace DIKUArcade.GUI;

using DIKUArcade.Graphics;

public readonly struct WindowContext {
    internal readonly Lowlevel.DrawingContext LowlevelContext { get; }
    public readonly Camera Camera { get; }
    public readonly Window Window { get; }
    internal WindowContext(Lowlevel.DrawingContext lowlevelContext, Camera camera, Window window) {
        LowlevelContext = lowlevelContext;
        Camera = camera;
        Window = window;
    }
}