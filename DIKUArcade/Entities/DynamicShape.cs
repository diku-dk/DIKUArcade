using System.Numerics;

namespace DIKUArcade.Entities {
    public class DynamicShape : Shape {
        /// <summary>
        /// Only dynamic entities carry a velocity vector.
        /// </summary>
        public Vector2 Velocity;

        public DynamicShape(float posX, float posY, float width, float height) {
            Position = new Vector2(posX, posY);
            Velocity = new Vector2();
            Extent = new Vector2(width, height);
        }

        public DynamicShape(float posX, float posY, float width, float height,
            float dirX, float dirY) : this(posX, posY, width, height) {
            Velocity.X = dirX;
            Velocity.Y = dirY;
        }

        public DynamicShape(Vector2 pos, Vector2 extent) {
            Position = pos;
            Extent = extent;
            Velocity = new Vector2(0f, 0f); // init 0 to avoid problems
        }

        public DynamicShape(Vector2 pos, Vector2 extent, Vector2 dir) {
            Position = pos;
            Extent = extent;
            Velocity = dir;
        }

        public void ChangeVelocity(Vector2 dir) {
            Velocity = dir;
        }


        /// <summary>
        /// Overrides the default Shape.Move() method to add
        /// this object's velocity to its position.
        /// </summary>
        public override void Move() {
            Position += Velocity;
        }

        public static explicit operator StationaryShape(DynamicShape obj) {
            return new StationaryShape(obj.Position, obj.Extent);
        }
    }
}