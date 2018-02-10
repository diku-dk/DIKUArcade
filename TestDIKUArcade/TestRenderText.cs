using System.Drawing;
using DIKUArcade;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace TestDIKUArcade {
    public class TestRenderText {

        public static void MainFunction() {
            var win = new Window("TestRenderText", 500, AspectRatio.R1X1);
            //win.SetClearColor(new Vec3I(128, 56, 173));
            win.SetClearColor(Color.DarkOliveGreen);

            var text = new Text("MIN TEXT", new Vec2F(0.25f, 0.25f), new Vec2F(0.25f, 0.25f));
            text.SetColor(Color.Crimson);
            text.GetShape().Rotation = (float)System.Math.PI / 2.0f;
            text.GetShape().ScaleXFromCenter(3.2f);

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