using DIKUArcade;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace TestDIKUArcade {
    public class TestGameTimer {
        public static void MainFunction() {
            var win = new Window("TestGameTimer", 300, AspectRatio.R4X3);
            var timer = new GameTimer(30);
            var fps = new Text("", new Vec2F(0.25f, 0.5f),
                new Vec2F(0.5f, 0.25f));
            var ups = new Text("", new Vec2F(0.25f, 0.25f),
                new Vec2F(0.5f, 0.25f));
            foreach (var text in new []{fps, ups}) {
                text.SetColor(new Vec3I(255, 255, 255));
                text.SetFontSize(80);
                text.GetShape().ScaleYFromCenter(1.2f);
            }

            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();
                timer.MeasureTime();

                while (timer.ShouldUpdate()) {
                    // win.GetEventBus().ProcessEvents(); // TODO: implement! (but property)
                    // update game logic
                }

                if (timer.ShouldRender()) {
                    // render game objects
                    fps.RenderText();
                    ups.RenderText();
                }

                if (timer.ShouldReset()) {
                    timer.ResetTime();
                    fps.SetText($"FPS: {timer.CapturedFrames}");
                    ups.SetText($"UPS: {timer.CapturedUpdates}");
                }
                win.SwapBuffers();
            }
        }
    }
}