using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public struct Entity {
        public Shape Shape { get; set; }
        public IBaseImage Image { get; set; }

        public Entity(Shape shape, IBaseImage image) {
            Shape = shape;
            Image = image;
        }

        public void RenderEntity() {
            Image.Render(this.Shape);
        }
    }
}