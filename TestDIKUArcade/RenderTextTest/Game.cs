namespace TestDIKUArcade.RenderTextTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using System.Numerics;
using System;

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

    public override void Update() { }
}