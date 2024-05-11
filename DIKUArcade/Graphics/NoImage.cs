namespace DIKUArcade.Graphics;

using System;
using DIKUArcade.Entities;

/// A stub for an image, to use with entities that are non-drawable
public class NoImage : IBaseImage {
    public NoImage() {}           
    public void Render(Shape shape) {}
    public void Render(Shape shape, Camera camera) {}
}
