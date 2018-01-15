using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Strategies;
using OpenTK.Input;

namespace TestDIKUArcade {
    internal class Program {
        public static void Main(string[] args) {
            var win = new Window("hej", 500, AspectRatio.R1X1);
            win.SetClearColor(System.Drawing.Color.Brown);

            //ImageStride imgs = new ImageStride(100,
            //    "files_01.png", "files_02.png", "files_03.png", "files_04.png");
            Image img = new Image("Taxi.png");
            MovementStrategy strat = new MovementStrategy();
            Entity ent = new DynamicEntity(new Vec2F(0.0f, 0.0f), new Vec2F(0.5f, 0.5f));
            EntityActor actor = new EntityActor(ent, strat, img);

            Entity ent2 = new DynamicEntity(new Vec2F(0.0f, 0.5f), ent.Extent);
            EntityActor actor2 = new EntityActor(ent2, strat, img);

            // TODO: Can we somehow avoid students creating references to OpenTK ??
            win.AddKeyPressEventHandler(delegate(OpenTK.Input.KeyboardKeyEventArgs keyArgs) {
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
                    ent.Scale(new Vec2F(1.1f, 1.1f));
                    ent2.Scale(new Vec2F(1.1f, 1.1f));
                }
                if (keyArgs.Key == Key.KeypadPlus) {
                    ent.Scale(new Vec2F(0.9f, 0.9f));
                    ent2.Scale(new Vec2F(0.9f, 0.9f));
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