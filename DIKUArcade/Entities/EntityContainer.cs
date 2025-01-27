namespace DIKUArcade.Entities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

/// <summary>
/// Represents a container that manages a collection of `Entity` objects. 
/// This container allows adding, iterating, rendering, and removing entities.
/// </summary>
public class EntityContainer : IEnumerable {
    /// <summary>
    /// The internal list that holds the entities in this container.
    /// </summary>
    private List<Entity> entities;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityContainer"/> class 
    /// with a specified initial capacity.
    /// </summary>
    /// <param name="size">The initial capacity of the container.</param>
    public EntityContainer(int size) {
        entities = new List<Entity>(size);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityContainer"/> class 
    /// with a default initial capacity of 50.
    /// </summary>
    public EntityContainer() : this(50) { }

    /// <summary>
    /// Adds a stationary entity to the container.
    /// </summary>
    /// <param name="ent">The stationary shape of the entity.</param>
    /// <param name="img">The image associated with the entity.</param>
    public void AddStationaryEntity(StationaryShape ent, IBaseImage img) {
        entities.Add(new Entity(ent, img));
    }

    /// <summary>
    /// Adds a dynamic entity to the container.
    /// </summary>
    /// <param name="ent">The dynamic shape of the entity.</param>
    /// <param name="img">The image associated with the entity.</param>
    public void AddDynamicEntity(DynamicShape ent, IBaseImage img) {
        entities.Add(new Entity(ent, img));
    }

    /// <summary>
    /// Delegate method used for iterating through entities in the container.
    /// This delegate allows modifying or performing actions on each entity 
    /// during iteration.
    /// </summary>
    /// <param name="entity">The entity currently being iterated over.</param>
    public delegate void IteratorMethod(Entity entity);

    /// <summary>
    /// Iterates through all entities in the container, allowing modification 
    /// or action on each entity through the provided delegate method.
    /// Entities marked for deletion are removed from the container after 
    /// iteration.
    /// </summary>
    /// <param name="iterator">The delegate method to be applied to each entity.</param>
    /// <remarks>
    /// This method allows modification of entities during iteration. 
    /// If this is not desired, use a `foreach` loop provided by the 
    /// `IEnumerable` interface instead.
    /// </remarks>
    public void Iterate(IteratorMethod iterator) {
        var count = entities.Count;
        var newList = new List<Entity>(count);

        // Iterate through entities and apply the iterator method
        for (int i = 0; i < count; i++) {
            iterator(entities[i]);
        }

        // Keep only entities that have not been marked for deletion
        foreach (var entity in entities) {
            if (!entity.IsDeleted()) {
                newList.Add(entity);
            }
        }
        entities = newList;
    }

    /// <summary>
    /// Renders all entities in the container using the provided rendering context.
    /// </summary>
    /// <param name="context">The rendering context used to draw the entities.</param>
    public void RenderEntities(WindowContext context) {
        foreach (Entity entity in entities) {
            entity.Image.Render(context, entity.Shape);
        }
    }

    /// <summary>
    /// Removes all entities from the container.
    /// </summary>
    public void ClearContainer() {
        entities.Clear();
    }

    /// <summary>
    /// Returns the number of entities currently in the container.
    /// </summary>
    /// <returns>The count of entities in the container.</returns>
    public int CountEntities() {
        return entities.Count;
    }
    
    /// <summary>
    /// Returns an enumerator that iterates through the `EntityContainer`.
    /// </summary>
    /// <returns>An enumerator for the `EntityContainer`.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the `EntityContainer`.
    /// </summary>
    /// <returns>An enumerator for the `EntityContainer`.</returns>
    public IEnumerator GetEnumerator() {
        return new EntityContainerEnum(entities);
    }

    /// <summary>
    /// Private enumerator class for iterating over the entities in the 
    /// `EntityContainer`. Implements the `IEnumerator` interface.
    /// </summary>
    private class EntityContainerEnum : IEnumerator {
        /// <summary>
        /// A read-only collection of the entities in the container.
        /// </summary>
        private ReadOnlyCollection<Entity> entities;

        /// <summary>
        /// The current position of the enumerator in the entity list.
        /// </summary>
        private int position = -1;

        /// <summary>
        /// Initializes a new instance of the `EntityContainerEnum` class.
        /// </summary>
        /// <param name="entities">The list of entities to iterate over.</param>
        public EntityContainerEnum(List<Entity> entities) {
            this.entities = entities.AsReadOnly();
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><c>true</c> if the enumerator was successfully advanced 
        /// to the next element; <c>false</c> if the enumerator has passed 
        /// the end of the collection.</returns>
        public bool MoveNext() {
            position++;
            return position < entities.Count;
        }

        /// <summary>
        /// Resets the enumerator to its initial position, which is before 
        /// the first element in the collection.
        /// </summary>
        public void Reset() {
            position = -1;
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Gets the current `Entity` in the collection.
        /// </summary>
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
}
