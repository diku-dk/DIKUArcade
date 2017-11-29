using DIKUArcade.Strategies;
using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public struct EntityActor {
        public Entity Entity;
        public MovementStrategy MovementStrategy;

        public EntityActor(Entity entity, MovementStrategy strat) {
            Entity = entity;
            MovementStrategy = strat;
        }
    }
}