namespace TestDIKUArcade.GameTimerTest;

using System;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using DIKUArcade.Math;

public class GameTimerTest : ITestable {
    public void RunTest() {
        var winArgs = new WindowArgs() {
            Title="GameTimerTest",
            AspectRatio=WindowAspectRatio.Aspect_4X3
        };
        var win = new Window(winArgs);
        var timer = new GameTimer();
        var fps = new Text("", new Vec2F(0.25f, 0.5f), new Vec2F(0.5f, 0.25f));
        var ups = new Text("", new Vec2F(0.25f, 0.25f), new Vec2F(0.5f, 0.25f));
        foreach (var text in new []{fps, ups}) {
            text.SetColor(new Vec3I(255, 255, 255));
            text.SetFontSize(80);
            text.GetShape().ScaleYFromCenter(1.2f);
        }
        while (win.IsRunning()) {
            win.PollEvents();
            timer.MeasureTime();
            while (timer.ShouldUpdate()) { }
            if (timer.ShouldRender()) {
                win.Clear();
                // render game objects
                fps.RenderText();
                ups.RenderText();
                win.SwapBuffers();
            }
            if (timer.ShouldReset()) {
                fps.SetText($"FPS: {timer.CapturedFrames}");
                ups.SetText($"UPS: {timer.CapturedUpdates}");
                win.Title = "TestGameTimer | " + timer.CapturedFrames;
            }
        }
    }

    public void Help() {
        var help = "Watch the timers.";
        Console.WriteLine(help);
    }
}