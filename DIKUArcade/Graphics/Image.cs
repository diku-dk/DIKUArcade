namespace DIKUArcade.Graphics;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

public class Image : IBaseImage {

    private Texture texture;

    public Image(string imageFile) {
        texture = new Texture(imageFile);
    }

    public Image(Texture texture) {
        this.texture = texture;
    }

    public void Render(Shape shape) {
        var window = Window.CurrentFocus();
        if (window is null || window.WindowContext is null)
            throw new Exception("The window context must not be null.");
        
        Render(shape, window.WindowContext.Value);
    }

    public void Render(Shape shape, WindowContext context) {
        var windowMatrix = context.Camera.WindowMatrix(shape, texture.originalExtent);
        var transformedPosition = Vector2.Transform(shape.Position, windowMatrix);

        texture.Render(
            context,
            (int) transformedPosition.X,
            (int) transformedPosition.Y,
            (int) width,
            (int) height
        );
    }
}