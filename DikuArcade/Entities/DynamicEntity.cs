using System.Runtime.CompilerServices;
using DikuArcade.Math;

namespace DikuArcade.Entities
{
    public class DynamicEntity : Entity
    {
        // TODO: Should width and height be given a texture (image) level instead?
        public DynamicEntity(int posX, int posY, int width, int height)
        {
            Position = new Vec2f(posX, posY);
            Direction = new Vec2f();
            Extent = new Vec2f();
        }

        public DynamicEntity(Vec2f pos, Vec2f extent)
        {
            Position = pos;
            Extent = extent;
            Direction = new Vec2f();
        }
        
        public DynamicEntity(Vec2f pos, Vec2f extent, Vec2f dir)
        {
            Position = pos;
            Extent = extent;
            Direction = dir;
        }
        
        public static explicit operator StaticEntity(DynamicEntity obj)
        {
            return new StaticEntity(obj.Position, obj.Extent);
        }
    }
}