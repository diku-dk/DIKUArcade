namespace TestDIKUArcade.AabbCollisionTest;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

public class Game : DIKUGame {
    private Entity player;
    private Entity wall;

    private float playerVelocity = 0.07f;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        player = new Entity(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(@"Assets/Taxi.png"));
        wall = new Entity(new StationaryShape(new Vec2F(0.25f, 0.5f), new Vec2F(0.15f, 0.15f)),
            new Image(@"Assets/wall.jpeg"));
    }

    public void MovePlayer(Vec2F dir) {
        ((DynamicShape) player.Shape).Direction = dir;
        var collide = CollisionDetection.Aabb((DynamicShape) player.Shape, wall.Shape);
        if (collide.Collision) {
            Console.WriteLine($"CollisionDetection occured in direction {collide.CollisionDir}");
            dir *= collide.DirectionFactor;
        }
        player.Shape.Position += dir;
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Left:
                MovePlayer(new Vec2F(-playerVelocity, 0.0f));
                break;
            case KeyboardKey.Right:
                MovePlayer(new Vec2F(playerVelocity, 0.0f));
                break;
            case KeyboardKey.Up:
                MovePlayer(new Vec2F(0.0f, playerVelocity));
                break;
            case KeyboardKey.Down:
                MovePlayer(new Vec2F(0.0f, -playerVelocity));
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render() { 
        player.RenderEntity();
        wall.RenderEntity();
    }

    public override void Update() {
    }
}