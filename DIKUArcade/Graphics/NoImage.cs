namespace DIKUArcade.Graphics;

using DIKUArcade.Entities;
using DIKUArcade.GUI;

/// <summary>
/// A stub implementation of the <see cref="IBaseImage"/> interface. This class is intended for use
/// with entities that do not require rendering, providing a no-operation implementation of the 
/// <see cref="Render"/> method.
/// </summary>
public class NoImage : IBaseImage {
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NoImage"/> class.
    /// </summary>
    public NoImage() {}

    /// <summary>
    /// A no-operation method for rendering. This method does nothing and is intended for use with
    /// entities that are non-drawable or where no image rendering is required.
    /// </summary>
    /// <param name="context">
    /// The <see cref="WindowContext"/> in which rendering would occur, though this method does not use it.
    /// </param>
    /// <param name="shape">
    /// The <see cref="Shape"/> to be rendered, though this method does not use it.
    /// </param>
    public void Render(WindowContext context, Shape shape) {}
}
