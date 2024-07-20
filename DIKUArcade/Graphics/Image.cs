namespace DIKUArcade.Graphics;

using System;
using System.Numerics;
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

    public void Render(Shape shape) {
        var window = Window.CurrentFocus();
        if (window is null || window.WindowContext is null)
            throw new Exception("The window context must not be null.");
        
        Render(shape, window.WindowContext.Value);
    }

    public void Render(Shape shape, WindowContext context) {
        var position = context.Camera.WindowPosition(shape);
        var extent = context.Camera.WindowExtent(shape, Texture.originalExtent);
        Texture.Render(
            context,
            (int) MathF.Round(position.X, MidpointRounding.AwayFromZero),
            (int) MathF.Round(position.Y, MidpointRounding.AwayFromZero),
            (int) MathF.Ceiling(extent.X),
            (int) MathF.Ceiling(extent.Y)
        );
    }
}