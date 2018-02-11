using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    public class Shape {
        /// <summary>
        /// Shape's rotational angle measured in radians.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Basic Shape properties
        /// </summary>
        public Vec2F Position;
        public Vec2F Extent { get; set; }

        public void Scale(float scale) {
            Extent *= scale;
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

        public void ScaleXFromCenter(float scale) {
            Position.X = (Position.X + Extent.X / 2.0f) - ((Extent.X / 2.0f) * scale);
            Extent.X *= scale;
        }

        public void ScaleYFromCenter(float scale) {
            Position.Y = (Position.Y + Extent.Y / 2.0f) - (Extent.Y / 2.0f * scale);
            Extent.Y *= scale;
        }

        public void ScaleFromCenter(float scale) {
            ScaleXFromCenter(scale);
            ScaleYFromCenter(scale);
        }

        public void ScaleFromCenter(Vec2F scalar) {
            ScaleXFromCenter(scalar.X);
            ScaleYFromCenter(scalar.Y);
        }

        /// <summary>
        /// Default Move method which does nothing.
        /// </summary>
        public virtual void Move() {}

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
            MoveX(x);
            MoveY(y);
        }

        public void Rotate(float angleRadians) {
            Rotation += angleRadians;
        }

        public void SetRotation(float angleRadians) {
            Rotation = angleRadians;
        }

        public void SetPosition(Vec2F newPosition) {
            Position = newPosition;
        }
    }
}