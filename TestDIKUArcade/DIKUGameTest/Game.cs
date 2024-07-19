namespace TestDIKUArcade.DIKUGameTest;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;

public class Game : DIKUGame {
    private bool f1Pressed = false;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }
        switch (key) {
            case KeyboardKey.Num_1:
                window.SetClearColor(128, 52, 43);
                break;
            case KeyboardKey.Num_2:
                window.SetClearColor(28, 108, 218);
                break;
            case KeyboardKey.F1:
                f1Pressed = true;
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render() { 
        
    }

    public override void Update() {
        if (f1Pressed) throw new Exception("You pressed F1.");
    }
}