using System.Numerics;

namespace DIKUArcade.Entities {
    /// <summary>
    /// Similar to DynamicShape, but does not contain velocity information,
    /// since a static object os not meant to be affected by game physics.
    /// </summary>
    public class StationaryShape : Shape {
        public StationaryShape(float posX, float posY, float width, float height) {
            Position = new Vector2(posX, posY);
            Extent = new Vector2(width, height);
        }

        public StationaryShape(Vector2 pos, Vector2 extent) {
            Position = pos;
            Extent = extent;
        }

        public static explicit operator DynamicShape(StationaryShape sta) {
            return new DynamicShape(sta.Position, sta.Extent);
        }
    }
}