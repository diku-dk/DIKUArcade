using DIKUArcade.Strategies;
using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public struct EntityActor {
        public Entity Entity { get; set; }
        public MovementStrategy MovementStrategy { get; set; }
        public IBaseImage Image { get; set; }

        public EntityActor(Entity entity, MovementStrategy strategy, IBaseImage image) {
            Entity = entity;
            MovementStrategy = strategy;
            Image = image;
        }

        public void RenderEntity() {
            Image.Render();
        }
    }
}