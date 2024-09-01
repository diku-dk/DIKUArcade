namespace TestDIKUArcade.AnimationContainerTest;

using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using System.Reflection;

public class Game : DIKUGame {
    private AnimationContainer container;
    private List<Image> strides;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        container = new AnimationContainer(4);
        strides = ImageStride.CreateStrides(4, "TestDIKUArcade.Assets.PuffOfSmoke.png");
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }
        switch (key) {
            case KeyboardKey.Num1:
                container.AddAnimation(new StationaryShape(0.0f, 0.0f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Num2:
                container.AddAnimation(new StationaryShape(0.5f, 0.0f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Num3:
                container.AddAnimation(new StationaryShape(0.0f, 0.5f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Num4:
                container.AddAnimation(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render(WindowContext context) { 
        container.RenderAnimations(context);
    }

    public override void Update() { }
}