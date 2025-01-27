namespace DIKUArcade.Physics {

    /// <summary>
    /// Represents the direction of a collision in the swept axis-aligned bounding box (AABB) collision detection algorithm.
    /// The algorithm assumes axis-aligned objects, so only the four cardinal directions are necessary.
    /// </summary>
    public enum CollisionDirection {
        /// <summary>
        /// Indicates an undefined or unchecked collision direction.
        /// This is typically used as a default value before a specific collision direction is determined.
        /// </summary>
        CollisionDirUnchecked,

        /// <summary>
        /// Indicates a collision direction upwards.
        /// This direction is used when the collision occurs from below the object.
        /// </summary>
        CollisionDirUp,

        /// <summary>
        /// Indicates a collision direction downwards.
        /// This direction is used when the collision occurs from above the object.
        /// </summary>
        CollisionDirDown,

        /// <summary>
        /// Indicates a collision direction to the left.
        /// This direction is used when the collision occurs from the right side of the object.
        /// </summary>
        CollisionDirLeft,

        /// <summary>
        /// Indicates a collision direction to the right.
        /// This direction is used when the collision occurs from the left side of the object.
        /// </summary>
        CollisionDirRight
    }
}
