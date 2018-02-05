namespace DIKUArcade.Physics {

    /// <summary>
    /// Direction of a collision for the swept AABB collision detection algorithm.
    /// Since this algorithm is axis-aligned, only four directions are needed.
    /// </summary>
    public enum CollisionDirection {
        CollisionDirUnchecked,
        CollisionDirUp,
        CollisionDirDown,
        CollisionDirLeft,
        CollisionDirRight
    }
}