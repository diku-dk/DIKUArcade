namespace DIKUArcade.Graphics;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

public class Camera {
    public Vector2 Offset { get; set; } = Vector2.Zero;
    public Vector2 Scale { get; set; } = Vector2.Zero;
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

    public Vector2 WindowPosition(Vector2 position, Vector2 windowExtent) {
        var newPosition = WindowVector * (position * (Scale + Vector2.One) - Scale / 2 + Offset); 
        return new Vector2(newPosition.X, Height - (newPosition.Y + windowExtent.Y));
    }

    public Vector2 WindowPosition(Shape shape, Vector2 windowExtent) {
        return WindowPosition(shape.Position, windowExtent);
    }

    public Vector2 WindowExtent(Vector2 extent) {
        var v = WindowVector * extent * (Scale + Vector2.One);
        var x = (int) v.X;
        var y = (int) v.Y;
        return new Vector2(x + x % 2, y + y % 2); 
    }

    public Vector2 WindowExtent(Shape shape) {
        return WindowExtent(shape.Extent);
    }

    public Matrix3x2 WindowMatrix(Shape shape, Vector2 originalExtent) {
        var windowExtent = WindowExtent(shape);
        var windowExtentScaling = windowExtent / originalExtent;
        var windowPosition = WindowPosition(shape, windowExtent);
        return new Matrix3x2(
            windowExtentScaling.X, 0,
            0, windowExtentScaling.Y,
            windowPosition.X, windowPosition.Y
        );
    }

}