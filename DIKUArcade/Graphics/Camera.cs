namespace DIKUArcade.Graphics;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

public class Camera {
    public Vector2 Offset { get; set; } = Vector2.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public Vector2 WindowVector { get => new Vector2(Width, Height); }
    public Camera (Vector2 offset, Vector2 scale, int width, int height) {
        Offset = offset;
        Scale = scale;
        Width = width;
        Height = height;
    }

    public Camera (int width, int height) {
        Width = width;
        Height = height;
    }

    public Matrix3x2 PositionMatrix(Vector2 extent) {
        return new Matrix3x2(
            Width, 0.0f,
            0.0f, -Height,
            0.0f, Height - (extent.Y * Height)
        );
    }

    public Vector2 WindowPosition(Shape shape) {
        return Vector2.Transform(
            shape.Position,
            PositionMatrix(shape.Extent)
        );
    }

    public Matrix3x2 PositionMatrix(Shape shape) {
        return PositionMatrix(shape.Extent);
    }

    public Vector2 WindowExtent(Vector2 extent, Vector2 originalExtent) {
        return extent * WindowVector / originalExtent;
    }

    public Vector2 WindowExtent(Shape shape, Vector2 originalExtent) {
        return WindowExtent(shape.Extent, originalExtent);
    }

    public Matrix3x2 WindowMatrix(Shape shape, Vector2 originalExtent) {
        var windowPosition = WindowPosition(shape);
        var windowExtent = WindowExtent(shape, originalExtent);
        return new Matrix3x2(
            windowExtent.X, 0,
            0, windowExtent.Y,
            windowPosition.X, windowPosition.Y
        );
    }

}