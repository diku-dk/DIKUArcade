namespace TestDIKUArcade.RenderTextTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using System.Numerics;
using System;
using DIKUArcade.Entities;

public class Game : DIKUGame {

    private Text text;
    private StationaryShape shape;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        text = new Text("MY TEXT");
        shape = new StationaryShape(new Vector2(0.25f, 0.5f), Vector2.One);
        text.SetColor(0, 255, 0, 255);
    }


    public override void Render() {
        shape.Position = new Vector2(0.25f, 0.5f);
        shape.Extent = text.IdealExtent(window.Width, window.Height);
        shape.ScaleYFromCenter(7f);
        text.Render(shape);
    }

    public override void Update() { }
}