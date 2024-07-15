using System;
using System.Numerics;
using System.Threading;

namespace DIKUArcade.Entities {
    public class Shape {
        /// <summary>
        /// Shape's rotational angle measured in radians.
        /// </summary>
        public float Rotation { get; set; }
        
        /// <summary>
        /// Basic Shape properties
        /// </summary>
        public Vector2 Position { get; set; }
        public Vector2 Extent { get; set; }

        /// <summary>
        /// Performs a downcast on this Shape instance to a
        /// DynamicShape. If the downcast fails, a new
        /// DynamicShape is returned instead with this Shape's
        /// Position and Extent properties, and a default (0,0)
        /// Velocity vector.
        /// </summary>
        /// <returns></returns>
        public DynamicShape AsDynamicShape() {
            var shape = this as DynamicShape;
            return shape ?? new DynamicShape(Position, Extent);
        }

        /// <summary>
        /// Performs a downcast on this Shape instance to a
        /// StationaryShape. If the downcast fails, a new
        /// StationaryShape is returned instead with this Shape's
        /// Position and Extent properties.
        /// </summary>
        /// <returns></returns>
        public StationaryShape AsStationaryShape() {
            var sta = this as StationaryShape;
            return sta ?? new StationaryShape(Position, Extent);
        }

        // Do not reference other shapes if you intend to scale.
        // Use .Copy() or you might scale everything.
        public void Scale(float scale) {
            Extent *= scale;
        }

        public void Scale(Vector2 scalar) {
            // This is doing pairwise vector multiplication!
            Extent *= scalar;
        }
        
        public void ScaleX(float scale) {
            Extent = new Vector2(Extent.X * scale, Extent.Y);
        }

        public void ScaleY(float scale) {
            Extent = new Vector2(Extent.X, Extent.Y * scale);
        }

        public void ScaleXFromCenter(float scale) {
            var posX = Position.X + Extent.X / 2.0f - (Extent.X / 2.0f * scale);
            Position = new Vector2(posX, Position.Y);
            Extent = new Vector2(Extent.X * scale, Extent.Y);
        }

        public void ScaleYFromCenter(float scale) {
            var posY = Position.Y + Extent.Y / 2.0f - (Extent.Y / 2.0f * scale);
            Position = new Vector2(Position.X, posY);
            Extent = new Vector2(Extent.X, Extent.Y * scale);
        }

        public void ScaleFromCenter(Vector2 scaling) {
            Position = Position + Extent / 2.0f - (Extent / 2.0f * scaling);
            Extent *= scaling; 
        }
        

        public void ScaleFromCenter(float scale) {
            ScaleFromCenter(new Vector2(scale, scale));
        }

        /// <summary>
        /// Default Move method which does nothing.
        /// </summary>
        public virtual void Move() {}

        public void Move(Vector2 mover) {
            Position += mover;
        }

        public void MoveX(float move) {
            Move(new Vector2(move, 0));
        }

        public void MoveY(float move) {
            Move(new Vector2(0, move));
        }

        public void Move(float x, float y) {
            Move(new Vector2(x, y));
        }

        public void Rotate(float angleRadians) {
            Rotation += angleRadians;
            throw new NotImplementedException();
        }

        public void SetRotation(float angleRadians) {
            Rotation = angleRadians;
            throw new NotImplementedException();
        }

        public void SetPosition(Vector2 newPosition) {
            Position = newPosition;
            throw new NotImplementedException();
        }
    }
}