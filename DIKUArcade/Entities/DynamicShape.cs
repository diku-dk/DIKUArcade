namespace DIKUArcade.Entities;

using System.Numerics;

/// <summary>
/// Represents a dynamic shape in the game world, characterized by a position, 
/// size (extent), and velocity. This class extends the basic `Shape` class by 
/// adding a velocity vector, allowing the shape to move dynamically.
/// </summary>
public class DynamicShape : Shape {
    /// <summary>
    /// The velocity of the dynamic shape, represented as a vector.
    /// This vector determines the change in the shape's position 
    /// per unit of time.
    /// </summary>
    public Vector2 Velocity;

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicShape"/> class 
    /// with the specified position and size. The velocity is initialized 
    /// to zero.
    /// </summary>
    /// <param name="posX">The X-coordinate of the shape's position.</param>
    /// <param name="posY">The Y-coordinate of the shape's position.</param>
    /// <param name="width">The width of the shape.</param>
    /// <param name="height">The height of the shape.</param>
    public DynamicShape(float posX, float posY, float width, float height) {
        Position = new Vector2(posX, posY);
        Velocity = new Vector2();
        Extent = new Vector2(width, height);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicShape"/> class 
    /// with the specified position, size, and initial velocity.
    /// </summary>
    /// <param name="posX">The X-coordinate of the shape's position.</param>
    /// <param name="posY">The Y-coordinate of the shape's position.</param>
    /// <param name="width">The width of the shape.</param>
    /// <param name="height">The height of the shape.</param>
    /// <param name="dirX">The X-component of the initial velocity vector.</param>
    /// <param name="dirY">The Y-component of the initial velocity vector.</param>
    public DynamicShape(float posX, float posY, float width, float height,
        float dirX, float dirY) : this(posX, posY, width, height) {
        Velocity.X = dirX;
        Velocity.Y = dirY;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicShape"/> class 
    /// with the specified position and size. The velocity is initialized 
    /// to zero.
    /// </summary>
    /// <param name="pos">The position vector of the shape.</param>
    /// <param name="extent">The extent (size) vector of the shape.</param>
    public DynamicShape(Vector2 pos, Vector2 extent) {
        Position = pos;
        Extent = extent;
        Velocity = new Vector2(0f, 0f); // Initialize velocity to zero to avoid problems
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicShape"/> class 
    /// with the specified position, size, and velocity.
    /// </summary>
    /// <param name="pos">The position vector of the shape.</param>
    /// <param name="extent">The extent (size) vector of the shape.</param>
    /// <param name="dir">The initial velocity vector of the shape.</param>
    public DynamicShape(Vector2 pos, Vector2 extent, Vector2 dir) {
        Position = pos;
        Extent = extent;
        Velocity = dir;
    }

    /// <summary>
    /// Changes the velocity of the dynamic shape to the specified vector.
    /// </summary>
    /// <param name="dir">The new velocity vector.</param>
    public void ChangeVelocity(Vector2 dir) {
        Velocity = dir;
    }

    /// <summary>
    /// Moves the shape by adding its velocity to its current position.
    /// This method overrides the base <see cref="Shape.Move"/> method.
    /// </summary>
    public override void Move() {
        Position += Velocity;
    }

    /// <summary>
    /// Explicitly converts a <see cref="DynamicShape"/> to a 
    /// <see cref="StationaryShape"/> by retaining the position and extent,
    /// but discarding the velocity.
    /// </summary>
    /// <param name="obj">The dynamic shape to be converted.</param>
    /// <returns>A new instance of <see cref="StationaryShape"/> with the 
    /// same position and extent as the original dynamic shape.</returns>
    public static explicit operator StationaryShape(DynamicShape obj) {
        return new StationaryShape(obj.Position, obj.Extent);
    }
}
