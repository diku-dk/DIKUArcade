namespace TestDIKUArcade.EntityRotationTest;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;

public class Game : DIKUGame {
    private Entity entity;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
        var image = new Image(@"Assets/Taxi.png");
        entity = new Entity(new DynamicShape(new Vec2F(0.25f,0.25f), new Vec2F(0.5f,0.5f)), image);
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Num_1:
                entity.Shape.Rotate((float)System.Math.PI / 16.0f);
                break;
            case KeyboardKey.Num_2:
                entity.Shape.Rotate((float)System.Math.PI / -16.0f);
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render() { 
        entity.RenderEntity();
    }

    public override void Update() { }
}