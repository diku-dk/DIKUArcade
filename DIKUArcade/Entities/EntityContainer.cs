using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Strategies;

namespace DIKUArcade.Entities
{
    public class GameObjectContainer
    {
        // TODO: Consider using IEnumerable interface.. (better data structure?)
        private readonly List<EntityInfo> _entities;
        
        public GameObjectContainer()
        {
            _entities = new List<EntityInfo>();
        }

        public void AddStationaryEntity(Vec2f pos, Vec2f extent)
        {
            // TODO: find a way to provide a default (no-action) movement strategy
            _entities.Add(new EntityInfo(new StationaryEntity(pos, extent), new MovementStrategy()));
        }

        public void AddDynamicEntity(Vec2f pos, Vec2f extent, Vec2f dir, MovementStrategy strat)
        {
            _entities.Add(new EntityInfo(new DynamicEntity(pos, extent, dir), strat));
        }
        
        // TODO: Should the input instead be an (integer) ID?
        public void RemoveGameObject(DynamicEntity obj)
        {
            
        }

        public void RemoveStationaryGameObject(StationaryEntity obj)
        {
            
        }

        // TODO: Is it better to make this private instead?
        public delegate void IteratorMethod(EntityInfo entity);

        public void IterateGameObjects(IteratorMethod iterator)
        {
            // iterate through entities
            foreach (var entity in _entities)
            {
                iterator(entity);
            }

            // TODO: Next do filtering, checking IsDeleted() for each entity in the collection!
        }
    }
}