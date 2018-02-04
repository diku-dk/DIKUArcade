﻿using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace DIKUArcade.Physics {

    public class CollisionDetection {
        public static CollisionData Aabb(DynamicShape actor, Shape shape) {
            var data = new CollisionData {
                Collision = false,
                DirectionFactor = new Vec2F(1.0f, 1.0f),
                CollisionDir = CollisionDirection.CollisionDirUnchecked
            };

            var InvEntry = new Vec2F();
            var InvExit = new Vec2F();

            //glm::vec2 dynLowerLeft(dyn->pos.x, dyn->pos.y - dyn->height);
            //glm::vec2 dynUpperRight(dyn->pos.x + dyn->width, dyn->pos.y);
            var dynLowerLeft = new Vec2F(actor.Position.X, actor.Position.Y);
            var dynUpperRight = new Vec2F(actor.Position.X + actor.Extent.X,
                actor.Position.Y + actor.Extent.Y);

            //glm::vec2 staLowerLeft(sta->pos.x, sta->pos.y - sta->height);
            //glm::vec2 staUpperRight(sta->pos.x + sta->width, sta->pos.y);
            var staLowerLeft = new Vec2F(shape.Position.X, shape.Position.Y);
            var staUpperRight = new Vec2F(shape.Position.X + shape.Extent.X,
                shape.Position.Y + shape.Extent.Y);

            if(System.Math.Abs(actor.Direction.X) < 1e-6f && System.Math.Abs(actor.Direction.Y) < 1e-6f)
            {
                return data;
            }
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
                    //data.CollisionDir = CollisionDirection.CollisionDirUp;
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
                    //data.CollisionDir = CollisionDirection.CollisionDirRight;
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
                float exitTimeMin = System.Math.Max(exitTime.Y, exitTime.Y);

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