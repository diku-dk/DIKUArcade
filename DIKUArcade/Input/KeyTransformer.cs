namespace DIKUArcade.Input;

using System;

/// <summary>
/// Transforms low-level keyboard keys and actions into high-level keyboard keys and actions.
/// </summary>
public class KeyTransformer : IKeyTransformer<Lowlevel.KeyboardKey, Lowlevel.KeyAction>
{
    /// <summary>
    /// Transforms a low-level keyboard key into a high-level keyboard key.
    /// </summary>
    /// <param name="key">The low-level keyboard key to transform.</param>
    /// <returns>The corresponding high-level keyboard key.</returns>
    public KeyboardKey TransformKey(Lowlevel.KeyboardKey key)
    {
        switch (key)
        {
            case Lowlevel.KeyboardKey.Unknown: return KeyboardKey.Unknown;
            case Lowlevel.KeyboardKey.Space: return KeyboardKey.Space;
            case Lowlevel.KeyboardKey.Apostrophe: return KeyboardKey.Apostrophe;
            case Lowlevel.KeyboardKey.Comma: return KeyboardKey.Comma;
            case Lowlevel.KeyboardKey.Minus: return KeyboardKey.Minus;
            case Lowlevel.KeyboardKey.Plus: return KeyboardKey.Plus;
            case Lowlevel.KeyboardKey.Period: return KeyboardKey.Period;
            case Lowlevel.KeyboardKey.Slash: return KeyboardKey.Slash;
            case Lowlevel.KeyboardKey.Num0: return KeyboardKey.Num0;
            case Lowlevel.KeyboardKey.Num1: return KeyboardKey.Num1;
            case Lowlevel.KeyboardKey.Num2: return KeyboardKey.Num2;
            case Lowlevel.KeyboardKey.Num3: return KeyboardKey.Num3;
            case Lowlevel.KeyboardKey.Num4: return KeyboardKey.Num4;
            case Lowlevel.KeyboardKey.Num5: return KeyboardKey.Num5;
            case Lowlevel.KeyboardKey.Num6: return KeyboardKey.Num6;
            case Lowlevel.KeyboardKey.Num7: return KeyboardKey.Num7;
            case Lowlevel.KeyboardKey.Num8: return KeyboardKey.Num8;
            case Lowlevel.KeyboardKey.Num9: return KeyboardKey.Num9;
            case Lowlevel.KeyboardKey.Semicolon: return KeyboardKey.Semicolon;
            case Lowlevel.KeyboardKey.Equal: return KeyboardKey.Equal;
            case Lowlevel.KeyboardKey.A: return KeyboardKey.A;
            case Lowlevel.KeyboardKey.B: return KeyboardKey.B;
            case Lowlevel.KeyboardKey.C: return KeyboardKey.C;
            case Lowlevel.KeyboardKey.D: return KeyboardKey.D;
            case Lowlevel.KeyboardKey.E: return KeyboardKey.E;
            case Lowlevel.KeyboardKey.F: return KeyboardKey.F;
            case Lowlevel.KeyboardKey.G: return KeyboardKey.G;
            case Lowlevel.KeyboardKey.H: return KeyboardKey.H;
            case Lowlevel.KeyboardKey.I: return KeyboardKey.I;
            case Lowlevel.KeyboardKey.J: return KeyboardKey.J;
            case Lowlevel.KeyboardKey.K: return KeyboardKey.K;
            case Lowlevel.KeyboardKey.L: return KeyboardKey.L;
            case Lowlevel.KeyboardKey.M: return KeyboardKey.M;
            case Lowlevel.KeyboardKey.N: return KeyboardKey.N;
            case Lowlevel.KeyboardKey.O: return KeyboardKey.O;
            case Lowlevel.KeyboardKey.P: return KeyboardKey.P;
            case Lowlevel.KeyboardKey.Q: return KeyboardKey.Q;
            case Lowlevel.KeyboardKey.R: return KeyboardKey.R;
            case Lowlevel.KeyboardKey.S: return KeyboardKey.S;
            case Lowlevel.KeyboardKey.T: return KeyboardKey.T;
            case Lowlevel.KeyboardKey.U: return KeyboardKey.U;
            case Lowlevel.KeyboardKey.V: return KeyboardKey.V;
            case Lowlevel.KeyboardKey.W: return KeyboardKey.W;
            case Lowlevel.KeyboardKey.X: return KeyboardKey.X;
            case Lowlevel.KeyboardKey.Y: return KeyboardKey.Y;
            case Lowlevel.KeyboardKey.Z: return KeyboardKey.Z;
            case Lowlevel.KeyboardKey.LeftBracket: return KeyboardKey.LeftBracket;
            case Lowlevel.KeyboardKey.Backslash: return KeyboardKey.Backslash;
            case Lowlevel.KeyboardKey.RightBracket: return KeyboardKey.RightBracket;
            case Lowlevel.KeyboardKey.GraveAccent: return KeyboardKey.GraveAccent;
            case Lowlevel.KeyboardKey.AcuteAccent: return KeyboardKey.AcuteAccent;
            case Lowlevel.KeyboardKey.Escape: return KeyboardKey.Escape;
            case Lowlevel.KeyboardKey.Enter: return KeyboardKey.Enter;
            case Lowlevel.KeyboardKey.Tab: return KeyboardKey.Tab;
            case Lowlevel.KeyboardKey.Backspace: return KeyboardKey.Backspace;
            case Lowlevel.KeyboardKey.Insert: return KeyboardKey.Insert;
            case Lowlevel.KeyboardKey.Delete: return KeyboardKey.Delete;
            case Lowlevel.KeyboardKey.Right: return KeyboardKey.Right;
            case Lowlevel.KeyboardKey.Left: return KeyboardKey.Left;
            case Lowlevel.KeyboardKey.Down: return KeyboardKey.Down;
            case Lowlevel.KeyboardKey.Up: return KeyboardKey.Up;
            case Lowlevel.KeyboardKey.PageUp: return KeyboardKey.PageUp;
            case Lowlevel.KeyboardKey.PageDown: return KeyboardKey.PageDown;
            case Lowlevel.KeyboardKey.Home: return KeyboardKey.Home;
            case Lowlevel.KeyboardKey.End: return KeyboardKey.End;
            case Lowlevel.KeyboardKey.CapsLock: return KeyboardKey.CapsLock;
            case Lowlevel.KeyboardKey.ScrollLock: return KeyboardKey.ScrollLock;
            case Lowlevel.KeyboardKey.NumLock: return KeyboardKey.NumLock;
            case Lowlevel.KeyboardKey.PrintScreen: return KeyboardKey.PrintScreen;
            case Lowlevel.KeyboardKey.Pause: return KeyboardKey.Pause;
            case Lowlevel.KeyboardKey.F1: return KeyboardKey.F1;
            case Lowlevel.KeyboardKey.F2: return KeyboardKey.F2;
            case Lowlevel.KeyboardKey.F3: return KeyboardKey.F3;
            case Lowlevel.KeyboardKey.F4: return KeyboardKey.F4;
            case Lowlevel.KeyboardKey.F5: return KeyboardKey.F5;
            case Lowlevel.KeyboardKey.F6: return KeyboardKey.F6;
            case Lowlevel.KeyboardKey.F7: return KeyboardKey.F7;
            case Lowlevel.KeyboardKey.F8: return KeyboardKey.F8;
            case Lowlevel.KeyboardKey.F9: return KeyboardKey.F9;
            case Lowlevel.KeyboardKey.F10: return KeyboardKey.F10;
            case Lowlevel.KeyboardKey.F11: return KeyboardKey.F11;
            case Lowlevel.KeyboardKey.F12: return KeyboardKey.F12;
            case Lowlevel.KeyboardKey.KeyPad0: return KeyboardKey.KeyPad0;
            case Lowlevel.KeyboardKey.KeyPad1: return KeyboardKey.KeyPad1;
            case Lowlevel.KeyboardKey.KeyPad2: return KeyboardKey.KeyPad2;
            case Lowlevel.KeyboardKey.KeyPad3: return KeyboardKey.KeyPad3;
            case Lowlevel.KeyboardKey.KeyPad4: return KeyboardKey.KeyPad4;
            case Lowlevel.KeyboardKey.KeyPad5: return KeyboardKey.KeyPad5;
            case Lowlevel.KeyboardKey.KeyPad6: return KeyboardKey.KeyPad6;
            case Lowlevel.KeyboardKey.KeyPad7: return KeyboardKey.KeyPad7;
            case Lowlevel.KeyboardKey.KeyPad8: return KeyboardKey.KeyPad8;
            case Lowlevel.KeyboardKey.KeyPad9: return KeyboardKey.KeyPad9;
            case Lowlevel.KeyboardKey.KeyPadDecimal: return KeyboardKey.KeyPadDecimal;
            case Lowlevel.KeyboardKey.KeyPadDivide: return KeyboardKey.KeyPadDivide;
            case Lowlevel.KeyboardKey.KeyPadMultiply: return KeyboardKey.KeyPadMultiply;
            case Lowlevel.KeyboardKey.KeyPadSubtract: return KeyboardKey.KeyPadSubtract;
            case Lowlevel.KeyboardKey.KeyPadAdd: return KeyboardKey.KeyPadAdd;
            case Lowlevel.KeyboardKey.KeyPadEnter: return KeyboardKey.KeyPadEnter;
            case Lowlevel.KeyboardKey.KeyPadEqual: return KeyboardKey.KeyPadEqual;
            case Lowlevel.KeyboardKey.LeftShift: return KeyboardKey.LeftShift;
            case Lowlevel.KeyboardKey.LeftControl: return KeyboardKey.LeftControl;
            case Lowlevel.KeyboardKey.LeftAlt: return KeyboardKey.LeftAlt;
            case Lowlevel.KeyboardKey.LeftSuper: return KeyboardKey.LeftSuper;
            case Lowlevel.KeyboardKey.RightShift: return KeyboardKey.RightShift;
            case Lowlevel.KeyboardKey.RightControl: return KeyboardKey.RightControl;
            case Lowlevel.KeyboardKey.RightAlt: return KeyboardKey.RightAlt;
            case Lowlevel.KeyboardKey.RightSuper: return KeyboardKey.RightSuper;
            case Lowlevel.KeyboardKey.Menu: return KeyboardKey.Menu;
            case Lowlevel.KeyboardKey.Diaresis: return KeyboardKey.Diaresis;
            case Lowlevel.KeyboardKey.LessThan: return KeyboardKey.LessThan;
            case Lowlevel.KeyboardKey.GreaterThan: return KeyboardKey.GreaterThan;
            case Lowlevel.KeyboardKey.FractionOneHalf: return KeyboardKey.FractionOneHalf;
            case Lowlevel.KeyboardKey.DanishAA: return KeyboardKey.DanishAA;
            case Lowlevel.KeyboardKey.DanishAE: return KeyboardKey.DanishAE;
            case Lowlevel.KeyboardKey.DanishOE: return KeyboardKey.DanishOE;
            default: break;
        }

        return KeyboardKey.Unknown;
    }

    /// <summary>
    /// Transforms a low-level key action into a high-level keyboard action.
    /// </summary>
    /// <param name="action">The low-level key action to transform.</param>
    /// <returns>The corresponding high-level keyboard action.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided action is not recognized.</exception>
    public KeyboardAction TransformAction(Lowlevel.KeyAction action)
    {
        switch (action)
        {
            case Lowlevel.KeyAction.KeyPress: return KeyboardAction.KeyPress;
            case Lowlevel.KeyAction.KeyRelease: return KeyboardAction.KeyRelease; // Fixed to map to KeyRelease
            default: break;
        }

        throw new ArgumentException(nameof(action));
    }
}
