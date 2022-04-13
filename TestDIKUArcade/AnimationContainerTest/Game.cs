namespace TestDIKUArcade.AnimationContainerTest;

using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;

public class Game : DIKUGame {
    private AnimationContainer container;
    private List<Image> strides;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        container = new AnimationContainer(4);
        strides = ImageStride.CreateStrides(4, @"Assets/PuffOfSmoke.png");
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }
        switch (key) {
            case KeyboardKey.Num_1:
                container.AddAnimation(new StationaryShape(0.0f, 0.0f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Num_2:
                container.AddAnimation(new StationaryShape(0.5f, 0.0f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Num_3:
                container.AddAnimation(new StationaryShape(0.0f, 0.5f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Num_4:
                container.AddAnimation(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), 1000,
                    new ImageStride(80, strides));
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render() { 
        container.RenderAnimations();
    }

    public override void Update() { }
}