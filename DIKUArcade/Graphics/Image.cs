namespace DIKUArcade.Graphics;

using System;
using System.IO;
using System.Reflection;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

/// <summary>
/// Represents an image to be rendered on the screen. The image is associated with a texture
/// and can be rendered within the specified window context using a shape to define its position
/// and dimensions.
/// </summary>
public class Image : IBaseImage {

    /// <summary>
    /// Gets the texture associated with this image.
    /// </summary>
    public Texture Texture { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Image"/> class using an existing <see cref="Texture"/>.
    /// </summary>
    /// <param name="texture">
    /// The texture to be used for rendering the image.
    /// </param>
    public Image(Texture texture) {
        Texture = texture;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Image"/>/>.
    /// </summary>
    /// <param name="manifestResourceName">
    /// The name of the embedded resource to be used as a texture.
    /// </param>
    public Image(string manifestResourceName) {
        var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(manifestResourceName);
        
        if (stream is null) {
            throw new Exception($"Resouce with name {manifestResourceName} does not exists. Make" +
             "sure the name is correct or you have remebered to embed the file using the .csproj" +
             "file.");
        }

        byte[] buffer = new byte[stream.Length];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(buffer, 0, bytesRead);
        Texture = new Texture(readOnlySpan);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Image"/> using a ReadOnlySpan<byte>/>.
    /// </summary>
    /// <param name="bytes">
    /// The bytes to be used as a texture.
    /// </param>
    public Image(ReadOnlySpan<byte> readOnlySpan) {
        Texture = new Texture(readOnlySpan);
    }

    /// <summary>
    /// Renders the image in the specified window context using the given shape.
    /// </summary>
    /// <param name="context">
    /// The context of the window in which the image will be rendered. Provides access to rendering functionalities.
    /// </param>
    /// <param name="shape">
    /// The shape that defines the position and dimensions of the image within the window. This shape is used to properly render the image.
    /// </param>
    public void Render(WindowContext context, Shape shape) {
        var extent = context.Camera.WindowExtent(shape);
        var position = context.Camera.WindowPosition(shape, extent);
        Texture.Render(
            context,
            (int) MathF.Round(position.X, MidpointRounding.AwayFromZero),
            (int) MathF.Round(position.Y, MidpointRounding.AwayFromZero),
            (int) MathF.Ceiling(extent.X) + 1,
            (int) MathF.Ceiling(extent.Y) + 1
        );
    }
}
