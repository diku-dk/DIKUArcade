using System;

namespace DIKUArcade.DataStructures {
    public class ObjectContainer<T> where T: struct {
        internal struct ObjectItem {
            public bool IsOccupied { get; set; }
            public T Element { get; set; }

            public ObjectItem(bool occupied) {
                IsOccupied = occupied;
                Element = new T();
            }
        }

        private ObjectItem [] container;
        private int size;

        private void Initialize() {
            for (int i = 0; i < size; i++) {
                container[i] = new ObjectItem();
            }
        }

        public ObjectContainer(int size) {
            if (size < 1) {
                throw new ArgumentOutOfRangeException($"Size must be a positive integer: {size}");
            }
            this.size = size;
            container = new ObjectItem[this.size];
        }
    }
}