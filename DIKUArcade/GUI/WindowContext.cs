using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace DIKUArcade.GUI;

public struct WindowContext {
    private Lowlevel.DrawingContext ctx;
    public readonly int Width;
    public readonly int Height;
    internal WindowContext(Lowlevel.DrawingContext ctx, int width, int height) {
        this.ctx = ctx;
        Width = width;
        Height = height;
    }

    public Lowlevel.DrawingContext Get() {
        return ctx;
    }
}