namespace DIKUArcade.Graphics;

using System;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

public class Image : IBaseImage {

    public Texture Texture { get; private set; }

    public Image(string imageFile) {
        Texture = new Texture(imageFile);
    }

    public Image(Texture texture) {
        Texture = texture;
    }

    public void Render(WindowContext context, Shape shape) {
        var extent = context.Camera.WindowExtent(shape);
        var position = context.Camera.WindowPosition(shape, extent);
        Texture.Render(
            context,
            (int) MathF.Round(position.X, MidpointRounding.AwayFromZero),
            (int) MathF.Round(position.Y, MidpointRounding.AwayFromZero),
            (int) MathF.Ceiling(extent.X) + 1,
            (int) MathF.Ceiling(extent.Y) + 1
        );
    }
}