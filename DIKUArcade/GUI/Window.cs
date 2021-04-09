using System;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Bitmap = System.Drawing.Bitmap;
using RotateFlipType = System.Drawing.RotateFlipType;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using DIKUArcade.Input;

namespace DIKUArcade.GUI {
    /// <summary>
    /// This class represents a graphical window in the DIKUArcade game engine.
    /// </summary>
    public class Window {
        private static uint screenShotCounter;

        /// <summary>
        /// Every DIKUArcade.Window instance has its own private
        /// OpenTK.GameWindow object.
        /// </summary>
        private GameWindow window;

        private bool isRunning;

        public string Title {
            get { return window.Title; }
            set { window.Title = value; }
        }

        /// <summary>
        /// Get or set if this Window instance should be resizable.
        /// </summary>
        public bool Resizable { get; set; } = true;

        /// <summary>
        /// Instance for transforming OpenTK key events to DIKUArcade-interfaced
        /// key events, based on globalization settings.
        /// </summary>
        private IKeyTransformer keyTransformer;


        #region OpenGLContext

        /// <summary>
        /// A static, private OpenTK.GameWindow instance.
        /// Only used for initializing an OpenGL context in the background.
        /// </summary>
        private static GameWindow _contextWin;

        /// <summary>
        /// Use this method to create an OpenGL context.
        /// Never use this method in your application, ONLY in unit testing.
        /// This will enable you to unit test classes which use OpenGL-dependent
        /// function calls, including `Text', `Image', and `ImageStride' classes.
        /// </summary>
        public static void CreateOpenGLContext() {
            var settings = new GameWindowSettings();
            var nativeSettings = new NativeWindowSettings();
            Window._contextWin = new GameWindow(settings, nativeSettings);
            Window._contextWin.Context.MakeCurrent();
        }

        #endregion


        private void ActivateThisWindowContext(string title, uint width, uint height, bool fullscreen) {
            // We use OpenGL 2.0 (ie. fixed-function pipeline!)
            var settings = new GameWindowSettings();
            settings.IsMultiThreaded = false;
            var nativeSettings = new NativeWindowSettings();
            nativeSettings.Profile = ContextProfile.Any;
            nativeSettings.WindowState = WindowState.Normal;
            nativeSettings.API = ContextAPI.OpenGL;
            nativeSettings.APIVersion = new Version(2, 0);
            nativeSettings.IsFullscreen = fullscreen;

            window = new GameWindow(settings, nativeSettings) {
                Title = title,
                Size = new OpenTK.Mathematics.Vector2i((int)width, (int)height)
            };

            GL.ClearDepth(1);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            isRunning = true;
            window.Context.MakeCurrent();
            window.IsVisible = true;

            GL.Viewport(0, 0, window.Size.X, window.Size.Y);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0,1.0,0.0,1.0, 0.0, 4.0);
        }


        public Window(WindowArgs windowArgs) {
            // keyboard layout
            switch(windowArgs.KeyboardLayout) {
                case KeyboardLayout.Danish:
                    keyTransformer = new Input.Languages.DanishKeyTransformer();
                    break;
                default:
                    throw new ArgumentException("Window(): only Danish keyboard layout is currently supported!");
            }

            // window dimensions
            uint width = windowArgs.Width;
            uint height = windowArgs.Height;
            switch (windowArgs.AspectRatio) {
                case WindowAspectRatio.Aspect_Custom:
                    break;
                case WindowAspectRatio.Aspect_1X1:
                    width = height;
                    break;
                case WindowAspectRatio.Aspect_3X2:
                    width = (height * 3) / 2;
                    break;
                case WindowAspectRatio.Aspect_4X3:
                    width = (height * 4) / 3;
                    break;
                case WindowAspectRatio.Aspect_16X9:
                    width = (height * 16) / 9;
                    break;
                default:
                    throw new ArgumentException("Window(): invalid aspect ratio!");
            }

            // create and bind OpenGL context
            ActivateThisWindowContext(windowArgs.Title, width, height, windowArgs.FullScreen);

            // setup event handlers
            if (windowArgs.Resizable) {
                AddDefaultResizeHandler();
            }
            AddDefaultKeyEventHandler();
        }

        ~Window() {
            if (window != null) this.DestroyWindow();
        }


        #region WINDOW_RESIZE

        private void DefaultResizeHandler(ResizeEventArgs args) {
            if (!Resizable) { 
                return; 
            }

            // GL.Viewport(0, 0, window.Size.X, window.Size.Y);
            GL.Viewport(0, 0, args.Size.X, args.Size.Y); // TODO: Is that right?
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, 1.0, 0.0, 1.0, 0.0, 4.0);
        }

        private void AddDefaultResizeHandler() {
            window.Resize += DefaultResizeHandler;
        }

        private void RemoveDefaultResizeHandler() {
            window.Resize -= DefaultResizeHandler;
        }

        #endregion WINDOW_RESIZE

        #region KEY_EVENT_HANDLERS

        

        private void DefaultKeyEventHandler(KeyboardKeyEventArgs args) {
            switch(args.Key) {
                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape:
                    CloseWindow();
                    break;
                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.F12:
                    SaveScreenShot();
                    break;
                default:
                    break;
            }
        }

        private void AddDefaultKeyEventHandler() {
            window.KeyDown += DefaultKeyEventHandler;
        }

        private void RemoveDefaultKeyEventHandler() {
            window.KeyDown -= DefaultKeyEventHandler;
        }

        /// <summary>
        /// Attach the specified keyHandler method argument to this window object.
        /// All key inputs will thereafter be directed to this keyHandler.
        /// </summary>
        public void SetKeyEventHandler(Action<KeyboardAction,KeyboardKey> keyHandler) {
            RemoveDefaultKeyEventHandler();
            window.KeyDown += args => {
                if (!args.IsRepeat) keyHandler(KeyboardAction.KeyPress, keyTransformer.TransformKey(args.Key));
            };
            window.KeyUp += args => {
                keyHandler(KeyboardAction.KeyRelease, keyTransformer.TransformKey(args.Key));
            };
        }

        #endregion KEY_EVENT_HANDLERS

        /// <summary>
        /// Check if the Window is still running.
        /// </summary>
        public bool IsRunning() {
            return isRunning;
        }

        /// <summary>
        /// Sets the window running variable to false such that calls to
        /// `IsRunning()` afterwards will return false. This will allow one
        /// to exit the game loop.
        /// </summary>
        public void CloseWindow() {
            isRunning = false;
        }

        /// <summary>
        /// Close the underlying OpenTK window object.
        /// Do not call this method outside the engine.
        /// </summary>
        public void DestroyWindow() {
            window.Close();
            window.Dispose();
            window = null;
        }

        /// <summary>
        /// Clear the Window with a uniform background color.
        /// </summary>
        public void Clear() {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        #region SET_CLEAR_COLOR

        /// <summary>
        /// Set color to be used as clear color when using the Window.Clear() method.
        /// </summary>
        /// <param name="vec">Vec3F containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetClearColor(Math.Vec3F vec) {
            if (vec.X < 0.0f || vec.X > 1.0f ||
                vec.Y < 0.0f || vec.Y > 1.0f ||
                vec.Z < 0.0f || vec.Z > 1.0f) {
                throw new ArgumentOutOfRangeException(
                    $"Normalized RGB Color values must be between 0 and 1: {vec}");
            }
            GL.ClearColor(vec.X, vec.Y, vec.Z, 1.0f);
        }

        /// <summary>
        /// Set color to be used as clear color when using the Window.Clear() method.
        /// </summary>
        /// <param name="vec">Vec3I containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be between 0 and 255.</exception>
        public void SetClearColor(Math.Vec3I vec) {
            if (vec.X < 0 || vec.X > 255 ||
                vec.Y < 0 || vec.Y > 255 ||
                vec.Z < 0 || vec.Z > 255) {
                throw new ArgumentOutOfRangeException(
                    $"RGB Color values must be between 0 and 255: {vec}");
            }
            GL.ClearColor(vec.X / 255.0f, vec.Y / 255.0f, vec.Z / 255.0f, 1.0f);
        }

        /// <summary>
        /// Set color to be used as clear color when using the Window.Clear() method.
        /// </summary>
        /// <param name="r">red channel value</param>
        /// <param name="g">green channel value</param>
        /// <param name="b">blue channel value</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetClearColor(float r, float g, float b) {
            if (r < 0.0f || r > 1.0f ||
                g < 0.0f || g > 1.0f ||
                b < 0.0f || b > 1.0f) {
                throw new ArgumentOutOfRangeException(
                    $"Normalized RGB Color values must be between 0 and 1: ({r},{g},{b})");
            }
            GL.ClearColor(r, g, b, 1.0f);
        }

        /// <summary>
        /// Set color to be used as clear color when using the Window.Clear() method.
        /// </summary>
        /// <param name="r">red channel value</param>
        /// <param name="g">green channel value</param>
        /// <param name="b">blue channel value</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be between 0 and 255.</exception>
        public void SetClearColor(int r, int g, int b) {
            if (r < 0 || r > 255 ||
                g < 0 || g > 255 ||
                b < 0 || b > 255) {
                throw new ArgumentOutOfRangeException(
                    $"RGB Color values must be between 0 and 255: ({r},{g},{b})");
            }
            GL.ClearColor(r / 255.0f, g / 255.0f, b / 255.0f, 1.0f);
        }

        /// <summary>
        /// Set color to be used as clear color when using the Window.Clear() method.
        /// </summary>
        /// <param name="color">System.Drawing.Color object containing color channel values.</param>
        public void SetClearColor(System.Drawing.Color color) {
            SetClearColor(new Math.Vec3I(color.R, color.G, color.B));
        }

        #endregion

        /// <summary>
        /// Swap double buffers for the Window.
        /// </summary>
        public void SwapBuffers() {
            window.SwapBuffers();
        }

        /// <summary>
        /// Check for incoming keyboard or mouse events.
        /// </summary>
        public void PollEvents() {
            window.ProcessEvents();
        }

        /// <summary>
        /// Save a screenshot of the Window's current state.
        /// </summary>
        /// <exception cref="GraphicsContextMissingException"></exception>
        public void SaveScreenShot() {
            if (window.Context == null) {
                throw new ArgumentNullException("GraphicsContextMissingException");
            }

            var bmp = new Bitmap(window.ClientSize.X, window.ClientSize.Y, PixelFormat.Format24bppRgb);
            var data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, window.ClientSize.X, window.ClientSize.Y),
                                    ImageLockMode.WriteOnly,
                                    PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, window.ClientSize.X, window.ClientSize.Y,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgr,
                PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

            // save screenshot, not in bin/Debug (et sim.), but in a logical place
            var dir = new DirectoryInfo(Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location));

            while (dir.Name != "bin") {
                dir = dir.Parent;
            }
            dir = dir.Parent;

            // build the save path
            var saveName = $"screenShot_{Window.screenShotCounter++}.bmp";
            var folder = Path.Combine(dir.ToString(), "screenShots");
            var path = Path.Combine(folder, saveName);

            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }

            bmp.Save(path);
            Console.WriteLine($"Screenshot saved as: {path}");
        }
    }
}
