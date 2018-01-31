using System.Drawing;
using DIKUArcade;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace TestDIKUArcade {
    public class TestRenderText {

        public static void MainFunction() {
            var win = new DIKUArcade.Window("TestRenderText", 500, AspectRatio.R1X1);
            //win.SetClearColor(new Vec3I(128, 56, 173));
            win.SetClearColor(System.Drawing.Color.DarkOliveGreen);

            var text = new Text("MIN TEXT", new Vec2F(0.25f, 0.25f), new Vec2F(0.25f, 0.25f));
            text.SetColor(Color.Crimson);

            while (win.IsRunning()) {
                win.PollEvents();

                win.Clear();
                // some drawing...
                text.RenderText();

                win.SwapBuffers();
            }
        }
    }
}