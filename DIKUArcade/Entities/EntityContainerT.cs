using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DIKUArcade.Graphics;

namespace DIKUArcade.Entities {
    public sealed class EntityContainer<T> : IEnumerable where T: Entity {
        private List<T> entities;

        public EntityContainer(int size) {
            entities = new List<T>(size);
        }

        public EntityContainer() : this(50) { }

        public void AddStationaryEntity(T obj) {
            entities.Add(obj);
        }

        public void AddDynamicEntity(T obj) {
            entities.Add(obj);
        }

        /// <summary>
        /// Delegate method for iterating through an EntityContainer.
        /// This function should return true if the object should be
        /// removed from the EntityContainer.
        /// </summary>
        /// <param name="obj">Generic object of type T</param>
        public delegate void IteratorMethod(T obj);

        /// <summary>Iterate through all objects in this EntityContainer.</summary>
        /// <remarks>This method can modify objects during iteration!
        /// If this functionality is undesired, iterate then through this
        /// EntityContainer using a 'foreach'-loop (from IEnumerable).</remarks>
        public void Iterate(IteratorMethod iterator) {
            var count = entities.Count;
            var newList = new List<T>(count);

            // iterate through entities
            for (int i = 0; i < count; i++) {
                iterator(entities[i]);
            }

            // keep Entities that have not been marked for deletion during iteration
            foreach (var obj in entities) {
                if (!obj.IsDeleted()) {
                    newList.Add(obj);
                }
            }
            entities = newList;
        }

        /// <summary>
        /// Render all entities in this EntityContainer
        /// </summary>
        public void RenderEntities() {
            foreach (var obj in entities) {
                obj.Image.Render(obj.Shape);
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
            private ReadOnlyCollection<T> entities;
            private int position = -1;

            public EntityContainerEnum(List<T> entities) {
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

            public T Current {
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