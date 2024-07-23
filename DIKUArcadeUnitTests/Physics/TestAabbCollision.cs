namespace DIKUArcadeUnitTests.Physics;

using DIKUArcade.Entities;
using System.Numerics;
using DIKUArcade.Physics;
using NUnit.Framework;

[TestFixture]
public class TestAabbCollision {
    private DynamicShape actor;
    private float actorVelocity;
    private StationaryShape solidBlockLeft;
    private StationaryShape solidBlockRight;
    private StationaryShape solidBlockUp;
    private StationaryShape solidBlockDown;

    public TestAabbCollision() {
        solidBlockLeft  = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(0.1f, 1.0f));
        solidBlockRight = new StationaryShape(new Vector2(1.0f, 0.0f), new Vector2(0.1f, 1.0f));
        solidBlockUp    = new StationaryShape(new Vector2(0.0f, 1.0f), new Vector2(1.0f, 0.1f));
        solidBlockDown  = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.1f));
        actorVelocity = 0.05f;
    }

    [SetUp]
    public void BeforeEachTest() {
        actor = new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.1f, 0.1f));
    }


    // =====  TESTING COLLISION  ===== //

    [Test]
    public void TestCollisionDirectionLeft() {
        actor.Velocity.X = -actorVelocity;
        actor.Velocity.Y = 0.0f;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockLeft);
            if (data.Collision) {
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirLeft));
                return;
            }
            actor.Position += actor.Velocity;
        }
        Assert.IsTrue(false); // collision was supposed to happen
    }

    [Test]
    public void TestCollisionDirectionRight() {
        actor.Velocity.X = actorVelocity;
        actor.Velocity.Y = 0.0f;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockRight);
            if (data.Collision) {
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirRight));
                return;
            }
            actor.Position += actor.Velocity;
        }
        Assert.IsTrue(false); // collision was supposed to happen
    }

    [Test]
    public void TestCollisionDirectionUp() {
        actor.Velocity.X = 0.0f;
        actor.Velocity.Y = actorVelocity;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockUp);
            if (data.Collision) {
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirUp));
                return;
            }
            actor.Position += actor.Velocity;
        }
        Assert.IsTrue(false); // collision was supposed to happen
    }

    [Test]
    public void TestCollisionDirectionDown() {
        actor.Velocity.X = 0.0f;
        actor.Velocity.Y = -actorVelocity;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockDown);
            if (data.Collision) {
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirDown));
                return;
            }
            actor.Position += actor.Velocity;
        }
        Assert.IsTrue(false); // collision was supposed to happen
    }


    // =====  NEGATIVE COLLISION TESTING  ===== //

    [Test]
    public void TestNoCollisionDirectionLeft() {
        actor.Velocity.X = -actorVelocity;
        actor.Velocity.Y = 0.0f;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockRight);
            actor.Position += actor.Velocity;

            Assert.IsFalse(data.Collision);
        }
    }

    [Test]
    public void TestNoCollisionDirectionRight() {
        actor.Velocity.X = actorVelocity;
        actor.Velocity.Y = 0.0f;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockLeft);
            actor.Position += actor.Velocity;

            Assert.IsFalse(data.Collision);
        }
    }

    [Test]
    public void TestNoCollisionDirectionUp() {
        actor.Velocity.X = 0.0f;
        actor.Velocity.Y = actorVelocity;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockDown);
            actor.Position += actor.Velocity;

            Assert.IsFalse(data.Collision);
        }
    }

    [Test]
    public void TestNoCollisionDirectionDown() {
        actor.Velocity.X = 0.0f;
        actor.Velocity.Y = -actorVelocity;
        for (int i = 0; i < 20; i++) {
            var data = CollisionDetection.Aabb(actor, solidBlockUp);
            actor.Position += actor.Velocity;

            Assert.IsFalse(data.Collision);
        }
    }


    // =====  TESTING COLLISION MULTIPLICATION FACTORS  ===== //
    [Test]
    public void TestCollisionMultiplicationFactorCloseness() {
        var wall = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f));
        var move = new DynamicShape(new Vector2(2.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector2(-1.0f, 0.0f));
        var data = CollisionDetection.Aabb(move, wall);
        Assert.IsFalse(data.Collision);
    }

    [Test]
    public void TestCollisionMultiplicationFactorExactness() {
        var wall = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f));
        var move = new DynamicShape(new Vector2(2.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector2(-2.0f, 0.0f));
        var data = CollisionDetection.Aabb(move, wall);
        Assert.AreEqual(data.VelocityFactor.X, 0.5f);
    }
}