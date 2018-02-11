using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public struct Entity {
        private bool markedForDeletion;

        public Shape Shape { get; set; }
        public IBaseImage Image { get; set; }

        public Entity(Shape shape, IBaseImage image) {
            markedForDeletion = false;
            Shape = shape;
            Image = image;
        }

        public void DeleteEntity() {
            markedForDeletion = true;
        }

        public bool IsDeleted() {
            return markedForDeletion;
        }

        public void RenderEntity() {
            Image.Render(Shape);
        }
    }
}