using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using OpenTK.Input;

namespace TestDIKUArcade {
    public class TestSimpleEntityRendering {
        public static void MainFunction() {
            var win = new Window("hej", 500, AspectRatio.R1X1);
            win.SetClearColor(System.Drawing.Color.Brown);

            //ImageStride imgs = new ImageStride(100,
            //    "files_01.png", "files_02.png", "files_03.png", "files_04.png");
            Image img1 = new Image("Taxi.png");
            Image img2 = new Image("Taxi2.png");

            ImageStride imgs1 = new ImageStride(250, img1, img2);
            ImageStride imgs2 = new ImageStride(250, img1, img2);

            Shape ent = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.5f, 0.5f));
            Shape ent2 = new DynamicShape(new Vec2F(0.0f, 0.5f), ent.Extent.Copy());

            Entity actor = new Entity(ent, imgs1);
            Entity actor2 = new Entity(ent2, imgs2);

            win.AddKeyPressEventHandler(delegate(KeyboardKeyEventArgs keyArgs) {
                if (keyArgs.Key == Key.A) {
                    win.SetClearColor(System.Drawing.Color.Red);
                }
                if (keyArgs.Key == Key.C) {
                    win.SetClearColor(System.Drawing.Color.DarkGoldenrod);
                }
                if (keyArgs.Key == Key.Left) {
                    ent.Position.X -= 0.03f;
                    ent2.Position.X -= 0.03f;
                }
                if (keyArgs.Key == Key.Right) {
                    ent.Position.X += 0.03f;
                    ent2.Position.X += 0.03f;
                }
                if (keyArgs.Key == Key.KeypadSubtract) {
                    ent.Scale(1.1f);
                    ent2.Scale(1.1f);
                }
                if (keyArgs.Key == Key.KeypadPlus) {
                    ent.Scale(0.9f);
                    ent2.Scale(0.9f);
                }
                if (keyArgs.Key == Key.U) {
                    ent.ScaleXFromCenter(0.9f);
                    ent2.ScaleXFromCenter(0.9f);
                }
                if (keyArgs.Key == Key.I) {
                    ent.ScaleXFromCenter(1.1f);
                    ent2.ScaleXFromCenter(1.1f);
                }
            });

            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();

                actor.RenderEntity();
                actor2.RenderEntity();

                win.SwapBuffers();
            }
            Console.WriteLine("window closed now");
        }
    }
}