namespace DIKUArcade.Physics;

using System.Numerics;

public class CollisionData {
    /// <summary>
    /// Indicating whether or not a collision has occured.
    /// </summary>
    public bool Collision { get; set; }

    /// <summary>
    /// This factor should be multiplied onto the actor shape's
    /// velocity vector to get the closest position to the
    /// incident object.
    /// </summary>
    public Vector2 VelocityFactor { get; set; }

    /// <summary>
    /// The surface normal of the incident object, indicating
    /// from which direction a collision has occured.
    /// </summary>
    public CollisionDirection CollisionDir { get; set; } // might sometimes be useful!
}
