using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.Physics {
    [TestFixture]
    public class TestMovementCollision {

        /// <summary>
        /// Move an actor in its predefined direction, checking for collisions
        /// with a list of obstacles.
        /// </summary>
        /// <returns>If any collision occured</returns>
        public bool CheckActorMove(DynamicShape actor, List<StationaryShape> obstacles, int iterations) {
            var collision = false;

            for (int i = 0; i < iterations; i++) {
                actor.Move();
                foreach (var obstacle in obstacles) {
                    collision = CollisionDetection.Aabb(actor, obstacle).Collision;
                }
            }
            return collision;
        }

        /// <summary>
        /// An entity is moving upwards in a clear path between two obstacles
        /// </summary>
        [Test]
        public void TestMoveUp() {
            var obstacleLeft = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.4f, 1.0f));
            var obstacleRight = new StationaryShape(new Vec2F(0.6f, 0.0f), new Vec2F(0.4f, 1.0f));
            var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

            var actor = new DynamicShape(new Vec2F(0.45f, 0.0f), new Vec2F(0.1f, 0.1f));
            actor.Direction.X = 0.0f;
            actor.Direction.Y = 0.001f;

            Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
        }

        /// <summary>
        /// An entity is moving downwards in a clear path between two obstacles
        /// </summary>
        [Test]
        public void TestMoveDown() {
            var obstacleLeft = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.4f, 1.0f));
            var obstacleRight = new StationaryShape(new Vec2F(0.6f, 0.0f), new Vec2F(0.4f, 1.0f));
            var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

            var actor = new DynamicShape(new Vec2F(0.45f, 1.0f), new Vec2F(0.1f, 0.1f));
            actor.Direction.X = 0.0f;
            actor.Direction.Y = -0.001f;

            Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
        }

        /// <summary>
        /// An entity is moving left in a clear path between two obstacles
        /// </summary>
        [Test]
        public void TestMoveLeft() {
            var obstacleLeft = new StationaryShape(new Vec2F(0.0f, 0.6f), new Vec2F(1.0f, 0.4f));
            var obstacleRight = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 0.4f));
            var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

            var actor = new DynamicShape(new Vec2F(0.0f, 0.45f), new Vec2F(0.1f, 0.1f));
            actor.Direction.X = 0.001f;
            actor.Direction.Y = 0.0f;

            Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
        }

        /// <summary>
        /// An entity is moving left in a clear path between two obstacles
        /// </summary>
        [Test]
        public void TestMoveRight() {
            var obstacleLeft = new StationaryShape(new Vec2F(0.0f, 0.6f), new Vec2F(1.0f, 0.4f));
            var obstacleRight = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 0.4f));
            var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

            var actor = new DynamicShape(new Vec2F(0.9f, 0.45f), new Vec2F(0.1f, 0.1f));
            actor.Direction.X = -0.001f;
            actor.Direction.Y = 0.0f;

            Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
        }

        /// <summary>
        /// An entity is moving diagonally up and left in a clear path between
        /// a number of obstacles on each side of its path
        /// </summary>
        [Test]
        public void TestMoveDiagonalUpRight() {
            var obstacles = new List<StationaryShape>() {
                // (pos, extent)
                new StationaryShape(new Vec2F(0.0f, 0.8f), new Vec2F(0.2f, 0.2f)),
                new StationaryShape(new Vec2F(0.0f, 0.5f), new Vec2F(0.2f, 0.2f)),
                new StationaryShape(new Vec2F(0.5f, 0.8f), new Vec2F(0.2f, 0.2f))
            };

            // (pos, extent, dir)
            var actor = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.1f, 0.1f), new Vec2F(0.001f, 0.001f));

            Assert.IsFalse(CheckActorMove(actor, obstacles, 1500));
        }
    }
}