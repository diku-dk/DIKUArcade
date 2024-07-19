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
        var originalExtent = Texture.originalExtent;
        var extent = originalExtent * context.Camera.WindowExtentScaling(shape, originalExtent);
        Texture.Render(
            context,
            (int) position.X,
            (int) position.Y,
            (int) extent.X,
            (int) extent.Y
        );
    }
}