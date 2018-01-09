using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    public class Entity {
        private bool markedForDeletion;

        /// <summary>
        /// Entity's rotational angle measured in radians.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Basic Entity properties
        /// </summary>
        public Vec2F Position;
        public Vec2F Extent { get; set; }

        public void DeleteEntity() {
            markedForDeletion = true;
        }

        public bool IsDeleted() {
            return markedForDeletion;
        }

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

        /// <summary>
        /// Default Move method which does nothing.
        /// </summary>
        public virtual void Move() {
            return;
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

        public void Move(float x, float y) {
            this.MoveX(x);
            this.MoveY(y);
        }

        public void Rotate(float angleRadians) {
            Rotation += angleRadians;
        }

        public void SetRotation(float angleRadians) {
            Rotation = angleRadians;
        }

        public void MoveToPosition(Vec2F newPosition) {
            Position = newPosition;
        }
    }
}