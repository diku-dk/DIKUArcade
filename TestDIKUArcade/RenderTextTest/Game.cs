namespace TestDIKUArcade.RenderTextTest;

using System;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;

public class Game : DIKUGame {

    private Text text;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        text = new Text("MY TEXT", new Vector2(0.25f, 0.5f));
        text.SetColor(0, 255, 0, 255);
        text.Scale *= new Vector2(1, 2.5f);
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
    }

    public override void Render(WindowContext context) {
        text.Render(context);
    }

    public override void Update() {
    }
}