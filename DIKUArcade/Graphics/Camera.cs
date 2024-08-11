namespace DIKUArcade.Graphics;

using System.Numerics;
using DIKUArcade.Entities;

/// <summary>
/// Represents a camera that controls the view of the game world, including its position, scale, and viewport dimensions.
/// It handles conversions between world coordinates and window coordinates, and adjusts object sizes for rendering within the viewport.
/// </summary>
public class Camera {

    private Vector2 offset = Vector2.Zero;
    private Vector2 scale = Vector2.Zero;
    private Vector2 halfScale = Vector2.Zero;
    private Vector2 scalePlusOne = Vector2.One;
    private Vector2 halfScaleMinusOffset = Vector2.Zero;
    private Vector2 windowHalfScaleMinusOffset = Vector2.Zero;
    private Vector2 windowScalePlusOne = Vector2.Zero;

    /// <summary>
    /// Gets or sets the offset of the camera in world coordinates.
    /// Setting this property updates derived values such as <see cref="halfScaleMinusOffset"/> and <see cref="windowHalfScaleMinusOffset"/>.
    /// </summary>
    public Vector2 Offset {
        get => offset;
        set {
            offset = value;
            halfScaleMinusOffset = halfScale - offset;
            windowHalfScaleMinusOffset = WindowVector * halfScaleMinusOffset;
        }
    }

    /// <summary>
    /// Gets or sets the scaling factor of the camera.
    /// Setting this property updates derived values such as <see cref="halfScale"/>, <see cref="scalePlusOne"/>, <see cref="halfScaleMinusOffset"/>, <see cref="windowHalfScaleMinusOffset"/>, and <see cref="windowScalePlusOne"/>.
    /// </summary>
    public Vector2 Scale {
        get => scale;
        set {
            scale = value;
            halfScale = scale / 2.0f;
            scalePlusOne = scale + Vector2.One;
            halfScaleMinusOffset = halfScale - offset;
            windowHalfScaleMinusOffset = WindowVector * halfScaleMinusOffset;
            windowScalePlusOne = WindowVector * scalePlusOne;
        }
    }

    private int width = 0;
    private int height = 0;

    /// <summary>
    /// Gets or sets the width of the camera's viewport.
    /// Setting this property updates derived values such as <see cref="windowHalfScaleMinusOffset"/> and <see cref="windowScalePlusOne"/>.
    /// </summary>
    public int Width {
        get => width;
        set {
            width = value;
            windowHalfScaleMinusOffset = WindowVector * halfScaleMinusOffset;
            windowScalePlusOne = WindowVector * scalePlusOne;
        }
    }

    /// <summary>
    /// Gets or sets the height of the camera's viewport.
    /// Setting this property updates derived values such as <see cref="windowHalfScaleMinusOffset"/> and <see cref="windowScalePlusOne"/>.
    /// </summary>
    public int Height {
        get => height;
        set {
            height = value;
            windowHalfScaleMinusOffset = WindowVector * halfScaleMinusOffset;
            windowScalePlusOne = WindowVector * scalePlusOne;
        }
    }

    /// <summary>
    /// Gets the size of the camera's viewport as a <see cref="Vector2"/>.
    /// </summary>
    public Vector2 WindowVector => new Vector2(width, height);

    /// <summary>
    /// Initializes a new instance of the <see cref="Camera"/> class with specified offset, scale, width, and height.
    /// </summary>
    /// <param name="offset">The offset of the camera in world coordinates.</param>
    /// <param name="scale">The scaling factor of the camera.</param>
    /// <param name="width">The width of the camera's viewport.</param>
    /// <param name="height">The height of the camera's viewport.</param>
    public Camera(Vector2 offset, Vector2 scale, int width, int height) {
        Offset = offset;
        Scale = scale;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Camera"/> class with specified width and height.
    /// The offset and scale are set to default values.
    /// </summary>
    /// <param name="width">The width of the camera's viewport.</param>
    /// <param name="height">The height of the camera's viewport.</param>
    public Camera(int width, int height) {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Converts world coordinates to window coordinates based on the camera's offset and scale.
    /// </summary>
    /// <param name="position">The world position to convert.</param>
    /// <param name="windowExtent">The size of the object in window coordinates.</param>
    /// <returns>The position in window coordinates.</returns>
    public Vector2 WindowPosition(Vector2 position, Vector2 windowExtent) {
        var newPosition = position * windowScalePlusOne - windowHalfScaleMinusOffset; 
        return new Vector2(newPosition.X, Height - (newPosition.Y + windowExtent.Y));
    }

    /// <summary>
    /// Converts the position of a <see cref="Shape"/> to window coordinates based on the camera's offset and scale.
    /// </summary>
    /// <param name="shape">The shape to convert.</param>
    /// <param name="windowExtent">The size of the shape in window coordinates.</param>
    /// <returns>The position of the shape in window coordinates.</returns>
    public Vector2 WindowPosition(Shape shape, Vector2 windowExtent) {
        return WindowPosition(shape.Position, windowExtent);
    }

    /// <summary>
    /// Converts the extent (size) of an object from world coordinates to window coordinates based on the camera's scale.
    /// </summary>
    /// <param name="extent">The size of the object in world coordinates.</param>
    /// <returns>The size of the object in window coordinates.</returns>
    public Vector2 WindowExtent(Vector2 extent) {
        var v = extent * windowScalePlusOne;
        var x = (int) v.X;
        var y = (int) v.Y;
        return new Vector2(x + x % 2, y + y % 2); 
    }

    /// <summary>
    /// Converts the extent (size) of a <see cref="Shape"/> from world coordinates to window coordinates based on the camera's scale.
    /// </summary>
    /// <param name="shape">The shape to convert.</param>
    /// <returns>The size of the shape in window coordinates.</returns>
    public Vector2 WindowExtent(Shape shape) {
        return WindowExtent(shape.Extent);
    }

    /// <summary>
    /// Computes the transformation matrix for a <see cref="Shape"/> to be rendered correctly in the window,
    /// considering its original extent and the camera's offset and scale.
    /// </summary>
    /// <param name="shape">The shape to compute the matrix for.</param>
    /// <param name="originalExtent">The original size of the shape in world coordinates.</param>
    /// <returns>A <see cref="Matrix3x2"/> representing the transformation needed to render the shape correctly in the window.</returns>
    public Matrix3x2 WindowMatrix(Shape shape, Vector2 originalExtent) {
        var windowExtent = WindowExtent(shape);
        var windowExtentScaling = windowExtent / originalExtent;
        var windowPosition = WindowPosition(shape, windowExtent);
        return new Matrix3x2(
            windowExtentScaling.X, 0,
            0, windowExtentScaling.Y,
            windowPosition.X, windowPosition.Y
        );
    }

    public Matrix3x2 WindowMatrix(Vector2 position, Vector2 extent, Vector2 originalExtent) {
        var windowExtent = WindowExtent(extent);
        var windowExtentScaling = windowExtent / originalExtent;
        var windowPosition = WindowPosition(position, windowExtent);
        return new Matrix3x2(
            windowExtentScaling.X, 0,
            0, windowExtentScaling.Y,
            windowPosition.X, windowPosition.Y
        );
    }

}
