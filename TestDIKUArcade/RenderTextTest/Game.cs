namespace TestDIKUArcade.RenderTextTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using System.Numerics;

public class Game : DIKUGame {

    private Text text;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        text = new Text("MIN TEXT", new Vector2(0.25f, 0.25f));
        text.SetColor(255, 255, 0, 0);
        text.GetShape().Rotation = (float)System.Math.PI / -3.0f;
        text.GetShape().ScaleXFromCenter(3.2f);
    }


    public override void Render(WindowContext ctx) {
        text.RenderText(ctx);
    }

    public override void Update() { }
}