using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace DIKUArcade {
    public enum AspectRatio {
        R4X3,
        R16X9,
        R1X1
    }

    /// <summary>
    ///     Create an OpenTK window wrapper, where we only show
    ///     relevant data members and hide unneeded functionality.
    /// </summary>
    public class Window {
        private bool isRunning;
        private uint width, height;

        private GameWindow window;

        public Window(string title, uint width, uint height) {
            this.width = width;
            this.height = height;
            isRunning = true;

            window = new GameWindow((int) width, (int) height,
                GraphicsMode.Default, title);
            window.Run();
        }

        public Window(string title, uint width, AspectRatio aspect) { }

        public bool Resizable { get; set; }

        // This is the signature for a key event handler:
        //private delegate void KeyEventHandler(object sender, KeyboardKeyEventArgs e);

        private void AddKeyUpEventHandler(EventHandler<KeyboardKeyEventArgs> method) {
            window.Keyboard.KeyUp += method;
        }

        private void AddKeyDownEventHandler(EventHandler<KeyboardKeyEventArgs> method) {
            window.Keyboard.KeyDown += method;
        }

        private bool IsRunning() {
            return isRunning;
        }

        private void CloseWindow() {
            isRunning = false;
            window.Close();
        }

        // TODO: Call this DoubleBuffer() instead?
        private void SwapBuffers() { }
    }
}