using OpenTK.Windowing.GraphicsLibraryFramework;
using DIKUArcade.Input;
using System;

namespace DIKUArcade.Input.Languages
{
    /// <summary>
    /// Represents the Danish keyboard layout.
    /// </summary>
    public class DanishKeyTransformer : IKeyTransformer
    {
        public KeyboardKey TransformKey(Keys key)
        {
            switch (key)
            {
                case Keys.Unknown: return KeyboardKey.Unknown;
                case Keys.Space: return KeyboardKey.Space;
                case Keys.Apostrophe: return KeyboardKey.Danish_OE;
                case Keys.Comma: return KeyboardKey.Comma;
                case Keys.Minus: return KeyboardKey.Plus;
                case Keys.Period: return KeyboardKey.Period;
                case Keys.Slash: return KeyboardKey.Minus;

                case Keys.D0: return KeyboardKey.Num_0;
                case Keys.D1: return KeyboardKey.Num_1;
                case Keys.D2: return KeyboardKey.Num_2;
                case Keys.D3: return KeyboardKey.Num_3;
                case Keys.D4: return KeyboardKey.Num_4;
                case Keys.D5: return KeyboardKey.Num_5;
                case Keys.D6: return KeyboardKey.Num_6;
                case Keys.D7: return KeyboardKey.Num_7;
                case Keys.D8: return KeyboardKey.Num_8;
                case Keys.D9: return KeyboardKey.Num_9;
                case Keys.Semicolon: return KeyboardKey.Danish_AE;
                case Keys.Equal: return KeyboardKey.AcuteAccent;

                case Keys.A: return KeyboardKey.A;
                case Keys.B: return KeyboardKey.B;
                case Keys.C: return KeyboardKey.C;
                case Keys.D: return KeyboardKey.D;
                case Keys.E: return KeyboardKey.E;
                case Keys.F: return KeyboardKey.F;
                case Keys.G: return KeyboardKey.G;
                case Keys.H: return KeyboardKey.H;
                case Keys.I: return KeyboardKey.I;
                case Keys.J: return KeyboardKey.J;
                case Keys.K: return KeyboardKey.K;
                case Keys.L: return KeyboardKey.L;
                case Keys.M: return KeyboardKey.M;
                case Keys.N: return KeyboardKey.N;
                case Keys.O: return KeyboardKey.O;
                case Keys.P: return KeyboardKey.P;
                case Keys.Q: return KeyboardKey.Q;
                case Keys.R: return KeyboardKey.R;
                case Keys.S: return KeyboardKey.S;
                case Keys.T: return KeyboardKey.T;
                case Keys.U: return KeyboardKey.U;
                case Keys.V: return KeyboardKey.V;
                case Keys.W: return KeyboardKey.W;
                case Keys.X: return KeyboardKey.X;
                case Keys.Y: return KeyboardKey.Y;
                case Keys.Z: return KeyboardKey.Z;

                case Keys.LeftBracket: return KeyboardKey.Danish_AA;
                case Keys.Backslash: return KeyboardKey.Apostrophe;
                case Keys.RightBracket: return KeyboardKey.Diaresis;
                case Keys.GraveAccent: return KeyboardKey.FractionOneHalf;
                case Keys.Escape: return KeyboardKey.Escape;
                case Keys.Enter: return KeyboardKey.Enter;
                case Keys.Tab: return KeyboardKey.Tab;
                case Keys.Backspace: return KeyboardKey.Backspace;
                case Keys.Insert: return KeyboardKey.Insert;
                case Keys.Delete: return KeyboardKey.Delete;

                case Keys.Right: return KeyboardKey.Right;
                case Keys.Left: return KeyboardKey.Left;
                case Keys.Down: return KeyboardKey.Down;
                case Keys.Up: return KeyboardKey.Up;

                case Keys.PageUp: return KeyboardKey.PageUp;
                case Keys.PageDown: return KeyboardKey.PageDown;
                case Keys.Home: return KeyboardKey.Home;
                case Keys.End: return KeyboardKey.End;

                case Keys.CapsLock: return KeyboardKey.CapsLock;
                case Keys.ScrollLock: return KeyboardKey.ScrollLock;
                case Keys.NumLock: return KeyboardKey.NumLock;
                case Keys.PrintScreen: return KeyboardKey.PrintScreen;
                case Keys.Pause: return KeyboardKey.Pause;

                case Keys.F1: return KeyboardKey.F1;
                case Keys.F2: return KeyboardKey.F2;
                case Keys.F3: return KeyboardKey.F3;
                case Keys.F4: return KeyboardKey.F4;
                case Keys.F5: return KeyboardKey.F5;
                case Keys.F6: return KeyboardKey.F6;
                case Keys.F7: return KeyboardKey.F7;
                case Keys.F8: return KeyboardKey.F8;
                case Keys.F9: return KeyboardKey.F9;
                case Keys.F10: return KeyboardKey.F10;
                case Keys.F11: return KeyboardKey.F11;
                case Keys.F12: return KeyboardKey.F12;
                case Keys.F13: return KeyboardKey.F13;
                case Keys.F14: return KeyboardKey.F14;
                case Keys.F15: return KeyboardKey.F15;
                case Keys.F16: return KeyboardKey.F16;
                case Keys.F17: return KeyboardKey.F17;
                case Keys.F18: return KeyboardKey.F18;
                case Keys.F19: return KeyboardKey.F19;
                case Keys.F20: return KeyboardKey.F20;
                case Keys.F21: return KeyboardKey.F21;
                case Keys.F22: return KeyboardKey.F22;
                case Keys.F23: return KeyboardKey.F23;
                case Keys.F24: return KeyboardKey.F24;
                case Keys.F25: return KeyboardKey.F25;

                case Keys.KeyPad0: return KeyboardKey.KeyPad0;
                case Keys.KeyPad1: return KeyboardKey.KeyPad1;
                case Keys.KeyPad2: return KeyboardKey.KeyPad2;
                case Keys.KeyPad3: return KeyboardKey.KeyPad3;
                case Keys.KeyPad4: return KeyboardKey.KeyPad4;
                case Keys.KeyPad5: return KeyboardKey.KeyPad5;
                case Keys.KeyPad6: return KeyboardKey.KeyPad6;
                case Keys.KeyPad7: return KeyboardKey.KeyPad7;
                case Keys.KeyPad8: return KeyboardKey.KeyPad8;
                case Keys.KeyPad9: return KeyboardKey.KeyPad9;
                case Keys.KeyPadDecimal: return KeyboardKey.KeyPadDecimal;
                case Keys.KeyPadDivide: return KeyboardKey.KeyPadDivide;
                case Keys.KeyPadMultiply: return KeyboardKey.KeyPadMultiply;
                case Keys.KeyPadSubtract: return KeyboardKey.KeyPadSubtract;
                case Keys.KeyPadAdd: return KeyboardKey.KeyPadAdd;
                case Keys.KeyPadEnter: return KeyboardKey.KeyPadEnter;
                case Keys.KeyPadEqual: return KeyboardKey.KeyPadEqual;

                case Keys.LeftShift: return KeyboardKey.LeftShift;
                case Keys.LeftControl: return KeyboardKey.LeftControl;
                case Keys.LeftAlt: return KeyboardKey.LeftAlt;
                case Keys.LeftSuper: return KeyboardKey.LeftSuper;
                case Keys.RightShift: return KeyboardKey.RightShift;
                case Keys.RightControl: return KeyboardKey.RightControl;
                case Keys.RightAlt: return KeyboardKey.RightAlt;
                case Keys.RightSuper: return KeyboardKey.RightSuper;
                case Keys.Menu: return KeyboardKey.Menu;

                default:
                    break;
            }

            // special case for Danish keyboard layout, since this key is not given a
            // name by OpenTK 4.5.
            if ((int)key == 161) { return KeyboardKey.LessThan; }
            else { return KeyboardKey.Unknown; }
        }
    }
}
