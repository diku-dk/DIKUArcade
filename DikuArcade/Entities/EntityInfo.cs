using DikuArcade.Strategies;
using DikuArcade.Graphics;

namespace DikuArcade.Entities
{
    public class EntityInfo
    {
        public Entity Entity;
        public MovementStrategy MovementStrategy;
        
        public EntityInfo(Entity entity, MovementStrategy strat)
        {
            Entity = entity;
            MovementStrategy = strat;
        }
    }
}