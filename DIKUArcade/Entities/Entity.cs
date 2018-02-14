using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public class Entity {
        public Shape Shape { get; set; }
        public IBaseImage Image { get; set; }

        private bool isDeleted;

        public Entity(Shape shape, IBaseImage image) {
            isDeleted = false;
            Shape = shape;
            Image = image;
        }

        /// <summary>
        /// Make an Entity as ready for being deleted.
        /// This functionality is needed for the EntityContainer class.
        /// </summary>
        public void DeleteEntity() {
            isDeleted = true;
        }

        /// <summary>
        /// Check if this Entity has been marked as ready for being deleted.
        /// This functionality is needed for the EntityContainer class.
        /// </summary>
        public bool IsDeleted() {
            return isDeleted;
        }

        public void RenderEntity() {
            Image.Render(Shape);
        }
    }
}