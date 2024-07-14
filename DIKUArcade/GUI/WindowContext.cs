using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace DIKUArcade.GUI;

public struct WindowContext {
    private Lowlevel.DrawingContext ctx;
    internal WindowContext(Lowlevel.DrawingContext ctx) {
        this.ctx = ctx;
    }

    public Lowlevel.DrawingContext Get() {
        return ctx;
    }
}