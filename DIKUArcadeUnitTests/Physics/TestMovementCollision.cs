namespace DIKUArcadeUnitTests.Physics;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using System.Numerics;
using DIKUArcade.Physics;
using NUnit.Framework;

[TestFixture]
public class TestMovementCollision {

    /// <summary>
    /// Move an actor in its predefined Velocity, checking for collisions
    /// with a list of obstacles.
    /// </summary>
    /// <returns>If any collision occured</returns>
    private bool CheckActorMove(DynamicShape actor, List<StationaryShape> obstacles, int iterations) {
        var collision = false;

        for (int i = 0; i < iterations; i++) {
            actor.Move();
            foreach (var obstacle in obstacles) {
                collision = CollisionDetection.Aabb(actor, obstacle).Collision;
                if (collision) {
                    Console.WriteLine($"Encountered collision:\n" +
                                        $"actor lower left: {actor.Position}\n" +
                                        $"actor upper left: {actor.Position + new Vector2(0.0f, actor.Extent.Y)}\n" +
                                        $"actor upper right: {actor.Position + actor.Extent}\n" +
                                        $"actor upper left: {actor.Position + new Vector2(actor.Extent.X, 0.0f)}\n" +
                                        $"obstacle pos: {obstacle.Position}\n" +
                                        $"obstacle extent: {obstacle.Extent}"
                    );
                                        //actor.pos: {actor.Position}, obstacle.pos: {obstacle.Position}");
                    return true;
                }
            }
        }
        return collision;
    }

    /// <summary>
    /// An entity is moving upwards in a clear path between two obstacles
    /// </summary>
    [Test]
    public void TestMoveUp() {
        var obstacleLeft = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(0.4f, 1.0f));
        var obstacleRight = new StationaryShape(new Vector2(0.6f, 0.0f), new Vector2(0.4f, 1.0f));
        var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

        var actor = new DynamicShape(new Vector2(0.45f, 0.0f), new Vector2(0.1f, 0.1f));
        actor.Velocity.X = 0.0f;
        actor.Velocity.Y = 0.001f;

        Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
    }

    /// <summary>
    /// An entity is moving downwards in a clear path between two obstacles
    /// </summary>
    [Test]
    public void TestMoveDown() {
        var obstacleLeft = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(0.4f, 1.0f));
        var obstacleRight = new StationaryShape(new Vector2(0.6f, 0.0f), new Vector2(0.4f, 1.0f));
        var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

        var actor = new DynamicShape(new Vector2(0.45f, 1.0f), new Vector2(0.1f, 0.1f));
        actor.Velocity.X = 0.0f;
        actor.Velocity.Y = -0.001f;

        Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
    }

    /// <summary>
    /// An entity is moving left in a clear path between two obstacles
    /// </summary>
    [Test]
    public void TestMoveLeft() {
        var obstacleLeft = new StationaryShape(new Vector2(0.0f, 0.6f), new Vector2(1.0f, 0.4f));
        var obstacleRight = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.4f));
        var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

        var actor = new DynamicShape(new Vector2(0.0f, 0.45f), new Vector2(0.1f, 0.1f));
        actor.Velocity.X = 0.001f;
        actor.Velocity.Y = 0.0f;

        Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
    }

    /// <summary>
    /// An entity is moving left in a clear path between two obstacles
    /// </summary>
    [Test]
    public void TestMoveRight() {
        var obstacleLeft = new StationaryShape(new Vector2(0.0f, 0.6f), new Vector2(1.0f, 0.4f));
        var obstacleRight = new StationaryShape(new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.4f));
        var obstacles = new List<StationaryShape>() {obstacleLeft, obstacleRight};

        var actor = new DynamicShape(new Vector2(0.9f, 0.45f), new Vector2(0.1f, 0.1f));
        actor.Velocity.X = -0.001f;
        actor.Velocity.Y = 0.0f;

        Assert.IsFalse(CheckActorMove(actor, obstacles, 1000));
    }

    /// <summary>
    /// An entity is moving diagonally up and right in a clear path between
    /// a number of obstacles on each side of its path
    /// </summary>
    [Test]
    public void TestMoveDiagonalUpRight() {
        var obstacles = new List<StationaryShape>() {
            // (pos, extent)
            // upper-left triangle
            new StationaryShape(new Vector2(0.0f, 0.8f), new Vector2(0.2f, 0.2f)),
            new StationaryShape(new Vector2(0.0f, 0.5f), new Vector2(0.2f, 0.2f)),
            new StationaryShape(new Vector2(0.5f, 0.8f), new Vector2(0.2f, 0.2f)),

            // lower-right triangle
            new StationaryShape(new Vector2(0.8f, 0.0f), new Vector2(0.2f, 0.2f)),
            new StationaryShape(new Vector2(0.5f, 0.0f), new Vector2(0.2f, 0.2f)),
            new StationaryShape(new Vector2(0.8f, 0.5f), new Vector2(0.2f, 0.2f))
        };

        // (pos, extent, dir)
        var actor = new DynamicShape(new Vector2(0.0f, 0.0f), new Vector2(0.09f, 0.09f), new Vector2(0.001f, 0.001f));

        Assert.IsFalse(CheckActorMove(actor, obstacles, 1500));
    }

    /// <summary>
    /// This check was developed by a student
    /// </summary>
    [Test]
    public void StudentCheck() {
        // Copied in the exact parameters from assignment 9 debug run where it first fails
        StationaryShape s = new StationaryShape(new Vector2(0.9f, 0.739130437f), new Vector2(0.025f, 0.0434782617f));
        DynamicShape a = new DynamicShape(new Vector2(0.744843602f, 0.783036947f),
            new Vector2(0.065f, 0.05f), new Vector2(-0.00157072174f, -0.00205000024f));

        Assert.IsFalse(CollisionDetection.Aabb(a, s).Collision);
    }
}
