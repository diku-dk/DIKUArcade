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

        /// <summary>
        /// Delegate method for iterating through an EntityContainer.
        /// This function should return true if the shape should be
        /// removed from the EntityContainer.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True if the entity should be deleted, otherwise false.</returns>
        public delegate bool IteratorMethod(Entity entity);

        public void Iterate(IteratorMethod iterator) {
            var newList = new List<Entity>(entities.Count);
            // iterate through entities
            foreach (var entity in entities) {
                if (!iterator(entity)) {
                    newList.Add(entity);
                }
            }
            entities = newList;
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

        /// <summary>
        /// Count the number of entities in the EntityContainer
        /// </summary>
        public int CountEntities() {
            return entities.Count;
        }
    }
}