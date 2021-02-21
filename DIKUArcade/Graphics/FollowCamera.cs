using DIKUArcade.Entities;
using DIKUArcade.Math;
namespace DIKUArcade.Graphics {

    public class FollowCamera : Camera {
        public Shape WorldShape;
        
        private Vec2F baseOffset = new Vec2F(0.5f, 0.5f);
        public FollowCamera(StationaryShape worldShape) { 
            Offset = new Vec2F(0f, 0f);
            Scale = 1f;
            WorldShape = worldShape;
        }

        public void OffsetRelativeTo(Vec2F offsetRelativeTo) {
            Offset = baseOffset - offsetRelativeTo;
            if (-Offset.X      < WorldShape.Position.X)                            { Offset.X = -WorldShape.Position.X; }
            if (-Offset.X + 1f > WorldShape.Position.X + WorldShape.Extent.X) { Offset.X = -(WorldShape.Position.X + WorldShape.Extent.X - 1f); }
            if (-Offset.Y      < WorldShape.Position.Y)                            { Offset.Y = -WorldShape.Position.Y; }
            if (-Offset.Y + 1f > WorldShape.Position.Y + WorldShape.Extent.Y) { Offset.Y = -(WorldShape.Position.Y + WorldShape.Extent.Y - 1.0f); }
        }
    }
}