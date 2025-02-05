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
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirRight));
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
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirLeft));
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
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirDown));
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
                Assert.That(data.CollisionDir, Is.EqualTo(CollisionDirection.CollisionDirUp));
                return;
            }
            actor.Position += actor.Velocity;
        }
        Assert.IsTrue(false); // collision was supposed to happen
    }

    // The following four sweeps demonstrate that collision detection is consistent, whether or
    // not the actor comes straight at the block or at an angle.

    [TestCase(-0.02f)]
    [TestCase(-0.01f)]
    [TestCase( 0.00f)]
    [TestCase( 0.01f)]
    [TestCase( 0.02f)]
    public void SweepUp(float dx) {
        actor.Velocity.X = dx;
        actor.Velocity.Y = 1;

        var actual = CollisionDetection.Aabb(actor, solidBlockUp);

        Assert.AreEqual(true, actual.Collision);
        Assert.AreEqual(CollisionDirection.CollisionDirDown, actual.CollisionDir);
    }

    [TestCase(-0.02f)]
    [TestCase(-0.01f)]
    [TestCase( 0.00f)]
    [TestCase( 0.01f)]
    [TestCase( 0.02f)]
    public void SweepDown(float dx) {
        actor.Velocity.X = dx;
        actor.Velocity.Y = -1;

        var actual = CollisionDetection.Aabb(actor, solidBlockDown);

        Assert.AreEqual(true, actual.Collision);
        Assert.AreEqual(CollisionDirection.CollisionDirUp, actual.CollisionDir);
    }

    [TestCase(-0.02f)]
    [TestCase(-0.01f)]
    [TestCase( 0.00f)]
    [TestCase( 0.01f)]
    [TestCase( 0.02f)]
    public void SweepLeft(float dy) {
        actor.Velocity.X = -1;
        actor.Velocity.Y = dy;

        var actual = CollisionDetection.Aabb(actor, solidBlockLeft);

        Assert.AreEqual(true, actual.Collision);
        Assert.AreEqual(CollisionDirection.CollisionDirRight, actual.CollisionDir);
    }

    [TestCase(-0.02f)]
    [TestCase(-0.01f)]
    [TestCase( 0.00f)]
    [TestCase( 0.01f)]
    [TestCase( 0.02f)]
    public void SweepRight(float dy) {
        actor.Velocity.X = 1;
        actor.Velocity.Y = dy;

        var actual = CollisionDetection.Aabb(actor, solidBlockRight);

        Assert.AreEqual(true, actual.Collision);
        Assert.AreEqual(CollisionDirection.CollisionDirLeft, actual.CollisionDir);
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