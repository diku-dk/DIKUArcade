using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    public class DynamicEntity : Entity {
        // TODO: Should width and height be given a texture (image) level instead?
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

        public static explicit operator StationaryEntity(DynamicEntity obj) {
            return new StationaryEntity(obj.Position, obj.Extent);
        }
    }
}