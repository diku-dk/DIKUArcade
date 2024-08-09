namespace DIKUArcade.Physics;

using System.Numerics;

public class CollisionData {
    /// <summary>
    /// Gets or sets a value indicating whether or not a collision has occurred.
    /// </summary>
    /// <value>
    /// <c>true</c> if a collision has occurred; otherwise, <c>false</c>.
    /// </value>
    public bool Collision { get; set; }

    /// <summary>
    /// Gets or sets the factor that should be multiplied onto the actor shape's
    /// velocity vector to determine the closest position to the incident object.
    /// This factor helps adjust the position of the actor to resolve the collision.
    /// </summary>
    /// <value>
    /// A <see cref="Vector2"/> representing the factor to adjust the velocity vector.
    /// </value>
    public Vector2 VelocityFactor { get; set; }

    /// <summary>
    /// Gets or sets the surface normal of the incident object, indicating
    /// the direction from which the collision occurred. This can be useful for
    /// determining the collision response or handling physics calculations.
    /// </summary>
    /// <value>
    /// A <see cref="CollisionDirection"/> representing the direction of the collision.
    /// </value>
    public CollisionDirection CollisionDir { get; set; } // might sometimes be useful!
}
