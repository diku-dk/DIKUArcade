namespace TestDIKUArcade.GameTimerTest;

using System;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using System.Numerics;
using DIKUArcade.Entities;

public class GameTimerTest : ITestable {
    public void RunTest() {
        var winArgs = new WindowArgs() {
            Title="GameTimerTest",
            AspectRatio=WindowAspectRatio.Aspect_4X3
        };
        var win = new Window(winArgs);
        var timer = new GameTimer();
        var fps = new Text("FPS:");
        var ups = new Text("UPS:");
        var fpsShape = new StationaryShape(new Vector2(0.25f, 0.50f), Vector2.One);
        var upsShape = new StationaryShape(new Vector2(0.25f, 0.25f), Vector2.One);
        Action<WindowContext> render = ctx => {
            fpsShape.Extent = fps.IdealExtent(winArgs.Width, winArgs.Height);
            upsShape.Extent = ups.IdealExtent(winArgs.Width, winArgs.Height);
            fps.Render(fpsShape, ctx);
            ups.Render(upsShape, ctx);
        };
        foreach (var text in new []{fps, ups}) {
            text.SetColor(255, 255, 255);
            text.SetFontSize(80);
            // text.GetShape().ScaleYFromCenter(1.2f);
        }
        while (win.IsRunning()) {
            win.PollEvents();
            timer.MeasureTime();
            while (timer.ShouldUpdate()) { }
            if (timer.ShouldRender()) {
                // render game objects
                win.Render(render);
            }
            if (timer.ShouldReset()) {
                fps.SetText($"FPS: {timer.CapturedFrames}");
                ups.SetText($"UPS: {timer.CapturedUpdates}");
                // win.Title = "TestGameTimer | " + timer.CapturedFrames;
            }
        }
    }

    public void Help() {
        var help = "Watch the timers.";
        Console.WriteLine(help);
    }
}