namespace DIKUArcade.Physics;

using System;
using DIKUArcade.Entities;
using System.Numerics;

/// <summary>
/// Provides methods for detecting collisions between various shapes.
/// This class includes methods for detecting collisions between axis-aligned
/// bounding boxes (AABBs) and between an AABB and a circle. It utilizes various
/// algorithms to determine if and how two shapes intersect and provides information
/// about the collision, including the direction and impact factor.
/// </summary>
public class CollisionDetection {
    /// <summary>
    /// Detects collision between an axis-aligned bounding box (AABB) and a circle.
    /// This method is currently not implemented and will throw a 
    /// <see cref="NotImplementedException"/> if called.
    /// </summary>
    /// <param name="actor">The dynamic shape representing the moving object.</param>
    /// <param name="shape">The static shape representing the stationary object.</param>
    /// <returns>A <see cref="CollisionData"/> object containing collision information.</returns>
    /// <exception cref="NotImplementedException">Thrown if this method is not implemented.</exception>
    public static CollisionData Aabb_C(DynamicShape actor, Shape shape) {
        throw new NotImplementedException("CollisionDetection.Aabb_C is not finished!");

        /*
        var data = new CollisionData {
            Collision = false,
            VelocityFactor = new Vector2(1.0f, 1.0f),
            CollisionDir = CollisionVelocity.CollisionDirUnchecked
        };

        var circRadius = shape.Extent.Y/2;
        var circCenter = new Vector2(shape.Position.X + shape.Extent.X/2, shape.Position.Y + shape.Extent.Y/2);

        var staLowerLeft = new Vector2(shape.Position.X, shape.Position.Y);
        var staUpperRight = new Vector2(shape.Position.X + shape.Extent.X,
            shape.Position.Y + shape.Extent.Y);
        var staCenter = new Vector2(shape.Position.X + shape.Extent.X/2, shape.Position.Y + shape.Extent.Y/2);

        var D = circCenter - staCenter;

        // Clamp D to width/2 height/2 and add it to staCenter

        return data;
        https://learnopengl.com/In-Practice/2D-Game/Collisions/Collision-detection
        Potentially allow for AABB with circles.
        Or use SAT.
        */
    }

    /// <summary>
    /// Detects collision between two axis-aligned bounding boxes (AABBs).
    /// This method calculates if and how two AABBs intersect and determines
    /// the collision details such as direction and impact factor.
    /// </summary>
    /// <param name="actor">The dynamic shape representing the moving object.</param>
    /// <param name="shape">The static shape representing the stationary object.</param>
    /// <returns>A <see cref="CollisionData"/> object containing collision information.</returns>
    public static CollisionData Aabb(DynamicShape actor, Shape shape) {
        var data = new CollisionData {
            Collision = false,
            VelocityFactor = new Vector2(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUnchecked
        };

        var dynLowerLeft = new Vector2(actor.Position.X, actor.Position.Y);
        var dynUpperRight = new Vector2(actor.Position.X + actor.Extent.X,
            actor.Position.Y + actor.Extent.Y);

        var staLowerLeft = new Vector2(shape.Position.X, shape.Position.Y);
        var staUpperRight = new Vector2(shape.Position.X + shape.Extent.X,
            shape.Position.Y + shape.Extent.Y);

        // inactive movement in both x- and y-direction
        if(System.Math.Abs(actor.Velocity.X) < 1e-6f && System.Math.Abs(actor.Velocity.Y) < 1e-6f) {
            return data;
        }

        // inactive movement in x-direction
        else if(System.Math.Abs(actor.Velocity.X) < 1e-6f)
        {
            float entryDistanceY, exitDistanceY;
            if(actor.Velocity.Y < 0.0f)
            {
                entryDistanceY = staUpperRight.Y - dynLowerLeft.Y;
                exitDistanceY = staLowerLeft.Y - dynUpperRight.Y;
                data.CollisionDir = CollisionDirection.CollisionDirUp;
            }
            else
            {
                entryDistanceY = staLowerLeft.Y - dynUpperRight.Y;
                exitDistanceY = staUpperRight.Y - dynLowerLeft.Y;
                data.CollisionDir = CollisionDirection.CollisionDirDown;
            }

            var entryTimeY = entryDistanceY / actor.Velocity.Y;
            var exitTimeY = exitDistanceY / actor.Velocity.Y;

            bool xOverlaps = staUpperRight.X > dynLowerLeft.X && staLowerLeft.X < dynUpperRight.X;

            if(entryTimeY < exitTimeY && entryTimeY >= 0.0f && entryTimeY < 1.0f && xOverlaps)
            {
                data.VelocityFactor = new Vector2(data.VelocityFactor.X, entryTimeY);
                data.Collision = true;
                return data;
            }
            else
            {
                return data;
            }
        }
        // inactive movement in y-direction
        else if(System.Math.Abs(actor.Velocity.Y) < 1e-6f)
        {
            float entryDistanceX, exitDistanceX;
            if(actor.Velocity.X < 0.0f)
            {
                entryDistanceX = staUpperRight.X - dynLowerLeft.X;
                exitDistanceX = staLowerLeft.X - dynUpperRight.X;
                data.CollisionDir = CollisionDirection.CollisionDirRight;
            }
            else
            {
                entryDistanceX = staLowerLeft.X - dynUpperRight.X;
                exitDistanceX = staUpperRight.X - dynLowerLeft.X;
                data.CollisionDir = CollisionDirection.CollisionDirLeft;
            }

            float entryTimeX = entryDistanceX / actor.Velocity.X;
            float exitTimeX = exitDistanceX / actor.Velocity.X;

            bool yOverlaps = staUpperRight.Y > dynLowerLeft.Y && staLowerLeft.Y < dynUpperRight.Y;

            if(entryTimeX < exitTimeX && entryTimeX >= 0.0f && entryTimeX < 1.0f && yOverlaps)
            {
                data.VelocityFactor = new Vector2(entryTimeX, data.VelocityFactor.Y);
                data.Collision = true;
            }
            return data;
        }
        // active movement in both x- and y-direction
        else
        {
            var entryDistance = new Vector2();
            var exitDistance = new Vector2();

            if(actor.Velocity.X < 0.0f)
            {
                entryDistance.X = staUpperRight.X - dynLowerLeft.X;
                exitDistance.X = staLowerLeft.X - dynUpperRight.X;
            }
            else
            {
                entryDistance.X = staLowerLeft.X - dynUpperRight.X;
                exitDistance.X = staUpperRight.X - dynLowerLeft.X;
            }
            if(actor.Velocity.Y < 0.0f)
            {
                entryDistance.Y = staUpperRight.Y - dynLowerLeft.Y;
                exitDistance.Y = staLowerLeft.Y - dynUpperRight.Y;
            }
            else
            {
                entryDistance.Y = staLowerLeft.Y - dynUpperRight.Y;
                exitDistance.Y = staUpperRight.Y - dynLowerLeft.Y;
            }

            var entryTime = new Vector2(entryDistance.X / actor.Velocity.X, entryDistance.Y / actor.Velocity.Y);
            var exitTime = new Vector2(exitDistance.X / actor.Velocity.X, exitDistance.Y / actor.Velocity.Y);

            float entryTimeMax = System.Math.Max(entryTime.X, entryTime.Y);
            float exitTimeMin = System.Math.Min(exitTime.X, exitTime.Y);

            if(entryTimeMax < exitTimeMin && (entryTime.X >= 0.0f || entryTime.Y >= 0.0f) &&
                entryTime.X < 1.0f && entryTime.Y < 1.0f)
            {
                if (entryTime.X > entryTime.Y)
                {
                    data.VelocityFactor = new Vector2(entryTimeMax, data.VelocityFactor.Y);
                    if (actor.Velocity.X < 0.0f) {
                        data.CollisionDir = CollisionDirection.CollisionDirRight;
                    } else {
                        data.CollisionDir = CollisionDirection.CollisionDirLeft;
                    }
                }
                else
                {
                    data.VelocityFactor = new Vector2(data.VelocityFactor.X, entryTimeMax);
                    if (actor.Velocity.Y < 0.0f) {
                        data.CollisionDir = CollisionDirection.CollisionDirUp;
                    } else {
                        data.CollisionDir = CollisionDirection.CollisionDirDown;
                    }
                }

                data.Collision = true;
                return data;
            }
            else
            {
                return data;
            }
        }
    }
}
