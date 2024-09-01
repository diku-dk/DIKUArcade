namespace DIKUArcade.Graphics;

using System;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

/// <summary>
/// Manages a collection of animations, allowing for the addition and rendering of animations
/// within a fixed-size container. This class provides methods to add animations to the container,
/// clear the container, and render all active animations.
/// </summary>
public class AnimationContainer {
    
    /// <summary>
    /// Represents a slot in the container that can either be occupied or empty. 
    /// It holds an animation and a flag indicating whether it is in use.
    /// </summary>
    internal class OccupyValue<T> {
        /// <summary>
        /// Gets or sets a value indicating whether the slot is occupied by an animation.
        /// </summary>
        public bool Occupied { get; set; }

        /// <summary>
        /// The animation stored in the slot. Null if the slot is not occupied.
        /// </summary>
        public T? Value;
    }

    private OccupyValue<Animation>[] container;
    private int size;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimationContainer"/> class with the specified size.
    /// </summary>
    /// <param name="size">
    /// The maximum number of animations the container can hold. Must be a non-negative integer.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified size is negative.
    /// </exception>
    public AnimationContainer(int size) {
        if (size < 0) {
            throw new ArgumentOutOfRangeException(
                $"Cannot instantiate Animation container with negative size: {size}");
        }

        container = new OccupyValue<Animation>[size];
        this.size = size;
        ResetContainer();
    }

    /// <summary>
    /// Clears the container by resetting all slots to be empty. Initializes each slot with a default animation.
    /// </summary>
    public void ResetContainer() {
        for (int i = 0; i < size; i++) {
            container[i] = new OccupyValue<Animation>()
                {Occupied = false, Value = new Animation() {
                    Duration = 0, Shape = new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f)
                }};
        }
    }

    /// <summary>
    /// Adds a new animation to the first available slot in the container.
    /// If the container is full or no slots are available, returns false.
    /// </summary>
    /// <param name="shape">
    /// The <see cref="Shape"/> defining the position and size of the animation.
    /// </param>
    /// <param name="duration">
    /// The duration of the animation in milliseconds.
    /// </param>
    /// <param name="stride">
    /// The <see cref="ImageStride"/> used to render the sequence of images for the animation.
    /// </param>
    /// <returns>
    /// True if the animation was added successfully; otherwise, false.
    /// </returns>
    public bool AddAnimation(Shape shape, int duration, ImageStride stride) {
        for (int i = 0; i < size; i++) {
            var anim = container[i];
            if (!anim.Occupied) {
                anim.Occupied = true;
                anim.Value!.Shape!.Position = shape.Position;
                anim.Value.Shape.Extent = shape.Extent;
                anim.Value.Duration = duration;
                anim.Value.Stride = stride;
                anim.Value.ResetAnimation();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Renders all active animations in the container. If an animation is no longer active,
    /// its slot is marked as empty.
    /// </summary>
    /// <param name="context">
    /// The <see cref="WindowContext"/> used for rendering the animations.
    /// </param>
    public void RenderAnimations(WindowContext context) {
        foreach (var animation in container) {
            if (animation.Occupied && animation.Value!.IsActive()) {
                animation.Value.RenderAnimation(context);
            } else {
                animation.Occupied = false;
            }
        }
    }
}
