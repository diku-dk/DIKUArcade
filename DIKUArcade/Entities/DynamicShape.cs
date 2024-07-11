using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    public class DynamicShape : Shape {
        /// <summary>
        /// Only dynamic entities carry a velocity vector.
        /// </summary>
        public Vec2F Velocity;

        public DynamicShape(float posX, float posY, float width, float height) {
            Position = new Vec2F(posX, posY);
            Velocity = new Vec2F();
            Extent = new Vec2F(width, height);
        }

        public DynamicShape(float posX, float posY, float width, float height,
            float dirX, float dirY) : this(posX, posY, width, height) {
            Velocity.X = dirX;
            Velocity.Y = dirY;
        }

        public DynamicShape(Vec2F pos, Vec2F extent) {
            Position = pos;
            Extent = extent;
            Velocity = new Vec2F(0f, 0f); // init 0 to avoid problems
        }

        public DynamicShape(Vec2F pos, Vec2F extent, Vec2F dir) {
            Position = pos;
            Extent = extent;
            Velocity = dir;
        }

        public void ChangeVelocity(Vec2F dir) {
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