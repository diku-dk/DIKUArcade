using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DIKUArcade.EventBus
{
    public class GameEventQueue<TP> : ICollection, IReadOnlyCollection<TP>
    {
        private readonly ConcurrentQueue<TP> _queue= new ConcurrentQueue<TP>();

        public IEnumerator<TP> GetEnumerator()
        {
            return ((IEnumerable<TP>)_queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            _queue.CopyTo((TP[])array, index);
        }

        int ICollection.Count => _queue.Count;

        public bool IsSynchronized { get; }
        public object SyncRoot { get; }

        int IReadOnlyCollection<TP>.Count => _queue.Count;

        public void Enqueue(TP gameEvent)
        {
            _queue.Enqueue(gameEvent);
        }

        public TP Dequeue()
        {
            TP gameEvent;
            _queue.TryDequeue(out gameEvent);
            return gameEvent;
        }

        public void Flush()
        {
            TP gameEventDummy;
            while(!_queue.IsEmpty)
            {
                _queue.TryDequeue(out gameEventDummy);
            }
        }
    }
}