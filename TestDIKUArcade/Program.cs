using System;
using DIKUArcade.Graphics;

namespace TestDIKUArcade {
    internal class Program {
        public static void Main(string[] args) {
            //var win = new Window("hej", 500, AspectRatio.Ratio_1X1);
            var stride = new ImageStride(500);
            stride.StartAnimationTimer();
            Console.WriteLine("window opened now");

            while (false)
                //while (win.IsRunning())
            {
                //win.SwapBuffers();
                stride.RenderNextFrame();
            }
            Console.WriteLine("window closed now");
        }
    }
}