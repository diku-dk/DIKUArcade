using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Strategies;

namespace DIKUArcade.Entities {
    public class EntityContainer {

        // TODO: A better data structure can be used here to minimize overhead when copying memory all the time.
        private List<EntityActor> entities;
        private readonly List<EntityActor> pendingEntities;

        public EntityContainer(int size) {
            entities = new List<EntityActor>(size);
            pendingEntities = new List<EntityActor>(size);
        }

        public EntityContainer() : this(100) { }

        public void AddStationaryEntity(StationaryEntity ent, IBaseImage img) {
            // TODO: No strategy should be provided here!
            entities.Add(new EntityActor(ent, new MovementStrategy(), img));
        }

        public void AddDynamicEntity(DynamicEntity ent, MovementStrategy strat, IBaseImage img) {
            pendingEntities.Add(new EntityActor(ent, strat, img));
        }

        private void TendToPending() {
            var newList = new List<EntityActor>(entities.Count);
            foreach (var ent in entities) {
                if (!ent.Entity.IsDeleted()) {
                    newList.Add(ent);
                }
            }
            foreach (var ent in pendingEntities) {
                newList.Add(ent);
            }
            entities = newList;
        }

        /// <summary>
        /// Delegate method for iterating through an EntityContainer.
        /// This function should return true if the entity should be
        /// removed from the EntityContainer.
        /// </summary>
        /// <param name="entity"></param>
        public delegate void IteratorMethod(EntityActor entity);

        public void IterateGameObjects(IteratorMethod iterator) {
            var entitiesPendingForRemoval = new List<EntityActor>(entities.Count);

            // iterate through entities
            foreach (var entity in entities) {
                iterator(entity);
            }
            TendToPending();
        }

        /// <summary>
        /// Render all entities in this EntityContainer
        /// </summary>
        public void RenderEntities() {
            foreach (EntityActor actor in entities) {
                actor.Image.Render(actor.Entity);
            }
        }
    }
}