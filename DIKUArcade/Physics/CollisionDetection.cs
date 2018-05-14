using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace DIKUArcade.Physics {
    public class CollisionDetection {
        
        
        /// <summary>
        /// Performs an Aabb collision test between two shapes. Uses CollisionDirection data to
        /// test for collision rather than testing for collision before determining direction.
        /// The CollisionDirection is only left unchecked if the actor shape is not moving and this
        /// provides smoother collision when the taxi is moving in both directions.
        /// TODO: needs testing. Edge cases might deem this NOT a better approach!!!
        /// </summary>
        public static CollisionData Aabb2(DynamicShape actor, Shape shape, 
            CollisionDirection actorDir) {
            
            
            CollisionData data = new CollisionData {
                Collision = false,
                DirectionFactor = new Vec2F(1f, 1f),
                CollisionDir = actorDir
            };

            
            //if dir unchecked and actor not moving in either direction
            if (actorDir == CollisionDirection.CollisionDirUnchecked) {
                return data;
            }
            
            
            //draw bounding boxes. The coordinates of each box represent LowerLeft.X,
            //LowerLeft.Y, UpperRight.X, and UpperRight.Y, respectively.
            
            Vec4F dynBox = new Vec4F(actor.Position.X, actor.Position.Y, 
                actor.Position.X + actor.Extent.X, actor.Position.Y + actor.Extent.Y);

            Vec4F staBox = new Vec4F(shape.Position.X, shape.Position.Y,
                shape.Position.X + shape.Extent.X, shape.Position.Y + shape.Extent.Y);
                
                
            data.CollisionDir = actorDir;


            if (actorDir == CollisionDirection.CollisionDirDown ||
                actorDir == CollisionDirection.CollisionDirUp) {
                
                float entryDistanceY = actorDir == CollisionDirection.CollisionDirDown
                    ? staBox.W - dynBox.Y
                    : staBox.Y - dynBox.W;
                
                float exitDistanceY = actorDir == CollisionDirection.CollisionDirDown
                    ? staBox.Y - dynBox.W
                    : staBox.W - dynBox.Y;
                
                
                float entryTimeY = entryDistanceY / actor.Direction.Y;
                float exitTimeY = exitDistanceY / actor.Direction.Y;
                
                bool xOverlaps = staBox.Z > dynBox.X && staBox.X < dynBox.Z;
                

                if (entryTimeY < exitTimeY && entryTimeY > .0f && entryTimeY < 1f && xOverlaps) {
                    data.DirectionFactor.Y = entryTimeY;
                    data.Collision = true;
                }
                
                
            } else if (actorDir == CollisionDirection.CollisionDirLeft ||
                       actorDir == CollisionDirection.CollisionDirRight) {

                float entryDistanceX = actorDir == CollisionDirection.CollisionDirDown
                    ? staBox.Z - dynBox.X
                    : staBox.X - dynBox.Z;
                
                float exitDistanceX = actorDir == CollisionDirection.CollisionDirDown
                    ? staBox.X - dynBox.Z
                    : staBox.Z - dynBox.X;
                

                float entryTimeX = entryDistanceX / actor.Direction.X;
                float exitTimeX = exitDistanceX / actor.Direction.X;

                bool yOverlaps = staBox.W > dynBox.Y && staBox.Y < dynBox.W;
                

                if (entryTimeX < exitTimeX && entryTimeX >= .0f && entryTimeX < 1f && yOverlaps) {
                    data.DirectionFactor.X = entryTimeX;
                    data.Collision = true;
                }

            }
            
            return data;
        }
        
        
        /// <summary>
        /// Calculates the angle of a vector using Math.Atan2 (see msdn doc) to determine direction
        /// of an actor shape.
        /// </summary>
        /// <param name="actorDir">The velocity vector of an actor shape in a collision test</param>
        public static CollisionDirection CalcDir(Vec2F actorDir) {

            CollisionDirection dirData;
            //The arctangent Atan(v) of a vector v=(x,y) is 0 if y==0 && x>0 (ie. v points directly
            //to the right). However, Atan2 ALSO returns 0 for zero vectors, so this first if is necessary:
            if (actorDir.Length() < 1e-6f) {
                dirData = CollisionDirection.CollisionDirUnchecked;
                
                return dirData;
            }
            
            
            var rads = System.Math.Atan2(actorDir.Y, actorDir.X);
            var absRads = System.Math.Abs(rads);
            
            
            if (absRads <= 0.785) {
                dirData = CollisionDirection.CollisionDirRight;
                
            } else if (absRads > 2.356) {
                dirData = CollisionDirection.CollisionDirLeft;
                
            } else if (rads > 0.785 && rads <= 2.356) {
                dirData = CollisionDirection.CollisionDirUp;
                
            } else if (rads < -0.785 && rads >= -2.356) {
                dirData = CollisionDirection.CollisionDirDown;
                
              //this will (should?) never evaluate, but is kept for good measure
            } else {
                dirData = CollisionDirection.CollisionDirUnchecked;
                
            }

            return dirData;
        }

        
        /// <summary>
        /// If multiple collision tests with the same actor shape and during the same iteration
        /// of the GameLoop (eg. in a foreach on an EntityContainer) are NOT necessary,
        /// use this overload.
        /// </summary>
        public static CollisionData Aabb2(DynamicShape actor, Shape shape) {
            
            CollisionDirection actorDir = CollisionDetection.CalcDir(actor.Direction);
            
            return CollisionDetection.Aabb2(actor, shape, actorDir);
            
        }
        
        
        
        
        
        public static CollisionData Aabb(DynamicShape actor, Shape shape) {
            var data = new CollisionData {
                Collision = false,
                DirectionFactor = new Vec2F(1.0f, 1.0f),
                CollisionDir = CollisionDirection.CollisionDirUnchecked
            };

            var dynLowerLeft = new Vec2F(actor.Position.X, actor.Position.Y);
            var dynUpperRight = new Vec2F(actor.Position.X + actor.Extent.X,
                actor.Position.Y + actor.Extent.Y);

            var staLowerLeft = new Vec2F(shape.Position.X, shape.Position.Y);
            var staUpperRight = new Vec2F(shape.Position.X + shape.Extent.X,
                shape.Position.Y + shape.Extent.Y);

            // inactive movement in both x- and y-direction
            if(System.Math.Abs(actor.Direction.X) < 1e-6f && System.Math.Abs(actor.Direction.Y) < 1e-6f)
            {
                return data;
            }

            // inactive movement in x-direction
            else if(System.Math.Abs(actor.Direction.X) < 1e-6f)
            {
                float entryDistanceY, exitDistanceY;
                if(actor.Direction.Y < 0.0f)
                {
                    entryDistanceY = staUpperRight.Y - dynLowerLeft.Y;
                    exitDistanceY = staLowerLeft.Y - dynUpperRight.Y;
                    data.CollisionDir = CollisionDirection.CollisionDirDown;
                }
                else
                {
                    entryDistanceY = staLowerLeft.Y - dynUpperRight.Y;
                    exitDistanceY = staUpperRight.Y - dynLowerLeft.Y;
                    data.CollisionDir = CollisionDirection.CollisionDirUp;
                }

                var entryTimeY = entryDistanceY / actor.Direction.Y;
                var exitTimeY = exitDistanceY / actor.Direction.Y;

                bool xOverlaps = staUpperRight.X > dynLowerLeft.X && staLowerLeft.X < dynUpperRight.X;

                if(entryTimeY < exitTimeY && entryTimeY >= 0.0f && entryTimeY < 1.0f && xOverlaps)
                {
                    data.DirectionFactor.Y = entryTimeY;
                    data.Collision = true;
                    return data;
                }
                else
                {
                    return data;
                }
            }
            // inactive movement in y-direction
            else if(System.Math.Abs(actor.Direction.Y) < 1e-6f)
            {
                float entryDistanceX, exitDistanceX;
                if(actor.Direction.X < 0.0f)
                {
                    entryDistanceX = staUpperRight.X - dynLowerLeft.X;
                    exitDistanceX = staLowerLeft.X - dynUpperRight.X;
                    data.CollisionDir = CollisionDirection.CollisionDirLeft;
                }
                else
                {
                    entryDistanceX = staLowerLeft.X - dynUpperRight.X;
                    exitDistanceX = staUpperRight.X - dynLowerLeft.X;
                    data.CollisionDir = CollisionDirection.CollisionDirRight;
                }

                float entryTimeX = entryDistanceX / actor.Direction.X;
                float exitTimeX = exitDistanceX / actor.Direction.X;

                bool yOverlaps = staUpperRight.Y > dynLowerLeft.Y && staLowerLeft.Y < dynUpperRight.Y;

                if(entryTimeX < exitTimeX && entryTimeX >= 0.0f && entryTimeX < 1.0f && yOverlaps)
                {
                    data.DirectionFactor.X = entryTimeX;
                    data.Collision = true;
                }
                return data;
            }
            // active movement in both x- and y-direction
            else
            {
                var entryDistance = new Vec2F();
                var exitDistance = new Vec2F();

                if(actor.Direction.X < 0.0f)
                {
                    entryDistance.X = staUpperRight.X - dynLowerLeft.X;
                    exitDistance.X = staLowerLeft.X - dynUpperRight.X;
                }
                else
                {
                    entryDistance.X = staLowerLeft.X - dynUpperRight.X;
                    exitDistance.X = staUpperRight.X - dynLowerLeft.X;
                }
                if(actor.Direction.Y < 0.0f)
                {
                    entryDistance.Y = staUpperRight.Y - dynLowerLeft.Y;
                    exitDistance.Y = staLowerLeft.Y - dynUpperRight.Y;
                }
                else
                {
                    entryDistance.Y = staLowerLeft.Y - dynUpperRight.Y;
                    exitDistance.Y = staUpperRight.Y - dynLowerLeft.Y;
                }

                var entryTime = new Vec2F(entryDistance.X / actor.Direction.X, entryDistance.Y / actor.Direction.Y);
                var exitTime = new Vec2F(exitDistance.X / actor.Direction.X, exitDistance.Y / actor.Direction.Y);

                float entryTimeMax = System.Math.Max(entryTime.X, entryTime.Y);
                float exitTimeMin = System.Math.Min(exitTime.X, exitTime.Y);

                if(entryTimeMax < exitTimeMin && (entryTime.X >= 0.0f || entryTime.Y >= 0.0f) &&
                   entryTime.X < 1.0f && entryTime.Y < 1.0f)
                {
                    if (entryTime.X > entryTime.Y)
                    {
                        data.DirectionFactor.X = entryTimeMax;
                    }
                    else
                    {
                        data.DirectionFactor.Y = entryTimeMax;
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
}
