using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DIKUArcade.EventBus
{
    /// <summary>
    /// Game event queue based on the concurrent queue implementation of the .NET framework 
    /// offering a simplified facade for the game event bus system.
    /// </summary>
    /// <typeparam name="TP">EventType data type.</typeparam>
    public class GameEventQueue<TP> : ICollection, IReadOnlyCollection<TP>
    {
        /// <summary>
        /// Core component of the event queue.
        /// </summary>
        private readonly ConcurrentQueue<TP> _queue= new ConcurrentQueue<TP>();

        /// <summary>
        /// Enumerator access for event queue.
        /// </summary>
        /// <returns>IEnumerator of concurrent queue.</returns>
        public IEnumerator<TP> GetEnumerator()
        {
            return ((IEnumerable<TP>)_queue).GetEnumerator();
        }

        /// <summary>
        /// Generic enumerator access for event queue.
        /// </summary>
        /// <returns>Generic IEnumerator of concurrent queue.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Copy semantics for fast array initialization and processing.
        /// </summary>
        /// <param name="array">Copy queue elements to array.</param>
        /// <param name="index">Copy queue elements to which index position.</param>
        public void CopyTo(Array array, int index)
        {
            _queue.CopyTo((TP[])array, index);
        }

        int ICollection.Count => _queue.Count;

        public bool IsSynchronized { get; }
        public object SyncRoot { get; }

        int IReadOnlyCollection<TP>.Count => _queue.Count;

        /// <summary>
        /// Enqueue a game event in the event queue.
        /// </summary>
        /// <param name="gameEvent">Event which is enqueued.</param>
        public void Enqueue(TP gameEvent)
        {
            _queue.Enqueue(gameEvent);
        }

        /// <summary>
        /// Dequeues a game event from the event queue.
        /// </summary>
        /// <returns>A game event from event queue.</returns>
        public TP Dequeue()
        {
            TP gameEvent;
            _queue.TryDequeue(out gameEvent);
            return gameEvent;
        }

        /// <summary>
        /// Checks if the element queue is empty.
        /// </summary>
        /// <returns>true if game event queue is empty, otherwise false.</returns>
        public bool IsEmpty()
        {
            return _queue.IsEmpty;
        }

        /// <summary>
        /// Flushes all elements stored in the event queue.
        /// TODO: Method is slow and needs a rewrite.
        /// </summary>
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