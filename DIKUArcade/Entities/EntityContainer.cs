using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Strategies;

namespace DIKUArcade.Entities {
    public class GameObjectContainer {
        // TODO: Is it better to make this private instead?
        public delegate void IteratorMethod(EntityInfo entity);

        // TODO: Consider using IEnumerable interface.. (better data structure?)
        private readonly List<EntityInfo> entities;

        public GameObjectContainer() {
            entities = new List<EntityInfo>();
        }

        public void AddStationaryEntity(Vec2F pos, Vec2F extent) {
            // TODO: find a way to provide a default (no-action) movement strategy
            entities.Add(new EntityInfo(new StationaryEntity(pos, extent),
                new MovementStrategy()));
        }

        public void AddDynamicEntity(Vec2F pos, Vec2F extent, Vec2F dir, MovementStrategy strat) {
            entities.Add(new EntityInfo(new DynamicEntity(pos, extent, dir), strat));
        }

        // TODO: Should the input instead be an (integer) ID?
        public void RemoveGameObject(DynamicEntity obj) { }

        public void RemoveStationaryGameObject(StationaryEntity obj) { }

        public void IterateGameObjects(IteratorMethod iterator) {
            // iterate through entities
            foreach (var entity in entities) {
                iterator(entity);
            }

            // TODO: Next do filtering, checking IsDeleted() for each entity in the collection!
        }
    }
}