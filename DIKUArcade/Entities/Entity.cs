using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    public class Entity {
        /// <summary>
        ///     Used for EntityContainer.
        ///     TODO: Is this good software design principle?
        /// </summary>
        private bool markedForDeletion;

        /// <summary>
        ///     Angle measured in radians.
        ///     TODO: could be called Orientation?
        /// </summary>
        public float Angle;

        public Vec2F Direction; // TODO: move this elsewhere
        public Vec2F Extent;

        /// <summary>
        ///     Basic Entity properties
        ///     TODO: Could (should) they be made private?
        /// </summary>
        public Vec2F Position;

        public void DeleteEntity() {
            markedForDeletion = true;
        }

        public bool IsDeleted() {
            return markedForDeletion;
        }

        // TODO: Should all manipulation methods (scale,translate,etc) be moved to EntityActor class?
        public void Scale(Vec2F scalar) {
            // This is doing pairwise vector multiplication!
            Extent *= scalar;
        }

        public void ScaleX(float scale) {
            Extent.X *= scale;
        }

        public void ScaleY(float scale) {
            Extent.Y *= scale;
        }

        public void Move(Vec2F mover) {
            Position += mover;
        }

        public void MoveX(float move) {
            Position.X += move;
        }

        public void MoveY(float move) {
            Position.Y += move;
        }

        public void Rotate(float angleRadians) {
            Angle += angleRadians;
        }

        public void MoveToPosition(Vec2F newPosition) {
            Position = newPosition;
        }
    }
}