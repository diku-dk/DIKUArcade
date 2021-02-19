using System;
using System.ComponentModel;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Bitmap = System.Drawing.Bitmap;
using Color = System.Drawing.Color;
using RotateFlipType = System.Drawing.RotateFlipType;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using DIKUArcade.EventBus;

namespace DIKUArcade {
    /// <summary>
    /// Aspect ratio for a DIKUArcade.Window object, defining
    /// width as a function of height.
    /// </summary>
    public enum AspectRatio {
        R4X3,
        R16X9,
        R1X1
    }

    /// <summary>
    /// Create an OpenTK window wrapper, where we only show
    /// relevant data members and hide unneeded functionality.
    /// </summary>
    public class Window {
        private static uint screenShotCounter;

        /// <summary>
        /// Every DIKUArcade.Window instance has its own private
        /// OpenTK.GameWindow object.
        /// </summary>
        private GameWindow window;

        // This is the signature for a key event handler:
        //private delegate void KeyEventHandler(object sender, KeyboardKeyEventArgs e);
        private Action<KeyboardKeyEventArgs> defaultKeyHandler;
        private Action<ResizeEventArgs> defaultResizeHandler;

        private bool isRunning;

        private uint width, height;

        public string Title {
            get { return window.Title; }
            set { window.Title = value; }
        }

        private GameEventBus<object> eventBus;


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


        private void ActivateThisWindowContext(string title) {
            // We use OpenGL 2.0 (ie. fixed-function pipeline!)
            var settings = new GameWindowSettings();
            settings.IsMultiThreaded = false;
            var nativeSettings = new NativeWindowSettings();
            nativeSettings.Profile = ContextProfile.Any;
            nativeSettings.WindowState = WindowState.Normal;
            nativeSettings.API = ContextAPI.OpenGL;
            nativeSettings.APIVersion = new Version(2, 0);

            window = new GameWindow(settings, nativeSettings) {
                Title = title,
                Size = new OpenTK.Mathematics.Vector2i((int)width, (int)height)
            };

            GL.ClearDepth(1);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            AddDefaultKeyEventHandler();
            AddDefaultResizeHandler();

            isRunning = true;
            window.Context.MakeCurrent();
            window.IsVisible = true;

            GL.Viewport(0, 0, window.Size.X, window.Size.Y);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0,1.0,0.0,1.0, 0.0, 4.0);
        }

        public Window(string title, uint width, uint height)
        {
            this.width = width;
            this.height = height;
            isRunning = true;
            ActivateThisWindowContext(title);
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
            ActivateThisWindowContext(title);
        }

        /// <summary>
        /// Register an event bus to this window instance. The specified
        /// bus will be used for capturing input events, such as keyboard presses.
        /// </summary>
        /// <param name="bus">A GameEventBus to register for this window</param>
        /// <returns>False if an event bus was already registered, true otherwise.</returns>
        public bool RegisterEventBus(GameEventBus<object> bus) {
            if (eventBus != null) {
                // an event bus was already registered!
                // TODO: Should it be possible to swap event bus?
                return false;
            }
            eventBus = bus;
            window.KeyDown += RegisterKeyDown;
            window.KeyUp += RegisterKeyUp;
            RemoveDefaultKeyEventHandler();
            return true;
        }

        private void RegisterKeyDown(KeyboardKeyEventArgs args) {
            var keyAction = "KEY_PRESS";
            var keyEvent = GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.InputEvent, this, Input.KeyTransformer.GetKeyString(args.Key), keyAction, "");
                eventBus.RegisterEvent(keyEvent);
        }

        private void RegisterKeyUp(KeyboardKeyEventArgs args) {
            var keyAction = "KEY_RELEASE";
            var keyEvent = GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.InputEvent, this, Input.KeyTransformer.GetKeyString(args.Key), keyAction, "");
                eventBus.RegisterEvent(keyEvent);
        }


        #region WINDOW_RESIZE

        private bool resizable = true;

        /// <summary>
        /// Get or set if this Window instance should be resizable.
        /// </summary>
        public bool Resizable {
            get {
                return resizable;
            }
            set {
                if (value) {
                    RemoveDefaultResizeHandler();
                } else {
                    AddDefaultResizeHandler();
                }
            }
        }

        private void AddDefaultResizeHandler() {
            if (defaultResizeHandler != null) {
                return;
            }

            defaultResizeHandler = delegate {
                GL.Viewport(0, 0, window.Size.X, window.Size.Y);
                width = (uint) window.Size.X;
                height = (uint) window.Size.Y;

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                //GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
                GL.Ortho(0.0,1.0,0.0,1.0, 0.0, 4.0);
                };
            window.Resize += defaultResizeHandler;
        }

        private void RemoveDefaultResizeHandler() {
            if (defaultResizeHandler != null) {
                window.Resize -= defaultResizeHandler;
                defaultResizeHandler = null;
            }
        }

        #endregion WINDOW_RESIZE

        #region KEY_EVENT_HANDLERS

        private void AddDefaultKeyEventHandler() {
            if (defaultKeyHandler != null) {
                return;
            }

            defaultKeyHandler = delegate(KeyboardKeyEventArgs e) {
                if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape) {
                    CloseWindow();
                    return;
                }
                if (e.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.F12) {
                    SaveScreenShot();
                }
            };
            window.KeyDown += defaultKeyHandler;
        }

        private void RemoveDefaultKeyEventHandler() {
            if (defaultKeyHandler != null) {
                window.KeyDown -= defaultKeyHandler;
                defaultKeyHandler = null;
            }
        }

        /// <summary>
        /// Function signature for a key event handler method.
        /// </summary>
        /// <param name="keyArgs">OpenTK.Input.KeyboardKeyEventArgs</param>
        public delegate void WindowKeyHandler(KeyboardKeyEventArgs keyArgs);

        /// <summary>
        /// Add an event handler for when any keyboard key is pressed.
        /// </summary>
        /// <param name="method">Method with the signature of a Window.WindowKeyHandler delegate.</param>
        public void AddKeyPressEventHandler(WindowKeyHandler method) {
            //RemoveDefaultKeyEventHandler();
            window.KeyUp += delegate(KeyboardKeyEventArgs args) { method(args); };
        }

        /// <summary>
        /// Add an event handler for when any keyboard key is released.
        /// </summary>
        /// <param name="method">Delegate method</param>
        public void AddKeyReleaseEventHandler(Action<KeyboardKeyEventArgs> method) {
            //RemoveDefaultKeyEventHandler();
            window.KeyDown += method;
        }

        #endregion KEY_EVENT_HANDLERS

        /// <summary>
        /// Check if the Window is still running.
        /// </summary>
        public bool IsRunning() {
            return isRunning;
        }

        /// <summary>
        /// Close the Window.
        /// </summary>
        public void CloseWindow() {
            isRunning = false;
            window.Close();
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
        /// <param name="vec">Vec4F containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetClearColor(Math.Vec4F vec) {
            if (vec.X < 0.0f || vec.X > 1.0f ||
                vec.Y < 0.0f || vec.Y > 1.0f ||
                vec.Z < 0.0f || vec.Z > 1.0f ||
                vec.W < 0.0f || vec.W > 1.0f) {
                throw new ArgumentOutOfRangeException(
                    $"Normalized RGBA Color values must be between 0 and 1: {vec}");
            }
            GL.ClearColor(vec.X, vec.Y, vec.Z, vec.W);
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
        /// <param name="vec">Vec4I containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be between 0 and 255.</exception>
        public void SetClearColor(Math.Vec4I vec) {
            if (vec.X < 0 || vec.X > 255 ||
                vec.Y < 0 || vec.Y > 255 ||
                vec.Z < 0 || vec.Z > 255 ||
                vec.W < 0 || vec.W > 255) {
                throw new ArgumentOutOfRangeException(
                    $"RGBA Color values must be between 0 and 255: {vec}");
            }
            GL.ClearColor(vec.X / 255.0f, vec.Y / 255.0f, vec.Z / 255.0f, vec.W / 255.0f);
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
        /// <param name="a">alpha channel value</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetClearColor(float r, float g, float b, float a) {
            if (r < 0.0f || r > 1.0f ||
                g < 0.0f || g > 1.0f ||
                b < 0.0f || b > 1.0f ||
                a < 0.0f || a > 1.0f) {
                throw new ArgumentOutOfRangeException(
                    $"Normalized RGBA Color values must be between 0 and 1: ({r},{g},{b},{a})");
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
        /// <param name="r">red channel value</param>
        /// <param name="g">green channel value</param>
        /// <param name="b">blue channel value</param>
        /// <param name="a">alpha channel value</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be between 0 and 255.</exception>
        public void SetClearColor(int r, int g, int b, int a) {
            if (r < 0 || r > 255 ||
                g < 0 || g > 255 ||
                b < 0 || b > 255 ||
                a < 0 || a > 255) {
                throw new ArgumentOutOfRangeException(
                    $"RGBA Color values must be between 0 and 255: ({r},{g},{b},{a})");
            }
            GL.ClearColor(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
        }

        /// <summary>
        /// Set color to be used as clear color when using the Window.Clear() method.
        /// </summary>
        /// <param name="color">System.Drawing.Color object containing color channel values.</param>
        public void SetClearColor(Color color) {
            SetClearColor(new Math.Vec4I(color.R, color.G, color.B, color.A));
        }

        #endregion

        /// <summary>
        /// Swap double buffers for the Window.
        /// </summary>
        public void SwapBuffers() {
            window.SwapBuffers();
            //window.Context.SwapBuffers();
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
                throw new ArgumentNullException("GraphicsContextMissingException");// TODO: GraphicsContextMissingException();
            }

            var bmp = new Bitmap(window.ClientSize.X, window.ClientSize.Y);
            var data =
                bmp.LockBits(new System.Drawing.Rectangle(
                    window.ClientRectangle.Min.X, window.ClientRectangle.Max.Y, // (left, top)
                    window.ClientRectangle.Max.X, window.ClientRectangle.Min.Y), // (right, bottom)
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
