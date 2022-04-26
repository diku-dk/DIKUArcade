using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace DIKUArcade.Physics;

// https://learnopengl.com/In-Practice/2D-Game/Collisions/Collision-detection
// Potentially allow for AABB with circles.
// Or use SAT.
public class CollisionDetection {
    public static CollisionData Aabb_C(DynamicShape actor, Shape shape) {
        throw new NotImplementedException("CollisionDetection.Aabb_C is not finished!");

        /*
        var data = new CollisionData {
            Collision = false,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUnchecked
        };

        var circRadius = shape.Extent.Y/2;
        var circCenter = new Vec2F(shape.Position.X + shape.Extent.X/2, shape.Position.Y + shape.Extent.Y/2);

        var staLowerLeft = new Vec2F(shape.Position.X, shape.Position.Y);
        var staUpperRight = new Vec2F(shape.Position.X + shape.Extent.X,
            shape.Position.Y + shape.Extent.Y);
        var staCenter = new Vec2F(shape.Position.X + shape.Extent.X/2, shape.Position.Y + shape.Extent.Y/2);

        var D = circCenter - staCenter;

        // Clamp D to width/2 height/2 and add it to staCenter

        return data;
        */
    }

    public static CollisionData Aabb(DynamicShape actor, Shape shape) {
        var data = new CollisionData {
            Collision = false,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUnchecked
        };

        var xIsNearZero = System.Math.Abs(actor.Direction.X) < 1e-6f;
        var yIsNearZero = System.Math.Abs(actor.Direction.Y) < 1e-6f;
        var xyIsNotNearZero = !xIsNearZero && !yIsNearZero;
        
        if (xIsNearZero && yIsNearZero) {
            return data;
        }

        var dynLowerLeft = actor.Position;
        var dynUpperRight = actor.Position + actor.Extent;

        var staLowerLeft = shape.Position;
        var staUpperRight = shape.Position + shape.Extent;

        var entryDistance = staUpperRight - dynLowerLeft;
        var exitDistance = staLowerLeft - dynUpperRight;

        if (actor.Direction.X > 0.0f) {
            (entryDistance.X, exitDistance.X) = (exitDistance.X, entryDistance.X); // Swap variables
        }

        if (actor.Direction.Y > 0.0f) {
            (entryDistance.Y, exitDistance.Y) = (exitDistance.Y, entryDistance.Y); // Swap variables
        }

        var entryTime = new Vec2F(entryDistance.X / actor.Direction.X,
                                  entryDistance.Y / actor.Direction.Y);
        var exitTime = new Vec2F(exitDistance.X / actor.Direction.X,
                                 exitDistance.Y / actor.Direction.Y);

        var entryTimeMax = System.Math.Max(entryTime.X, entryTime.Y);
        var exitTimeMin = System.Math.Min(exitTime.X, exitTime.Y);

        var xOverlaps = staUpperRight.X > dynLowerLeft.X && staLowerLeft.X < dynUpperRight.X;
        var yOverlaps = staUpperRight.Y > dynLowerLeft.Y && staLowerLeft.Y < dynUpperRight.Y;
        var xInBound = 0.0f <= entryTime.X && entryTime.X < 1.0f;
        var yInBound = 0.0f <= entryTime.Y && entryTime.Y < 1.0f;
        var xEntryIsMaxEntry = entryTime.X > entryTime.Y;
        var yBranch = xIsNearZero && entryTime.Y < exitTime.Y && yInBound && xOverlaps;
        var xBranch = yIsNearZero && entryTime.X < exitTime.X && xInBound && yOverlaps;
        var wentOver = entryTimeMax < exitTimeMin;
        var xyBranch = xyIsNotNearZero && wentOver && ((xInBound && entryTime.Y < 1.0f) || 
                                                       (yInBound && entryTime.X < 1.0f));

        // active movement in both x- and y-direction or just x.
        if (xBranch || (xEntryIsMaxEntry && xyBranch)) {
            data.DirectionFactor.X = yIsNearZero ? entryTime.X : entryTimeMax;
            data.CollisionDir = CollisionDirection.CollisionDirRight;
            if (actor.Direction.X < 0.0f) {
                data.CollisionDir = CollisionDirection.CollisionDirLeft;
            }
            data.Collision = true;
        // active movement in both x- and y-direction or just y.
        } else if (yBranch || (!xEntryIsMaxEntry && xyBranch)) {
            data.DirectionFactor.Y = xIsNearZero ? entryTime.Y : entryTimeMax;
            data.CollisionDir = CollisionDirection.CollisionDirUp;
            if (actor.Direction.Y < 0.0f) {
                data.CollisionDir = CollisionDirection.CollisionDirDown;
            }
            data.Collision = true;
        }
        return data;
    }
}
