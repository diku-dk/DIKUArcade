
using OpenTK.Input;
using OpenTK.Graphics;

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
            _window.Run();
        }
        public Window(string title, uint width, AspectRatio aspect)
        {
            
        }

        // This is the signature for a key event handler:
        //private delegate void KeyEventHandler(object sender, KeyboardKeyEventArgs e);
        
        void AddKeyUpEventHandler(System.EventHandler<OpenTK.Input.KeyboardKeyEventArgs> method)
        {
            _window.Keyboard.KeyUp += method;
        }
        
        void AddKeyDownEventHandler(System.EventHandler<OpenTK.Input.KeyboardKeyEventArgs> method)
        {
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
