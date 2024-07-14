using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace DIKUArcade.GUI;

public struct WindowContext {
    private Lowlevel.DrawingContext ctx;
    public readonly Vector2 Size;
    internal WindowContext(Lowlevel.DrawingContext ctx, int width, int height) {
        this.ctx = ctx;
        Size = new Vector2(width, height);
    }

    public Lowlevel.DrawingContext Get() {
        return ctx;
    }
}