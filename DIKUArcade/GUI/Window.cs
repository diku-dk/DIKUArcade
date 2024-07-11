using System;
using System.Drawing.Imaging;
using System.IO;
using DIKUArcade.Input;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Collections;
using System.Collections.Generic;



namespace DIKUArcade.GUI {
    /// <summary>
    /// This class represents a graphical window in the DIKUArcade game engine.
    /// </summary>
    public class Window {

        private bool isRunning;

        /// <summary>
        /// Get or set if this Window instance should be resizable.
        /// </summary>
        public bool Resizable { get; set; } = true;

        /// <summary>
        /// Attach the specified keyHandler method argument to this window object.
        /// All key inputs will thereafter be directed to this keyHandler.
        /// </summary>
        public void SetKeyEventHandler(Action<KeyboardAction,KeyboardKey> keyHandler) {
            
        }

        /// <summary>
        /// Check if the Window is still running.
        /// </summary>
        public bool IsRunning() {
            return isRunning;
        }

        public void PollEvents() {
        }

        public void DestroyWindow() {
        }

        /// <summary>
        /// Sets the window running variable to false such that calls to
        /// `IsRunning()` afterwards will return false. This will allow one
        /// to exit the game loop.
        /// </summary>
        public void CloseWindow() {
            isRunning = false;
        }
    }
}
