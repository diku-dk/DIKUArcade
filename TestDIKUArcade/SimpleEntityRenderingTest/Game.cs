namespace TestDIKUArcade.SimpleEntityRenderingTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Input;
using DIKUArcade.Math;

public class Game : DIKUGame {

    private Entity actor0;
    private Entity actor1;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        var img0 = new Image(@"Assets/Taxi.png");
        var img1 = new Image(@"Assets/Taxi2.png");

        var imgs0 = new ImageStride(250, img0, img1);
        var imgs1 = new ImageStride(250, img0, img1);

        var shape0 = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.5f, 0.5f));
        var shape1 = new DynamicShape(new Vec2F(0.0f, 0.5f), shape0.Extent.Copy());

        actor0 = new Entity(shape0, imgs0);
        actor1 = new Entity(shape1, imgs1);
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Left:
                actor0.Shape.Position.X -= 0.03f;
                actor1.Shape.Position.X -= 0.03f;
                break;
            case KeyboardKey.Right:
                actor0.Shape.Position.X += 0.03f;
                actor1.Shape.Position.X += 0.03f;
                break;
            case KeyboardKey.KeyPadSubtract:
                actor0.Shape.Scale(1.1f);
                actor1.Shape.Scale(1.1f);
                break;
            case KeyboardKey.KeyPadAdd:
                actor0.Shape.Scale(0.9f);
                actor1.Shape.Scale(0.9f);
                break;
            case KeyboardKey.U:
                actor0.Shape.ScaleXFromCenter(0.9f);
                actor1.Shape.ScaleXFromCenter(0.9f);
                break;
            case KeyboardKey.I:
                actor0.Shape.ScaleXFromCenter(1.1f);
                actor1.Shape.ScaleXFromCenter(1.1f);
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render() {
        actor0.RenderEntity();
        actor1.RenderEntity();
    }

    public override void Update() { }
}