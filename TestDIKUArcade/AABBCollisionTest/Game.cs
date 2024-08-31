namespace TestDIKUArcade.AabbCollisionTest;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using System.Reflection;

public class Game : DIKUGame {
    private Entity player;
    private Entity wall;

    private float playerVelocity = 0.07f;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Entity(new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.1f, 0.1f)),
            new Image("TestDIKUArcade.Assets.Taxi.png"));
        wall = new Entity(new StationaryShape(new Vector2(0.25f, 0.0f), new Vector2(0.15f, 0.15f)),
            new Image("TestDIKUArcade.Assets.wall.jpeg"));
    }

    public void MovePlayer(Vector2 dir) {
        ((DynamicShape) player.Shape).Velocity = dir;
        var collide = CollisionDetection.Aabb((DynamicShape) player.Shape, wall.Shape);
        if (collide.Collision) {
            Console.WriteLine($"CollisionDetection occured in direction {collide.CollisionDir}");
            dir *= collide.VelocityFactor;
        }
        player.Shape.Position += dir;
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Left:
                MovePlayer(new Vector2(-playerVelocity, 0.0f));
                break;
            case KeyboardKey.Right:
                MovePlayer(new Vector2(playerVelocity, 0.0f));
                break;
            case KeyboardKey.Up:
                MovePlayer(new Vector2(0.0f, playerVelocity));
                break;
            case KeyboardKey.Down:
                MovePlayer(new Vector2(0.0f, -playerVelocity));
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render(WindowContext context) { 
        player.RenderEntity(context);
        wall.RenderEntity(context);
    }

    public override void Update() {
    }
}