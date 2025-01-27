namespace DIKUArcade.Entities;

using System.Numerics;

/// <summary>
/// Represents a shape that remains stationary, meaning it does not have any velocity 
/// and is not intended to be affected by game physics. This class is a specialization 
/// of the <see cref="Shape"/> class and is used for static objects in a game environment.
/// </summary>
public class StationaryShape : Shape {
    /// <summary>
    /// Initializes a new instance of the <see cref="StationaryShape"/> class 
    /// with specified position and size.
    /// </summary>
    /// <param name="posX">The X-coordinate of the shape's position.</param>
    /// <param name="posY">The Y-coordinate of the shape's position.</param>
    /// <param name="width">The width of the shape.</param>
    /// <param name="height">The height of the shape.</param>
    public StationaryShape(float posX, float posY, float width, float height) {
        Position = new Vector2(posX, posY);
        Extent = new Vector2(width, height);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StationaryShape"/> class 
    /// with specified position and size as vectors.
    /// </summary>
    /// <param name="pos">A vector representing the shape's position (X, Y).</param>
    /// <param name="extent">A vector representing the shape's extent (width, height).</param>
    public StationaryShape(Vector2 pos, Vector2 extent) {
        Position = pos;
        Extent = extent;
    }

    /// <summary>
    /// Explicitly converts a <see cref="StationaryShape"/> to a <see cref="DynamicShape"/>. 
    /// The resulting <see cref="DynamicShape"/> will have the same position and extent, 
    /// but with a default velocity of (0, 0).
    /// </summary>
    /// <param name="sta">The <see cref="StationaryShape"/> to be converted.</param>
    /// <returns>A new <see cref="DynamicShape"/> instance with the same position and extent.</returns>
    public static explicit operator DynamicShape(StationaryShape sta) {
        return new DynamicShape(sta.Position, sta.Extent);
    }
}
