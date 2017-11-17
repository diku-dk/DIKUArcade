using System.ComponentModel;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace DIKUArcade
{
    public enum AspectRatio
    {
        Ratio_4X3,
        Ratio_16X9,
        Ratio_1X1,
    };

    /// <summary>
    /// Create an OpenTK window wrapper, where we only show
    /// relevant data members and hide unneeded functionality.
    /// </summary>
    public class Window
    {
        private uint _width, _height;
        public bool Resizable { get; set; }
        private bool _isRunning;

        private readonly OpenTK.GameWindow _window;

        private static uint _screenShotCounter = 0;

        public Window(string title, uint width, uint height)
        {
            _width = width;
            _height = height;
            _isRunning = true;

            _window = new OpenTK.GameWindow((int)_width, (int)_height,
                GraphicsMode.Default, title);
            AddDefaultKeyEventHandler();
            _window.Run();
        }
        public Window(string title, uint height, AspectRatio aspect)
        {
            _height = height;
            switch (aspect)
            {
                case AspectRatio.Ratio_1X1:
                    _width = _height;
                    break;
                case AspectRatio.Ratio_4X3:
                    _width = _height * 4 / 3;
                    break;
                case AspectRatio.Ratio_16X9:
                    _width = _height * 16 / 9;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            _window = new OpenTK.GameWindow((int)_width, (int)_height,
                GraphicsMode.Default, title);
            AddDefaultKeyEventHandler();
            _window.Run();
        }

        // This is the signature for a key event handler:
        //private delegate void KeyEventHandler(object sender, KeyboardKeyEventArgs e);
        private System.EventHandler<OpenTK.Input.KeyboardKeyEventArgs> _defaultKeyHandler = null;

        private void AddDefaultKeyEventHandler()
        {
            _defaultKeyHandler = delegate(object sender, KeyboardKeyEventArgs e)
            {
                if (e.Key == OpenTK.Input.Key.Escape)
                {
                    this.CloseWindow();
                }
            };
            _window.Keyboard.KeyDown += _defaultKeyHandler;
        }

        private void RemoveDefaultKeyEventHandler()
        {
            if (_defaultKeyHandler != null)
            {
                _window.Keyboard.KeyDown -= _defaultKeyHandler;
                _defaultKeyHandler = null;
            }
        }

        public void AddKeyUpEventHandler(System.EventHandler<OpenTK.Input.KeyboardKeyEventArgs> method)
        {
            RemoveDefaultKeyEventHandler();
            _window.Keyboard.KeyUp += method;
        }

        public void AddKeyDownEventHandler(System.EventHandler<OpenTK.Input.KeyboardKeyEventArgs> method)
        {
            RemoveDefaultKeyEventHandler();
            _window.Keyboard.KeyDown += method;
        }

        public bool IsRunning()
        {
            return _isRunning;
        }
        public void CloseWindow()
        {
            _isRunning = false;
            _window.Close();
        }

        // TODO: Call this DoubleBuffer() instead?
        public void SwapBuffers()
        {
            _window.SwapBuffers();
        }

        public void SaveScreenShot()
        {
            if (GraphicsContext.CurrentContext == null)
            {
                throw new GraphicsContextMissingException();
            }

            Bitmap bmp = new Bitmap(_window.ClientSize.Width, _window.ClientSize.Height);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(_window.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly,
                             System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, _window.ClientSize.Width, _window.ClientSize.Height, PixelFormat.Bgr,
                          PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bmp.Save($"screenShot_{_screenShotCounter}.bmp");
            _screenShotCounter++;
        }
    }
}
