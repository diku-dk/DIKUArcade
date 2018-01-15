using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Strategies;

namespace DIKUArcade.Entities {
    public class EntityContainer {

        private List<Entity> entities;
        private readonly List<Entity> pendingEntities;

        public EntityContainer(int size) {
            entities = new List<Entity>(size);
            pendingEntities = new List<Entity>(size);
        }

        public EntityContainer() : this(100) { }

        public void AddStationaryEntity(StationaryShape ent, IBaseImage img) {
            entities.Add(new Entity(ent, img));
        }

        public void AddDynamicEntity(DynamicShape ent, IBaseImage img) {
            entities.Add(new Entity(ent, img));
        }

        private void TendToPending() {
            var newList = new List<Entity>(entities.Count);
            foreach (var ent in entities) {
                if (!ent.Shape.IsDeleted()) {
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
        /// This function should return true if the shape should be
        /// removed from the EntityContainer.
        /// </summary>
        /// <param name="entity"></param>
        public delegate void IteratorMethod(Entity entity);

        // TODO: Rename to 'Iterate'
        public void Iterate(IteratorMethod iterator) {
            var entitiesPendingForRemoval = new List<Entity>(entities.Count);

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
            foreach (Entity entity in entities) {
                entity.Image.Render(entity.Shape);
            }
        }
    }
}