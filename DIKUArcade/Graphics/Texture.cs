namespace DIKUArcade.Graphics;

using System;
using System.IO;
using System.Numerics;
using DIKUArcade.GUI;

/// <summary>
/// Represents a texture that can be rendered onto a graphical context. The texture can be
/// initialized from image files, byte arrays, or specific image strides, and can be resized
/// dynamically before rendering.
/// </summary>
public class Texture {
    private readonly Lowlevel.Image originalImage;
    private Lowlevel.Image image;
    private int? prevWidth;
    private int? prevHeight;
    internal readonly int originalWidth;
    internal readonly int originalHeight;
    internal Vector2 originalExtent;

    /// <summary>
    /// Initializes a new instance of the <see cref="Texture"/> class from a byte array containing image data.
    /// </summary>
    /// <param name="bytes">
    /// A <see cref="ReadOnlySpan{byte}"/> containing the image data.
    /// </param>
    public Texture(ReadOnlySpan<byte> bytes) {
        originalImage = Lowlevel.createImage(bytes);
        image = originalImage;

        var (width, height) = Lowlevel.measureImage(originalImage);
        originalWidth = width;
        originalHeight = height;
        originalExtent = new Vector2(originalWidth, originalHeight);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Texture"/> class from an image file and
    /// extracts a specific stride from a sprite sheet.
    /// </summary>
    /// <param name="bytes">
    /// The bytes that makes up the image.
    /// </param>
    /// <param name="currentStride">
    /// The index of the stride (frame) to extract from the sprite sheet.
    /// </param>
    /// <param name="stridesInImage">
    /// The total number of strides (frames) in the sprite sheet.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the current stride index is out of range or when the total number of strides is invalid.
    /// </exception>
    public Texture(ReadOnlySpan<byte> bytes, int currentStride, int stridesInImage) {
        if (currentStride < 0 || currentStride >= stridesInImage || stridesInImage < 0) {
            throw new ArgumentOutOfRangeException(
                $"Invalid stride numbers: ({currentStride}/{stridesInImage})");
        }
        originalImage = Lowlevel.createImage(bytes);
        var (w, height) = Lowlevel.measureImage(originalImage);
        var width = w / stridesInImage;
        originalImage = Lowlevel.cropImage(originalImage, width * currentStride, 0, width, height);
        image = originalImage;

        originalWidth = width;
        originalHeight = height;
        originalExtent = new Vector2(originalWidth, originalHeight);
    }
    
    /// <summary>
    /// Renders the texture onto the specified graphical context at the given position and size.
    /// </summary>
    /// <param name="context">
    /// The <see cref="WindowContext"/> where the texture will be rendered.
    /// </param>
    /// <param name="x">
    /// The x-coordinate of the top-left corner of the rendering area.
    /// </param>
    /// <param name="y">
    /// The y-coordinate of the top-left corner of the rendering area.
    /// </param>
    /// <param name="width">
    /// The width of the rendering area.
    /// </param>
    /// <param name="height">
    /// The height of the rendering area.
    /// </param>
    public void Render(WindowContext context, int x, int y, int width, int height) {
        if (width != prevWidth || height != prevHeight) {
            image = Lowlevel.setSizeImage(originalImage, width, height);
            prevWidth = width;
            prevHeight = height;
        }

        Lowlevel.renderImage(x, y, image, context.LowlevelContext);
    }
}
