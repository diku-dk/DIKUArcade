namespace TestDIKUArcade.EntityRotationTest;

using System;
using System.Reflection;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.Numerics;

public class Game : DIKUGame {
    private Entity entity;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        var image = new Image("TestDIKUArcade.Assets.Taxi.png");
        entity = new Entity(new DynamicShape(new Vector2(0.25f,0.25f), new Vector2(0.5f,0.5f)), image);
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Num1:
                entity.Shape.Rotate((float)System.Math.PI / 16.0f);
                break;
            case KeyboardKey.Num2:
                entity.Shape.Rotate((float)System.Math.PI / -16.0f);
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render(WindowContext context) { 
        entity.RenderEntity(context);
    }

    public override void Update() { }
}