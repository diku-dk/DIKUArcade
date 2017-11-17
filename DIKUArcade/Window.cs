using OpenTK.Graphics;
using OpenTK.Input;

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
        
        private OpenTK.GameWindow _window;
        
        public Window(string title, uint width, uint height)
        {
            _width = width;
            _height = height;
            _isRunning = true;
            
            _window = new OpenTK.GameWindow((int)width, (int)height,
                GraphicsMode.Default, title);
            AddDefaultKeyEventHandler();
            _window.Run();
        }
        public Window(string title, uint width, AspectRatio aspect)
        {
            
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
        
        void AddKeyUpEventHandler(System.EventHandler<OpenTK.Input.KeyboardKeyEventArgs> method)
        {
            RemoveDefaultKeyEventHandler();
            _window.Keyboard.KeyUp += method;
        }
        
        void AddKeyDownEventHandler(System.EventHandler<OpenTK.Input.KeyboardKeyEventArgs> method)
        {
            RemoveDefaultKeyEventHandler();
            _window.Keyboard.KeyDown += method;
        }

        bool IsRunning()
        {
            return _isRunning;
        }
        void CloseWindow()
        {
            _isRunning = false;
            _window.Close();
        }

        // TODO: Call this DoubleBuffer() instead?
        void SwapBuffers()
        {
            
        }
    }
}
