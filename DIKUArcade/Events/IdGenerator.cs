namespace DIKUArcade.Events;

using System;
using System.Collections.Generic;

/// <summary>
/// The IdGenerator class is responsible for generating unique identifiers (IDs) and managing
/// their lifecycle. It provides a mechanism to create new IDs, remove them when no longer
/// needed, and recycle these IDs for future use. This ensures efficient management of IDs,
/// preventing continuous growth by reusing IDs that are no longer active.
/// </summary>
internal class IdGenerator {

    private readonly SortedSet<ulong> idSet = new SortedSet<ulong>();
    private readonly Queue<ulong> idQueue = new Queue<ulong>();
    private ulong nextId = 0;

    public IdGenerator() { }

    /// <summary>
    /// Generates and returns a unique ID.
    /// </summary>
    /// <returns>A unique ulong ID.</returns>
    public ulong Get() {
        // Reuse IDs from the queue if available.
        if (idQueue.Count != 0)
            return idQueue.Dequeue();

        // Otherwise, generate a new ID.
        var id = nextId;
        idSet.Add(id);
        nextId++;
        return id;
    }

    /// <summary>
    /// Removes an ID, making it available for future reuse.
    /// </summary>
    /// <param name="id">The ID to remove.</param>
    public void Remove(ulong id) {
        // If the ID is in the set, remove it and enqueue it for reuse.
        if (idSet.Remove(id)) {
            idQueue.Enqueue(id);
        }
    }

    /// <summary>
    /// Checks if the given ID exists in the generator.
    /// </summary>
    /// <param name="id">The ID to check for.</param>
    /// <returns>True if the ID exists, otherwise false.</returns>
    public bool Contains(ulong id) {
        return idSet.Contains(id);
    }
}
