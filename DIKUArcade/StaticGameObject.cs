using DIKUArcade.Math;

namespace DIKUArcade
{
    /// <summary>
    /// Similar to GameObject, but does not contain direction information,
    /// since a static object os not meant to be affected by game physics.
    /// </summary>
    public class StaticGameObject
    {
        /// <summary>
        /// TODO: Should we use float values here instead?
        /// TODO: Should Extent be renamed to "Size"?
        /// </summary>
        public Vec2i Position;
        public Vec2i Extent;
        
        /// <summary>
        /// Angle measured in radians
        /// </summary>
        public float Angle;
        
        // TODO: Should width and height be given a texture (image) level instead?
        public StaticGameObject(int posX, int posY, int width, int height)
        {
            Position = new Vec2i(posX, posY);
            Extent = new Vec2i(0,0);
        }

        public StaticGameObject(Vec2i pos, Vec2i extent)
        {
            Position = pos;
            Extent = extent;
        }
        
        public static explicit operator GameObject(StaticGameObject sta)
        {
            return new GameObject(sta.Position, sta.Extent);
        }
    }
}
