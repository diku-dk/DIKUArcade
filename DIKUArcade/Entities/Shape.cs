namespace DIKUArcade.Entities;

using System;
using System.Numerics;

/// <summary>
/// Represents a basic shape with properties such as position, extent, and rotation.
/// Provides various methods to manipulate the shape's size, position, and rotation.
/// This class serves as a base class for more specialized shapes like DynamicShape 
/// and StationaryShape.
/// </summary>
public class Shape {
    /// <summary>
    /// Gets or sets the shape's rotational angle measured in radians.
    /// </summary>
    public float Rotation { get; set; }
    
    /// <summary>
    /// Gets or sets the position of the shape as a 2D vector (X, Y).
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets or sets the extent (width and height) of the shape as a 2D vector.
    /// </summary>
    public Vector2 Extent { get; set; }

    /// <summary>
    /// Attempts to cast this Shape instance to a DynamicShape. 
    /// If the cast fails, returns a new DynamicShape instance 
    /// initialized with this shape's Position and Extent properties, 
    /// and a default (0,0) Velocity vector.
    /// </summary>
    /// <returns>A DynamicShape instance.</returns>
    public DynamicShape AsDynamicShape() {
        var shape = this as DynamicShape;
        return shape ?? new DynamicShape(Position, Extent);
    }

    /// <summary>
    /// Attempts to cast this Shape instance to a StationaryShape. 
    /// If the cast fails, returns a new StationaryShape instance 
    /// initialized with this shape's Position and Extent properties.
    /// </summary>
    /// <returns>A StationaryShape instance.</returns>
    public StationaryShape AsStationaryShape() {
        var sta = this as StationaryShape;
        return sta ?? new StationaryShape(Position, Extent);
    }

    /// <summary>
    /// Scales the shape uniformly in both dimensions by the specified factor.
    /// </summary>
    /// <param name="scale">The scaling factor to apply to both dimensions.</param>
    public void Scale(float scale) {
        Extent *= scale;
    }

    /// <summary>
    /// Scales the shape non-uniformly by applying a different scaling factor 
    /// to each dimension.
    /// </summary>
    /// <param name="scalar">A vector containing the scaling factors for each dimension.</param>
    public void Scale(Vector2 scalar) {
        Extent *= scalar;
    }
    
    /// <summary>
    /// Scales the width of the shape by the specified factor.
    /// </summary>
    /// <param name="scale">The scaling factor to apply to the width.</param>
    public void ScaleX(float scale) {
        Extent = new Vector2(Extent.X * scale, Extent.Y);
    }

    /// <summary>
    /// Scales the height of the shape by the specified factor.
    /// </summary>
    /// <param name="scale">The scaling factor to apply to the height.</param>
    public void ScaleY(float scale) {
        Extent = new Vector2(Extent.X, Extent.Y * scale);
    }

    /// <summary>
    /// Scales the width of the shape by the specified factor, keeping 
    /// the center of the shape fixed.
    /// </summary>
    /// <param name="scale">The scaling factor to apply to the width.</param>
    public void ScaleXFromCenter(float scale) {
        var posX = Position.X + Extent.X / 2.0f - (Extent.X / 2.0f * scale);
        Position = new Vector2(posX, Position.Y);
        Extent = new Vector2(Extent.X * scale, Extent.Y);
    }

    /// <summary>
    /// Scales the height of the shape by the specified factor, keeping 
    /// the center of the shape fixed.
    /// </summary>
    /// <param name="scale">The scaling factor to apply to the height.</param>
    public void ScaleYFromCenter(float scale) {
        var posY = Position.Y + Extent.Y / 2.0f - (Extent.Y / 2.0f * scale);
        Position = new Vector2(Position.X, posY);
        Extent = new Vector2(Extent.X, Extent.Y * scale);
    }

    /// <summary>
    /// Scales the shape non-uniformly, keeping the center of the shape fixed.
    /// </summary>
    /// <param name="scaling">A vector containing the scaling factors for each dimension.</param>
    public void ScaleFromCenter(Vector2 scaling) {
        Position = Position + Extent / 2.0f - (Extent / 2.0f * scaling);
        Extent *= scaling; 
    }
    
    /// <summary>
    /// Scales the shape uniformly in both dimensions, keeping the center of the shape fixed.
    /// </summary>
    /// <param name="scale">The scaling factor to apply to both dimensions.</param>
    public void ScaleFromCenter(float scale) {
        ScaleFromCenter(new Vector2(scale, scale));
    }

    /// <summary>
    /// Moves the shape by the specified vector. This method can be overridden 
    /// by derived classes to implement custom movement behavior.
    /// </summary>
    public virtual void Move() {}

    /// <summary>
    /// Moves the shape by the specified vector.
    /// </summary>
    /// <param name="mover">The vector by which to move the shape.</param>
    public void Move(Vector2 mover) {
        Position += mover;
    }

    /// <summary>
    /// Moves the shape along the X-axis by the specified amount.
    /// </summary>
    /// <param name="move">The amount to move along the X-axis.</param>
    public void MoveX(float move) {
        Move(new Vector2(move, 0));
    }

    /// <summary>
    /// Moves the shape along the Y-axis by the specified amount.
    /// </summary>
    /// <param name="move">The amount to move along the Y-axis.</param>
    public void MoveY(float move) {
        Move(new Vector2(0, move));
    }

    /// <summary>
    /// Moves the shape by the specified amounts along the X and Y axes.
    /// </summary>
    /// <param name="x">The amount to move along the X-axis.</param>
    /// <param name="y">The amount to move along the Y-axis.</param>
    public void Move(float x, float y) {
        Move(new Vector2(x, y));
    }

    /// <summary>
    /// Rotates the shape by the specified angle in radians. Currently does not work.
    /// </summary>
    /// <param name="angleRadians">The angle in radians by which to rotate the shape.</param>
    /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
    public void Rotate(float angleRadians) {
        Rotation += angleRadians;
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sets the shape's rotation to the specified angle in radians. Currently does not work.
    /// </summary>
    /// <param name="angleRadians">The angle in radians to set the rotation to.</param>
    /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
    public void SetRotation(float angleRadians) {
        Rotation = angleRadians;
        throw new NotImplementedException();
    }
}
