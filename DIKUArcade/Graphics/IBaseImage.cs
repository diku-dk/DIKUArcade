using DIKUArcade.Entities;
using DIKUArcade.GUI;

namespace DIKUArcade.Graphics {
    public interface IBaseImage {
        void Render(Shape shape);

        void Render(Shape shape, WindowContext ctx);
    }
}