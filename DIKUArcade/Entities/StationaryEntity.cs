using DIKUArcade.Math;

namespace DIKUArcade.Entities {
    /// <summary>
    ///     Similar to DynamicEntity, but does not contain direction information,
    ///     since a static object os not meant to be affected by game physics.
    /// </summary>
    public class StationaryEntity : Entity {
        // TODO: Should width and height be given a texture (image) level instead?
        public StationaryEntity(int posX, int posY, int width, int height) {
            Position = new Vec2f(posX, posY);
            Extent = new Vec2f();
        }

        public StationaryEntity(Vec2f pos, Vec2f extent) {
            Position = pos;
            Extent = extent;
        }

        public static explicit operator DynamicEntity(StationaryEntity sta) {
            return new DynamicEntity(sta.Position, sta.Extent);
        }
    }
}