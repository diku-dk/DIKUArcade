using DIKUArcade.Entities;
using DIKUArcade.Math;

using System.Collections.Generic;
namespace DIKUArcade.Graphics {

/// <summary>A camera that takes a direction </summary>
    public class ChaseCamera : Camera {
        public Shape WorldShape;
        
        public DynamicShape cameraShape;
        // We want to expose the camera position, as it is quite nice to know what we are looking at.
        //public Vec2F CameraPos() { return cameraShape.Position; }
       
        private Vec2F baseOffset = new Vec2F(0.5f, 0.5f);


        private Queue<Vec2F> directionQueue;
        // Set the frame delay of the camera  
        private const int CAMERA_DELAY = 20;      
      
      
        public ChaseCamera(StationaryShape worldShape) { 
            
            cameraShape = new DynamicShape(0.5f, 0.5f, 0.0f, 0.0f);
            Offset = baseOffset - cameraShape.Position;//new Vec2F(0f, 0f);
            Scale = 1f;

            WorldShape = worldShape;
             // Initialize the queue and fill it with 0-vectors to make the camera lag behind by CAMERA_DELAY seconds
            directionQueue = new Queue<Vec2F>(CAMERA_DELAY);
            for (int i = 0; i < CAMERA_DELAY; i++) { directionQueue.Enqueue(new Vec2F(0f,0f)); } 
        }
       
        public void EnqueueDirection(Vec2F direction) {
            cameraShape.Direction = directionQueue.Dequeue();
            directionQueue.Enqueue(direction);
            
            cameraShape.Move();
            
            // Update camera offset and clamp it to the worldshape
            Offset = baseOffset - cameraShape.Position;
            if (-Offset.X      < WorldShape.Position.X)                       { Offset.X = -WorldShape.Position.X; }
            if (-Offset.X + 1f > WorldShape.Position.X + WorldShape.Extent.X) { Offset.X = -(WorldShape.Position.X + WorldShape.Extent.X - 1f); }
            if (-Offset.Y      < WorldShape.Position.Y)                       { Offset.Y = -WorldShape.Position.Y; }
            if (-Offset.Y + 1f > WorldShape.Position.Y + WorldShape.Extent.Y) { Offset.Y = -(WorldShape.Position.Y + WorldShape.Extent.Y - 1.0f); }
           
        }
    }
}