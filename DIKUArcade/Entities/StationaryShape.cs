using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    /// <summary>
    /// Similar to DynamicShape, but does not contain direction information,
    /// since a static object os not meant to be affected by game physics.
    /// </summary>
    public class StationaryShape : Shape {
        public StationaryShape(float posX, float posY, float width, float height) {
            Position = new Vec2F(posX, posY);
            Extent = new Vec2F(width, height);
        }

        public StationaryShape(Vec2F pos, Vec2F extent) {
            Position = pos;
            Extent = extent;
        }

        public static explicit operator DynamicShape(StationaryShape sta) {
            return new DynamicShape(sta.Position, sta.Extent);
        }
    }
}