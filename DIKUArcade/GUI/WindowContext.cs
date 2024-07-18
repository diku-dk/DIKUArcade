using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using DIKUArcade.Graphics;

namespace DIKUArcade.GUI;

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