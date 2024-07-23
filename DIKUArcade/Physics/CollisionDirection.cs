namespace DIKUArcade.Physics;

/// <summary>
/// Velocity of a collision for the swept AABB collision detection algorithm.
/// Since this algorithm is axis-aligned, only four directions are needed.
/// </summary>
public enum CollisionDirection {
    CollisionDirUnchecked,
    CollisionDirUp,
    CollisionDirDown,
    CollisionDirLeft,
    CollisionDirRight
}
