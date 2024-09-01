namespace DIKUArcade.Graphics;

using DIKUArcade.Entities;
using DIKUArcade.GUI;

/// <summary>
/// Defines the contract for classes that represent an image to be rendered in the game world.
/// Implementations of this interface are responsible for rendering the image within a specified window context.
/// </summary>
public interface IBaseImage {

    /// <summary>
    /// Renders the image in the specified window context using the given shape.
    /// </summary>
    /// <param name="ctx">
    /// The context of the window in which the image will be rendered. This provides access to rendering functionalities.
    /// </param>
    /// <param name="shape">
    /// The shape that defines the position and dimensions of the image within the window. This shape is used to properly render the image.
    /// </param>
    void Render(WindowContext ctx, Shape shape);
}
