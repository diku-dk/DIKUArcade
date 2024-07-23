namespace DIKUArcade.Events;

using System;
using System.Collections.Generic;

internal class IdGenerator {

    private readonly SortedSet<ulong> idSet = new SortedSet<ulong>();
    private readonly Queue<ulong> idQueue = new Queue<ulong>();
    private ulong nextId = 0;

    public IdGenerator() { }

    public ulong Get() {
        if (idQueue.Count != 0)
            return idQueue.Dequeue();

        var id = nextId;
        idSet.Add(id);
        nextId++;
        return id;
    }

    public void Remove(ulong id) {
        if (idSet.Remove(id)) {
            idQueue.Enqueue(id);
        }
    }

    public bool Contains(ulong id) {
        return idSet.Contains(id);
    }
}