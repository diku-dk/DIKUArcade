using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
namespace DIKUArcade.Graphics {

    public class DynamicCamera : Camera {
        public Shape WorldShape;

        private DynamicShape innerBounds;
        private Entity overlay;

        private Vec2F displacement;
        public DynamicCamera(StationaryShape worldShape) { 
            Offset = new Vec2F(0.0f, 0.0f);
            Scale = 1f;
            WorldShape = worldShape;
            innerBounds = new DynamicShape(0.25f, 0.25f, 0.5f, 0.5f);
            //innerBounds.ScaleFromCenter(0.5f);
            displacement = innerBounds.Position;
            overlay = new Entity(innerBounds, new Image(Path.Combine("Assets", "Images", "Overlay.png")));

            System.Console.WriteLine("Offset is: {0}", Offset);
            System.Console.WriteLine("inner is: {0}", innerBounds.Position);
        }

        public void OffsetRelativeTo(Vec2F offsetRelativeTo) {
            // Check if "safe" inside the inner bounds and exit early
            // The magic constant 0.03f is the players width and height
            if (innerBounds.Position.X <= offsetRelativeTo.X 
                && offsetRelativeTo.X <= innerBounds.Position.X + innerBounds.Extent.X - 0.03f
                && innerBounds.Position.Y <= offsetRelativeTo.Y 
                && offsetRelativeTo.Y <= innerBounds.Position.Y + innerBounds.Extent.Y - 0.03f) {
                    //innerBounds.Direction = new Vec2F(0.0f, 0.0f);
                    return;
                }
            else {
                // Calculate new offset
                // If the player has driven out the left side of the box
                if (offsetRelativeTo.X < innerBounds.Position.X)
                {
                    innerBounds.Position.X = offsetRelativeTo.X;
                    //innerBounds.Direction.X = offsetRelativeTo.X - innerBounds.Position.X; 
                }
                else if (offsetRelativeTo.X > innerBounds.Position.X + innerBounds.Extent.X - 0.03f) {
                    innerBounds.Position.X = (offsetRelativeTo.X - innerBounds.Extent.X - 0.03f);
                    //innerBounds.Direction.X = (offsetRelativeTo.X - (innerBounds.Position.X + innerBounds.Extent.X -0.03f));
                }
                //innerBounds.Direction *= 3.0f;
                //innerBounds.Move();

                Offset = displacement - innerBounds.Position;
                // Then check if outside the world and move stuff back                                     
                // Stop at the edge of the world
                if (-Offset.X < WorldShape.Position.X) { // Left side
                        Offset.X = -WorldShape.Position.X;
                        innerBounds.Position.X = offsetRelativeTo.X;//WorldShape.Position.X + innerBounds.Extent.X / 2.0f; 
                    }
                if (-Offset.X + 1f > WorldShape.Position.X + WorldShape.Extent.X) { // Right side
                     Offset.X = -(WorldShape.Position.X + WorldShape.Extent.X - 1f);
                     innerBounds.Position.X = offsetRelativeTo.X - 0.03f;//WorldShape.Extent.X - innerBounds.Extent.X; 
                    } 
                if (-Offset.Y      < WorldShape.Position.Y) // Top
                    { Offset.Y = -WorldShape.Position.Y;
                    innerBounds.Position.Y = offsetRelativeTo.Y; }
                if (-Offset.Y + 1f > WorldShape.Position.Y + WorldShape.Extent.Y) // Bottom
                    { 
                        Offset.Y = -(WorldShape.Position.Y + WorldShape.Extent.Y - 1.0f); 
                        innerBounds.Position.Y = offsetRelativeTo.Y - 0.03f;
                    }
             
            }
            
        }

        public void Render() {
            overlay.Image.Render(overlay.Shape, this);
        }
    }
}