namespace DIKUArcade.Graphics;

using DIKUArcade.Entities;
using DIKUArcade.GUI;

public interface IBaseImage {
    void Render(WindowContext ctx, Shape shape);
}