using System;

namespace DIKUArcade {
    internal class PriorityQueue<Key, Value> where Key : IComparable {
        private readonly int size;

        private struct PQElement {
            public Key key;
            public Value value;
            public int next;
            public bool used;
        }

        private PQElement[] queue;
        private int count;
        private int head;

        public PriorityQueue(int size) {
            if (size < 0) {
                throw new ArgumentOutOfRangeException("PriorityQueue(): size may not be negative!");
            }
            this.size = size;
            queue = new PQElement[size];
            head = -1;
            count = 0;

            for (int i = 0; i < size; i++) {
                queue[i].next = -1;
                queue[i].used = false;
            }
        }

        public bool Insert(Key key, Value value) {
            if (count >= size) return false; // no can do

            // linear search from top, may be optimized later!
            int insertIndex;
            for (insertIndex = 0; insertIndex < size; insertIndex++) {
                if (!queue[insertIndex].used) break;
            }

            queue[insertIndex].used = true;
            queue[insertIndex].key = key;
            queue[insertIndex].value = value;

            return false;
        }
    }
}
