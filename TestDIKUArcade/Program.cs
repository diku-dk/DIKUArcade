using System;
using DIKUArcade;
using DIKUArcade.Graphics;

namespace TestDIKUArcade {
    internal class Program {
        public static void Main(string[] args) {
            ImageStride imgs = new ImageStride(100,
                "files_01.png", "files_02.png", "files_03.png", "files_04.png");
            Image img = new Image("file_01.png");

            var win = new Window("hej", 500, AspectRatio.R1X1);
            var stride = new ImageStride(500);

            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();

                win.SwapBuffers();
                imgs.Render();
                img.Render();
            }
            Console.WriteLine("window closed now");
        }
    }
}