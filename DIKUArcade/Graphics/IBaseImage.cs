namespace DIKUArcade.Graphics;

using DIKUArcade.Entities;
public interface IBaseImage {
    void Render(Shape shape);
    void Render(Shape shape, Camera camera);
}
