using System;
using DIKUArcade;
using DIKUArcade.Graphics;

namespace TestDIKUArcade {
    internal class Program {
        public static void Main(string[] args) {
            var win = new Window("hej", 500, AspectRatio.R1X1);
            var stride = new ImageStride(500);

            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();

                win.SwapBuffers();
                stride.RenderNextFrame();
            }
            Console.WriteLine("window closed now");
        }
    }
}