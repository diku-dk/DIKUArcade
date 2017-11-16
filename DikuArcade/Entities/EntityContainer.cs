using System.Collections.Generic;
using DikuArcade.Math;
using DikuArcade.Strategies;

namespace DikuArcade.Entities
{
    public class GameObjectContainer
    {
        // TODO: Consider using IEnumerable interface.. (better data structure?)
        private List<EntityInfo> _entities;
        
        public GameObjectContainer()
        {
            _entities = new List<EntityInfo>();
        }

        public void AddStaticEntity(Vec2f pos, Vec2f extent)
        {
            // TODO: find a way to provide a default (no-action) movement strategy
            _entities.Add(new EntityInfo(new StaticEntity(pos, extent), new MovementStrategy()));
        }

        public void AddDynamicEntity(Vec2f pos, Vec2f extent, Vec2f dir, MovementStrategy strat)
        {
            _entities.Add(new EntityInfo(new DynamicEntity(pos, extent, dir), strat));
        }
        
        public void RemoveGameObject(DynamicEntity obj)
        {
            
        }

        public void RemoveStaticGameObject(StaticEntity obj)
        {
            
        }

        public void IterateGameObjects(int someDelegateOrFunctionPointer)
        {
            foreach (var obj in _entities)
            {
                //someDelegateOrFunctionPointer(obj);
            }
        }
    }
}