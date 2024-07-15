namespace TestDIKUArcade.RenderTextTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using System.Numerics;
using System;

public class Game : DIKUGame {

    private Text text;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        text = new Text(window, "MY TEXT", new Vector2(0.25f, 0.5f));
        text.SetColor(0, 255, 0, 255);
        text.Shape.Rotation = (float) Math.PI / -3.0f;
        text.Shape.ScaleYFromCenter(7f);
    }


    public override void Render() {
        text.RenderText();
    }

    public override void Update() { }
}