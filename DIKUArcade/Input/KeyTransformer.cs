using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DIKUArcade.Input {
    [System.Obsolete("KeyTransformer class is obsolete! Use IKeyTransformer with globalization insted!", true)]
    public class KeyTransformer {
        /// <summary>
        /// Transform an OpenTK input key to a DIKUArcade key string
        /// </summary>
        public static string GetKeyString(Keys key) {
            var ret = "";
            switch (key) {
            case Keys.Unknown:
                break;
            case Keys.LeftShift:
                ret = "KEY_SHIFT_LEFT";
                break;
            case Keys.RightShift:
                ret = "KEY_SHIFT_RIGHT";
                break;
            case Keys.LeftControl:
                ret = "KEY_CONTROL_LEFT";
                break;
            case Keys.RightControl:
                ret = "KEY_CONTROL_RIGHT";
                break;
            case Keys.LeftAlt:
                ret = "KEY_ALT_LEFT";
                break;
            case Keys.RightAlt:
                ret = "KEY_ALT_RIGHT";
                break;
            case Keys.LeftSuper:
                ret = "KEY_WIN_LEFT";
                break;
            case Keys.RightSuper:
                ret = "KEY_WIN_RIGHT";
                break;
            case Keys.Menu:
                ret = "KEY_MENU";
                break;
            case Keys.F1:
                ret = "KEY_F1";
                break;
            case Keys.F2:
                ret = "KEY_F2";
                break;
            case Keys.F3:
                ret = "KEY_F3";
                break;
            case Keys.F4:
                ret = "KEY_F4";
                break;
            case Keys.F5:
                ret = "KEY_F5";
                break;
            case Keys.F6:
                ret = "KEY_F6";
                break;
            case Keys.F7:
                ret = "KEY_F7";
                break;
            case Keys.F8:
                ret = "KEY_F8";
                break;
            case Keys.F9:
                ret = "KEY_F9";
                break;
            case Keys.F10:
                ret = "KEY_F10";
                break;
            case Keys.F11:
                ret = "KEY_F11";
                break;
            case Keys.F12:
                ret = "KEY_F12";
                break;
            case Keys.F13:
                ret = "KEY_F13";
                break;
            case Keys.F14:
                ret = "KEY_F14";
                break;
            case Keys.F15:
                ret = "KEY_F15";
                break;
            case Keys.F16:
                ret = "KEY_F16";
                break;
            case Keys.F17:
                ret = "KEY_F17";
                break;
            case Keys.F18:
                ret = "KEY_F18";
                break;
            case Keys.F19:
                ret = "KEY_F19";
                break;
            case Keys.F20:
                ret = "KEY_F20";
                break;
            case Keys.F21:
                ret = "KEY_F21";
                break;
            case Keys.F22:
                ret = "KEY_F22";
                break;
            case Keys.F23:
                ret = "KEY_F23";
                break;
            case Keys.F24:
                ret = "KEY_F24";
                break;
            case Keys.F25:
                ret = "KEY_F25";
                break;
            case Keys.Up:
                ret = "KEY_UP";
                break;
            case Keys.Down:
                ret = "KEY_DOWN";
                break;
            case Keys.Left:
                ret = "KEY_LEFT";
                break;
            case Keys.Right:
                ret = "KEY_RIGHT";
                break;
            case Keys.Enter:
                ret = "KEY_ENTER";
                break;
            case Keys.Escape:
                ret = "KEY_ESCAPE";
                break;
            case Keys.Space:
                ret = "KEY_SPACE";
                break;
            case Keys.Tab:
                ret = "KEY_TAB";
                break;
            case Keys.Backspace:
                ret = "KEY_BACKSPACE";
                break;
            case Keys.Insert:
                ret = "KEY_INSERT";
                break;
            case Keys.Delete:
                ret = "KEY_DELETE";
                break;
            case Keys.PageUp:
                ret = "KEY_PAGEUP";
                break;
            case Keys.PageDown:
                ret = "KEY_PAGEDOWN";
                break;
            case Keys.Home:
                ret = "KEY_HOME";
                break;
            case Keys.End:
                ret = "KEY_END";
                break;
            case Keys.CapsLock:
                ret = "KEY_CAPSLOCK";
                break;
            case Keys.ScrollLock:
                ret = "KEY_SCROLL_LOCK";
                break;
            case Keys.PrintScreen:
                ret = "KEY_PRINTSCREEN";
                break;
            case Keys.Pause:
                ret = "KEY_PAUSE";
                break;
            case Keys.NumLock:
                ret = "KEY_NUMLOCK";
                break;
            case Keys.KeyPad0:
                ret = "KEY_KEYPAD_0";
                break;
            case Keys.KeyPad1:
                ret = "KEY_KEYPAD_1";
                break;
            case Keys.KeyPad2:
                ret = "KEY_KEYPAD_2";
                break;
            case Keys.KeyPad3:
                ret = "KEY_KEYPAD_3";
                break;
            case Keys.KeyPad4:
                ret = "KEY_KEYPAD_4";
                break;
            case Keys.KeyPad5:
                ret = "KEY_KEYPAD_5";
                break;
            case Keys.KeyPad6:
                ret = "KEY_KEYPAD_6";
                break;
            case Keys.KeyPad7:
                ret = "KEY_KEYPAD_7";
                break;
            case Keys.KeyPad8:
                ret = "KEY_KEYPAD_8";
                break;
            case Keys.KeyPad9:
                ret = "KEY_KEYPAD_9";
                break;
            case Keys.KeyPadDivide:
                ret = "KEY_KEYPAD_DIVIDE";
                break;
            case Keys.KeyPadMultiply:
                ret = "KEY_KEYPAD_MULTIPLY";
                break;
            case Keys.KeyPadSubtract:
                ret = "KEY_KEYPAD_SUBTRACT";
                break;
            case Keys.KeyPadAdd:
                ret = "KEY_KEYPAD_ADD";
                break;
            case Keys.KeyPadDecimal:
                ret = "KEY_KEYPAD_DECIMAL";
                break;
            case Keys.KeyPadEnter:
                ret = "KEY_KEYPAD_ENTER";
                break;
            case Keys.A:
                ret = "KEY_A";
                break;
            case Keys.B:
                ret = "KEY_B";
                break;
            case Keys.C:
                ret = "KEY_C";
                break;
            case Keys.D:
                ret = "KEY_D";
                break;
            case Keys.E:
                ret = "KEY_E";
                break;
            case Keys.F:
                ret = "KEY_F";
                break;
            case Keys.G:
                ret = "KEY_G";
                break;
            case Keys.H:
                ret = "KEY_H";
                break;
            case Keys.I:
                ret = "KEY_I";
                break;
            case Keys.J:
                ret = "KEY_J";
                break;
            case Keys.K:
                ret = "KEY_K";
                break;
            case Keys.L:
                ret = "KEY_L";
                break;
            case Keys.M:
                ret = "KEY_M";
                break;
            case Keys.N:
                ret = "KEY_N";
                break;
            case Keys.O:
                ret = "KEY_O";
                break;
            case Keys.P:
                ret = "KEY_P";
                break;
            case Keys.Q:
                ret = "KEY_Q";
                break;
            case Keys.R:
                ret = "KEY_R";
                break;
            case Keys.S:
                ret = "KEY_S";
                break;
            case Keys.T:
                ret = "KEY_T";
                break;
            case Keys.U:
                ret = "KEY_U";
                break;
            case Keys.V:
                ret = "KEY_V";
                break;
            case Keys.W:
                ret = "KEY_W";
                break;
            case Keys.X:
                ret = "KEY_X";
                break;
            case Keys.Y:
                ret = "KEY_Y";
                break;
            case Keys.Z:
                ret = "KEY_Z";
                break;
            case Keys.D0:
                ret = "KEY_0";
                break;
            case Keys.D1:
                ret = "KEY_1";
                break;
            case Keys.D2:
                ret = "KEY_2";
                break;
            case Keys.D3:
                ret = "KEY_3";
                break;
            case Keys.D4:
                ret = "KEY_4";
                break;
            case Keys.D5:
                ret = "KEY_5";
                break;
            case Keys.D6:
                ret = "KEY_6";
                break;
            case Keys.D7:
                ret = "KEY_7";
                break;
            case Keys.D8:
                ret = "KEY_8";
                break;
            case Keys.D9:
                ret = "KEY_9";
                break;
            case Keys.Minus:
                ret = "KEY_MINUS";
                break;
                /* // TODO: FIGURE OUT!!
            case Keys
                ret = "KEY_PLUS";
                break;
                */
            case Keys.LeftBracket:
                ret = "KEY_BRACKET_LEFT";
                break;
            case Keys.RightBracket:
                ret = "KEY_BRACKET_RIGHT";
                break;
            case Keys.Semicolon:
                ret = "KEY_SEMICOLON";
                break;
                /* // TODO: FIGURE OUT!!
            case Keys.
                ret = "KEY_QUOTE";
                break;
                */
            case Keys.Comma:
                ret = "KEY_COMMA";
                break;
            case Keys.Period:
                ret = "KEY_PERIOD";
                break;
            case Keys.Slash:
                ret = "KEY_SLASH";
                break;
            case Keys.Backslash:
                ret = "KEY_BACKSLASH";
                break;
                /* // TODO: FIGURE OUT!!
            case Keys
                ret = "KEY_NONUSBACKSLASH";
                break;
                */
                /* // TODO: FIGURE OUT!!
            case Keys.LastKey:
                ret = "KEY_LASTKEY";
                break;
                */
            default:
                break;
            }

            return ret;
        }
    }
}

