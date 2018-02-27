using OpenTK.Input;

namespace DIKUArcade.Input {
    public class KeyTransformer {
        /// <summary>
        /// Transform an OpenTK input key to a DIKUArcade key string
        /// </summary>
        public static string GetKeyString(Key key) {
            var ret = "";
            switch (key) {
            case Key.Unknown:
                break;
            case Key.ShiftLeft:
                ret = "KEY_SHIFT_LEFT";
                break;
            case Key.ShiftRight:
                ret = "KEY_SHIFT_RIGHT";
                break;
            case Key.ControlLeft:
                ret = "KEY_CONTROL_LEFT";
                break;
            case Key.ControlRight:
                ret = "KEY_CONTROL_RIGHT";
                break;
            case Key.AltLeft:
                ret = "KEY_ALT_LEFT";
                break;
            case Key.AltRight:
                ret = "KEY_ALT_RIGHT";
                break;
            case Key.WinLeft:
                ret = "KEY_WIN_LEFT";
                break;
            case Key.WinRight:
                ret = "KEY_WIN_RIGHT";
                break;
            case Key.Menu:
                ret = "KEY_MENU";
                break;
            case Key.F1:
                ret = "KEY_F1";
                break;
            case Key.F2:
                ret = "KEY_F2";
                break;
            case Key.F3:
                ret = "KEY_F3";
                break;
            case Key.F4:
                ret = "KEY_F4";
                break;
            case Key.F5:
                ret = "KEY_F5";
                break;
            case Key.F6:
                ret = "KEY_F6";
                break;
            case Key.F7:
                ret = "KEY_F7";
                break;
            case Key.F8:
                ret = "KEY_F8";
                break;
            case Key.F9:
                ret = "KEY_F9";
                break;
            case Key.F10:
                ret = "KEY_F10";
                break;
            case Key.F11:
                ret = "KEY_F11";
                break;
            case Key.F12:
                ret = "KEY_F12";
                break;
            case Key.F13:
                ret = "KEY_F13";
                break;
            case Key.F14:
                ret = "KEY_F14";
                break;
            case Key.F15:
                ret = "KEY_F15";
                break;
            case Key.F16:
                ret = "KEY_F16";
                break;
            case Key.F17:
                ret = "KEY_F17";
                break;
            case Key.F18:
                ret = "KEY_F18";
                break;
            case Key.F19:
                ret = "KEY_F19";
                break;
            case Key.F20:
                ret = "KEY_F20";
                break;
            case Key.F21:
                ret = "KEY_F21";
                break;
            case Key.F22:
                ret = "KEY_F22";
                break;
            case Key.F23:
                ret = "KEY_F23";
                break;
            case Key.F24:
                ret = "KEY_F24";
                break;
            case Key.F25:
                ret = "KEY_F25";
                break;
            case Key.F26:
                ret = "KEY_F26";
                break;
            case Key.F27:
                ret = "KEY_F27";
                break;
            case Key.F28:
                ret = "KEY_F28";
                break;
            case Key.F29:
                ret = "KEY_F29";
                break;
            case Key.F30:
                ret = "KEY_F30";
                break;
            case Key.F31:
                ret = "KEY_F31";
                break;
            case Key.F32:
                ret = "KEY_F32";
                break;
            case Key.F33:
                ret = "KEY_F33";
                break;
            case Key.F34:
                ret = "KEY_F34";
                break;
            case Key.F35:
                ret = "KEY_F35";
                break;
            case Key.Up:
                ret = "KEY_UP";
                break;
            case Key.Down:
                ret = "KEY_DOWN";
                break;
            case Key.Left:
                ret = "KEY_LEFT";
                break;
            case Key.Right:
                ret = "KEY_RIGHT";
                break;
            case Key.Enter:
                ret = "KEY_ENTER";
                break;
            case Key.Escape:
                ret = "KEY_ESCAPE";
                break;
            case Key.Space:
                ret = "KEY_SPACE";
                break;
            case Key.Tab:
                ret = "KEY_TAB";
                break;
            case Key.BackSpace:
                ret = "KEY_BACKSPACE";
                break;
            case Key.Insert:
                ret = "KEY_INSERT";
                break;
            case Key.Delete:
                ret = "KEY_DELETE";
                break;
            case Key.PageUp:
                ret = "KEY_PAGEUP";
                break;
            case Key.PageDown:
                ret = "KEY_PAGEDOWN";
                break;
            case Key.Home:
                ret = "KEY_HOME";
                break;
            case Key.End:
                ret = "KEY_END";
                break;
            case Key.CapsLock:
                ret = "KEY_CAPSLOCK";
                break;
            case Key.ScrollLock:
                ret = "KEY_SCROLL_LOCK";
                break;
            case Key.PrintScreen:
                ret = "KEY_PRINTSCREEN";
                break;
            case Key.Pause:
                ret = "KEY_PAUSE";
                break;
            case Key.NumLock:
                ret = "KEY_NUMLOCK";
                break;
            case Key.Clear:
                ret = "KEY_CLEAR";
                break;
            case Key.Sleep:
                ret = "KEY_SLEEP";
                break;
            case Key.Keypad0:
                ret = "KEY_KEYPAD_0";
                break;
            case Key.Keypad1:
                ret = "KEY_KEYPAD_1";
                break;
            case Key.Keypad2:
                ret = "KEY_KEYPAD_2";
                break;
            case Key.Keypad3:
                ret = "KEY_KEYPAD_3";
                break;
            case Key.Keypad4:
                ret = "KEY_KEYPAD_4";
                break;
            case Key.Keypad5:
                ret = "KEY_KEYPAD_5";
                break;
            case Key.Keypad6:
                ret = "KEY_KEYPAD_6";
                break;
            case Key.Keypad7:
                ret = "KEY_KEYPAD_7";
                break;
            case Key.Keypad8:
                ret = "KEY_KEYPAD_8";
                break;
            case Key.Keypad9:
                ret = "KEY_KEYPAD_9";
                break;
            case Key.KeypadDivide:
                ret = "KEY_KEYPAD_DIVIDE";
                break;
            case Key.KeypadMultiply:
                ret = "KEY_KEYPAD_MULTIPLY";
                break;
            case Key.KeypadSubtract:
                ret = "KEY_KEYPAD_SUBTRACT";
                break;
            case Key.KeypadAdd:
                ret = "KEY_KEYPAD_ADD";
                break;
            case Key.KeypadDecimal:
                ret = "KEY_KEYPAD_DECIMAL";
                break;
            case Key.KeypadEnter:
                ret = "KEY_KEYPAD_ENTER";
                break;
            case Key.A:
                ret = "KEY_A";
                break;
            case Key.B:
                ret = "KEY_B";
                break;
            case Key.C:
                ret = "KEY_C";
                break;
            case Key.D:
                ret = "KEY_D";
                break;
            case Key.E:
                ret = "KEY_E";
                break;
            case Key.F:
                ret = "KEY_F";
                break;
            case Key.G:
                ret = "KEY_G";
                break;
            case Key.H:
                ret = "KEY_H";
                break;
            case Key.I:
                ret = "KEY_I";
                break;
            case Key.J:
                ret = "KEY_J";
                break;
            case Key.K:
                ret = "KEY_K";
                break;
            case Key.L:
                ret = "KEY_L";
                break;
            case Key.M:
                ret = "KEY_M";
                break;
            case Key.N:
                ret = "KEY_N";
                break;
            case Key.O:
                ret = "KEY_O";
                break;
            case Key.P:
                ret = "KEY_P";
                break;
            case Key.Q:
                ret = "KEY_Q";
                break;
            case Key.R:
                ret = "KEY_R";
                break;
            case Key.S:
                ret = "KEY_S";
                break;
            case Key.T:
                ret = "KEY_T";
                break;
            case Key.U:
                ret = "KEY_U";
                break;
            case Key.V:
                ret = "KEY_V";
                break;
            case Key.W:
                ret = "KEY_W";
                break;
            case Key.X:
                ret = "KEY_X";
                break;
            case Key.Y:
                ret = "KEY_Y";
                break;
            case Key.Z:
                ret = "KEY_Z";
                break;
            case Key.Number0:
                ret = "KEY_0";
                break;
            case Key.Number1:
                ret = "KEY_1";
                break;
            case Key.Number2:
                ret = "KEY_2";
                break;
            case Key.Number3:
                ret = "KEY_3";
                break;
            case Key.Number4:
                ret = "KEY_4";
                break;
            case Key.Number5:
                ret = "KEY_5";
                break;
            case Key.Number6:
                ret = "KEY_6";
                break;
            case Key.Number7:
                ret = "KEY_7";
                break;
            case Key.Number8:
                ret = "KEY_8";
                break;
            case Key.Number9:
                ret = "KEY_9";
                break;
            case Key.Tilde:
                ret = "KEY_TILDE";
                break;
            case Key.Minus:
                ret = "KEY_MINUS";
                break;
            case Key.Plus:
                ret = "KEY_PLUS";
                break;
            case Key.BracketLeft:
                ret = "KEY_BRACKET_LEFT";
                break;
            case Key.BracketRight:
                ret = "KEY_BRACKET_RIGHT";
                break;
            case Key.Semicolon:
                ret = "KEY_SEMICOLON";
                break;
            case Key.Quote:
                ret = "KEY_QUOTE";
                break;
            case Key.Comma:
                ret = "KEY_COMMA";
                break;
            case Key.Period:
                ret = "KEY_PERIOD";
                break;
            case Key.Slash:
                ret = "KEY_SLASH";
                break;
            case Key.BackSlash:
                ret = "KEY_BACKSLASH";
                break;
            case Key.NonUSBackSlash:
                ret = "KEY_NONUSBACKSLASH";
                break;
            case Key.LastKey:
                ret = "KEY_LASTKEY";
                break;
            default:
                break;
            }

            return ret;
        }
    }
}

