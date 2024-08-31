namespace TestDIKUArcade.ZoomTest;

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
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Entity(new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.1f, 0.1f)),
            new Image("TestDIKUArcade.Assets.Taxi.png"));
        wall = new Entity(new StationaryShape(new Vector2(0.6f, 0.5f), new Vector2(0.1f, 0.1f)),
            new Image("TestDIKUArcade.Assets.wall.jpeg"));
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        
    }

    public override void Render(WindowContext context) { 
        player.RenderEntity(context);
        wall.RenderEntity(context);
    }
    private float angle = 0;

    public override void Update() {
        angle += 0.1f;
        window.Camera.Scale += new Vector2(0.001f, 0.001f);
        window.Camera.Offset = 0.05f * new Vector2(MathF.Cos(angle), MathF.Sin(angle));
    }
}