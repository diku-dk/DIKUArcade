namespace TestDIKUArcade.AnimationsTest;

using System.Collections.Generic;
using System.Reflection;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;

public class Game : DIKUGame {

    private Animation animation;
    private StationaryShape shape;
    private List<Image> strides;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        shape = new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f);
        strides = ImageStride.CreateStrides(4, "TestDIKUArcade.Assets.PuffOfSmoke.png");
        animation = new Animation() {Duration = 0};
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Space:
                // do something to test animations
                animation = new Animation() {Duration = 1000, Shape = shape};
                animation.Stride = new ImageStride(40, strides);
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render(WindowContext context) { 
        if (animation.IsActive()) {
            animation.RenderAnimation(context);
        }
    }

    public override void Update() { }
}