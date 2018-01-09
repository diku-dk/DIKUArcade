using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    public class DynamicEntity : Entity {
        /// <summary>
        /// Only dynamic entities carry a direction vector.
        /// </summary>
        public Vec2F Direction;

        public DynamicEntity(int posX, int posY, int width, int height) {
            Position = new Vec2F(posX, posY);
            Direction = new Vec2F();
            Extent = new Vec2F();
        }

        public DynamicEntity(Vec2F pos, Vec2F extent) {
            Position = pos;
            Extent = extent;
            Direction = new Vec2F();
        }

        public DynamicEntity(Vec2F pos, Vec2F extent, Vec2F dir) {
            Position = pos;
            Extent = extent;
            Direction = dir;
        }

        // TODO: Think better about how this could be done!
        public void ChangeDirection(Vec2F dir) {
            this.Direction = dir;
        }


        /// <summary>
        /// Overrides the default Entity.Move() method to add
        /// this object's direction to its position.
        /// </summary>
        public override void Move() {
            this.Position += this.Direction;
        }

        public static explicit operator StationaryEntity(DynamicEntity obj) {
            return new StationaryEntity(obj.Position, obj.Extent);
        }
    }
}