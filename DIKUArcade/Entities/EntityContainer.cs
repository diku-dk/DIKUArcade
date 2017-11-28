using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Strategies;
using DIKUArcade.DataStructures;

namespace DIKUArcade.Entities {
    public class GameObjectContainer {

        // TODO: A better data structure can be used here to minimize overhead when copying memory all the time.
        private readonly ObjectContainer<EntityInfo> entities;

        public GameObjectContainer(int size) {
            entities = new List<EntityInfo>();
        }

        public void AddStationaryEntity(Vec2F pos, Vec2F extent) {
            // TODO: find a way to provide a default (no-action) movement strategy
            entities.Add(new EntityInfo(new StationaryEntity(pos, extent),
                new MovementStrategy()));
        }

        public void AddDynamicEntity(Vec2F pos, Vec2F extent, Vec2F dir, MovementStrategy strat) {
            entitiesPendingForAdding.Add(new EntityInfo(new DynamicEntity(pos, extent, dir), strat));
        }

        public delegate void IteratorMethod(EntityInfo entity);

        public void IterateGameObjects(IteratorMethod iterator) {
            // iterate through entities
            foreach (var entity in entities) {
                iterator(entity);
            }

            // TODO: Next do filtering, checking IsDeleted() for each entity in the collection!
        }
    }
}