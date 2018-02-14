using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public class EntityContainer : IEnumerable {
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
        /// This function should return true if the Entity should be
        /// removed from the EntityContainer.
        /// </summary>
        /// <param name="entity"></param>
        public delegate void IteratorMethod(Entity entity);

        /// <summary>Iterate through all Entities in this EntityContainer.</summary>
        /// <remarks>This method can modify objects during iteration!
        /// If this functionality is undesired, iterate then through this
        /// EntityContainer using a 'foreach'-loop (from IEnumerable).</remarks>
        public void Iterate(IteratorMethod iterator) {
            var count = entities.Count;
            var newList = new List<Entity>(count);

            // iterate through entities
            for (int i = 0; i < count; i++) {
                iterator(entities[i]);
            }

            // keep Entities that have not been marked for deletion during iteration
            foreach (var entity in entities) {
                if (!entity.IsDeleted()) {
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

        // IEnumerable interface:
        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator GetEnumerator() {
            return new EntityContainerEnum(entities);
        }

        private class EntityContainerEnum : IEnumerator {
            private ReadOnlyCollection<Entity> entities;
            private int position = -1;

            public EntityContainerEnum(List<Entity> entities) {
                this.entities = entities.AsReadOnly();
            }

            public bool MoveNext() {
                position++;
                return position < entities.Count;
            }

            public void Reset() {
                position = -1;
            }

            object IEnumerator.Current => Current;

            public Entity Current {
                get {
                    try {
                        return entities[position];
                    } catch (IndexOutOfRangeException) {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        #endregion

    }
}