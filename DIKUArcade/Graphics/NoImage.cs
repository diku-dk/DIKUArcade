namespace DIKUArcade.Graphics;

using DIKUArcade.Entities;
using DIKUArcade.GUI;

/// A stub for an image, to use with entities that are non-drawable
public class NoImage : IBaseImage {
    public NoImage() {}           

    public void Render(WindowContext context, Shape shape) {}
}
