using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

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
        private static uint screenShotCounter;

        private readonly GameWindow window;

        // This is the signature for a key event handler:
        //private delegate void KeyEventHandler(object sender, KeyboardKeyEventArgs e);
        private EventHandler<KeyboardKeyEventArgs> defaultKeyHandler;

        private bool isRunning;

        // TODO: Remove these?
        private uint width, height;

        public Window(string title, uint width, uint height)
        {
            width = width;
            height = height;
            isRunning = true;

            window = new OpenTK.GameWindow((int)width, (int)height,
                GraphicsMode.Default, title);
            AddDefaultKeyEventHandler();
            window.Run();
        }
        public Window(string title, uint height, AspectRatio aspect) {
            this.height = height;
            switch (aspect) {
            case AspectRatio.R1X1:
                width = this.height;
                break;
            case AspectRatio.R4X3:
                width = this.height * 4 / 3;
                break;
            case AspectRatio.R16X9:
                width = this.height * 16 / 9;
                break;
            default:
                throw new InvalidEnumArgumentException();
            }

            window = new GameWindow((int) width, // initial width
                (int) height, // initial height
                GraphicsMode.Default,
                title, // initial title
                GameWindowFlags.Default,
                DisplayDevice.Default,
                3, // OpenGL major version
                3, // OpenGL minor version
                GraphicsContextFlags.ForwardCompatible);

            //window = new GameWindow((int) width, (int) this.height);
            //GraphicsMode.Default, title);
            AddDefaultKeyEventHandler();
            isRunning = true;
            //window.Run();
            //window.MakeCurrent();

            window.Context.MakeCurrent(window.WindowInfo);
            window.Visible = true;
        }

        public bool Resizable { get; set; }

        private void AddDefaultKeyEventHandler() {
            defaultKeyHandler = delegate(object sender, KeyboardKeyEventArgs e) {
                if (e.Key == Key.Escape) {
                    CloseWindow();
                }
                if (e.Key == Key.Down) {
                    Console.WriteLine("press Down");
                    //Console.Out.Flush();
                }
                if (e.Key == Key.C) {
                    this.Clear();
                }
                if (e.Key == Key.S) {
                    this.SwapBuffers();
                }
            };
            window.Keyboard.KeyDown += defaultKeyHandler;
        }

        private void RemoveDefaultKeyEventHandler() {
            if (defaultKeyHandler != null) {
                window.Keyboard.KeyDown -= defaultKeyHandler;
                defaultKeyHandler = null;
            }
        }

        public void AddKeyUpEventHandler(EventHandler<KeyboardKeyEventArgs> method) {
            RemoveDefaultKeyEventHandler();
            window.Keyboard.KeyUp += method;
        }

        public void AddKeyDownEventHandler(EventHandler<KeyboardKeyEventArgs> method) {
            RemoveDefaultKeyEventHandler();
            window.Keyboard.KeyDown += method;
        }

        public bool IsRunning() {
            return isRunning;
        }

        public void CloseWindow() {
            isRunning = false;
            window.Close();
        }

        public void Clear() {
            GL.ClearDepth(1);
            GL.ClearColor(Color.Aquamarine);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //_window.Context.
        }

        // TODO: Call this DoubleBuffer() instead?
        public void SwapBuffers() {
            window.SwapBuffers();
            //window.Context.SwapBuffers();
        }

        public void PollEvents() {
            window.ProcessEvents();
        }

        public void SaveScreenShot() {
            if (GraphicsContext.CurrentContext == null) {
                throw new GraphicsContextMissingException();
            }

            var bmp = new Bitmap(window.ClientSize.Width, window.ClientSize.Height);
            var data =
                bmp.LockBits(window.ClientRectangle, ImageLockMode.WriteOnly,
                    PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, window.ClientSize.Width, window.ClientSize.Height,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgr,
                PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bmp.Save($"screenShot_{Window.screenShotCounter}.bmp");
            Window.screenShotCounter++;
        }

        public void PrintVersion() { }
    }
}