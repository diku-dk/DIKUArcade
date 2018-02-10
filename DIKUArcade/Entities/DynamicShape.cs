using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    public class DynamicShape : Shape {
        /// <summary>
        /// Only dynamic entities carry a direction vector.
        /// </summary>
        public Vec2F Direction;

        public DynamicShape(float posX, float posY, float width, float height) {
            Position = new Vec2F(posX, posY);
            Direction = new Vec2F();
            Extent = new Vec2F(width, height);
        }

        public DynamicShape(float posX, float posY, float width, float height,
            float dirX, float dirY) : this(posX, posY, width, height) {
            Direction.X = dirX;
            Direction.Y = dirY;
        }

        public DynamicShape(Vec2F pos, Vec2F extent) {
            Position = pos;
            Extent = extent;
            Direction = new Vec2F();
        }

        public DynamicShape(Vec2F pos, Vec2F extent, Vec2F dir) {
            Position = pos;
            Extent = extent;
            Direction = dir;
        }

        public void ChangeDirection(Vec2F dir) {
            Direction = dir;
        }


        /// <summary>
        /// Overrides the default Shape.Move() method to add
        /// this object's direction to its position.
        /// </summary>
        public override void Move() {
            Position += Direction;
        }

        public static explicit operator StationaryShape(DynamicShape obj) {
            return new StationaryShape(obj.Position, obj.Extent);
        }
    }
}