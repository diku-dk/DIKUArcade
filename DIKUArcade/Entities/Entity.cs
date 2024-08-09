namespace DIKUArcade.Entities;

using DIKUArcade.Graphics;
using DIKUArcade.GUI;

/// <summary>
/// Represents an entity within the game world, which consists of a shape and 
/// an associated image. The entity can be rendered on the screen and marked 
/// for deletion.
/// </summary>
public class Entity {
    /// <summary>
    /// Gets or sets the shape of the entity, which defines its position, size, 
    /// and possibly other geometric properties.
    /// </summary>
    public Shape Shape { get; set; }

    /// <summary>
    /// Gets or sets the image associated with the entity, which defines 
    /// how the entity is visually represented on the screen.
    /// </summary>
    public IBaseImage Image { get; set; }

    /// <summary>
    /// A private field that indicates whether the entity has been marked 
    /// for deletion. This is used internally by the entity management system.
    /// </summary>
    private bool isDeleted;

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class 
    /// with the specified shape and image.
    /// </summary>
    /// <param name="shape">The shape that defines the entity's geometry.</param>
    /// <param name="image">The image that visually represents the entity.</param>
    public Entity(Shape shape, IBaseImage image) {
        isDeleted = false;
        Shape = shape;
        Image = image;
    }

    /// <summary>
    /// Marks the entity as ready for deletion. This method is typically 
    /// used by the <see cref="EntityContainer"/> class to manage entities 
    /// that are no longer needed.
    /// </summary>
    public void DeleteEntity() {
        isDeleted = true;
    }

    /// <summary>
    /// Checks if the entity has been marked for deletion.
    /// </summary>
    /// <returns>Returns <c>true</c> if the entity has been marked for 
    /// deletion; otherwise, <c>false</c>.</returns>
    public bool IsDeleted() {
        return isDeleted;
    }

    /// <summary>
    /// Renders the entity on the screen within the given context. This method 
    /// uses the entity's shape and image to draw it.
    /// </summary>
    /// <param name="context">The rendering context which provides 
    /// information about the window and graphics settings.</param>
    public void RenderEntity(WindowContext context) {
        Image.Render(context, Shape);
    }
}
