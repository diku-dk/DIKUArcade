using System.Collections.Generic;
using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public class EntityContainer {

        private List<Entity> entities;

        public EntityContainer(int size) {
            entities = new List<Entity>(size);
        }

        public EntityContainer() : this(50) { }

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
            entities = newList;
        }

        /// <summary>
        /// Delegate method for iterating through an EntityContainer.
        /// This function should return true if the shape should be
        /// removed from the EntityContainer.
        /// </summary>
        /// <param name="entity"></param>
        public delegate void IteratorMethod(Entity entity);

        public void Iterate(IteratorMethod iterator) {
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

        /// <summary>
        /// Remove all entities from this container
        /// </summary>
        public void ClearContainer() {
            entities.Clear();
        }
    }
}