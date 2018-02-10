using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using OpenTK.Input;

namespace TestDIKUArcade {
    public class TestEntityRotation {
        public static void MainFunction() {
            var win = new Window("TestEntityRotation", 500, AspectRatio.R1X1) {Resizable = false};

            var img = new Image("Taxi.png");
            var ent = new Entity(
                new DynamicShape(new Vec2F(0.25f,0.25f), new Vec2F(0.5f,0.5f)), img);

            win.AddKeyPressEventHandler(delegate(KeyboardKeyEventArgs args) {
                if (args.Key == Key.R) {
                    if (args.Shift) {
                        ent.Shape.Rotate((float)System.Math.PI / 16.0f);
                    } else {
                        ent.Shape.Rotate((float)System.Math.PI / -16.0f);
                    }
                }
                if (args.Key == Key.Escape) {
                    win.CloseWindow();
                }
            });

            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();
                ent.RenderEntity();
                win.SwapBuffers();
            }
        }
    }
}