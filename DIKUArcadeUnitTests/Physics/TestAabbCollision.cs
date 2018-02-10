using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.Physics {
    [TestFixture]
    public class TestAabbCollision {
        private DynamicShape actor;
        private float actorVelocity;
        private StationaryShape solidBlockLeft;
        private StationaryShape solidBlockRight;
        private StationaryShape solidBlockUp;
        private StationaryShape solidBlockDown;

        public TestAabbCollision() {
            solidBlockLeft  = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.1f, 1.0f));
            solidBlockRight = new StationaryShape(new Vec2F(1.0f, 0.0f), new Vec2F(0.1f, 1.0f));
            solidBlockUp    = new StationaryShape(new Vec2F(0.0f, 1.0f), new Vec2F(1.0f, 0.1f));
            solidBlockDown  = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 0.1f));
            actorVelocity = 0.05f;
        }

        [SetUp]
        public void BeforeEachTest() {
            actor = new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f));
        }


        // =====  TESTING COLLISION  ===== //

        [Test]
        public void TestCollisionDirectionLeft() {
            actor.Direction.X = -actorVelocity;
            actor.Direction.Y = 0.0f;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockLeft);
                if (data.Collision) {
                    Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirLeft));
                    return;
                }
                actor.Position += actor.Direction;
            }
            Assert.IsTrue(false); // collision was supposed to happen
        }

        [Test]
        public void TestCollisionDirectionRight() {
            actor.Direction.X = actorVelocity;
            actor.Direction.Y = 0.0f;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockRight);
                if (data.Collision) {
                    Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirRight));
                    return;
                }
                actor.Position += actor.Direction;
            }
            Assert.IsTrue(false); // collision was supposed to happen
        }

        [Test]
        public void TestCollisionDirectionUp() {
            actor.Direction.X = 0.0f;
            actor.Direction.Y = actorVelocity;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockUp);
                if (data.Collision) {
                    Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirUp));
                    return;
                }
                actor.Position += actor.Direction;
            }
            Assert.IsTrue(false); // collision was supposed to happen
        }

        [Test]
        public void TestCollisionDirectionDown() {
            actor.Direction.X = 0.0f;
            actor.Direction.Y = -actorVelocity;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockDown);
                if (data.Collision) {
                    Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirDown));
                    return;
                }
                actor.Position += actor.Direction;
            }
            Assert.IsTrue(false); // collision was supposed to happen
        }


        // =====  NEGATIVE COLLISION TESTING  ===== //

        [Test]
        public void TestNoCollisionDirectionLeft() {
            actor.Direction.X = -actorVelocity;
            actor.Direction.Y = 0.0f;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockRight);
                actor.Position += actor.Direction;

                Assert.IsFalse(data.Collision);
            }
        }

        [Test]
        public void TestNoCollisionDirectionRight() {
            actor.Direction.X = actorVelocity;
            actor.Direction.Y = 0.0f;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockLeft);
                actor.Position += actor.Direction;

                Assert.IsFalse(data.Collision);
            }
        }

        [Test]
        public void TestNoCollisionDirectionUp() {
            actor.Direction.X = 0.0f;
            actor.Direction.Y = actorVelocity;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockDown);
                actor.Position += actor.Direction;

                Assert.IsFalse(data.Collision);
            }
        }

        [Test]
        public void TestNoCollisionDirectionDown() {
            actor.Direction.X = 0.0f;
            actor.Direction.Y = -actorVelocity;
            for (int i = 0; i < 20; i++) {
                var data = CollisionDetection.Aabb(actor, solidBlockUp);
                actor.Position += actor.Direction;

                Assert.IsFalse(data.Collision);
            }
        }


        // =====  TESTING COLLISION MULTIPLICATION FACTORS  ===== //
        [Test]
        public void TestCollisionMultiplicationFactorCloseness() {
            var wall = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
            var move = new DynamicShape(new Vec2F(2.0f, 0.0f), new Vec2F(1.0f, 1.0f), new Vec2F(-1.0f, 0.0f));
            var data = CollisionDetection.Aabb(move, wall);
            Assert.IsFalse(data.Collision);
        }

        [Test]
        public void TestCollisionMultiplicationFactorExactness() {
            var wall = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
            var move = new DynamicShape(new Vec2F(2.0f, 0.0f), new Vec2F(1.0f, 1.0f), new Vec2F(-2.0f, 0.0f));
            var data = CollisionDetection.Aabb(move, wall);
            Assert.AreEqual(data.DirectionFactor.X, 0.5f);
        }
    }
}