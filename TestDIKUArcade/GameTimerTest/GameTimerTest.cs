namespace TestDIKUArcade.GameTimerTest;

using System;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using System.Numerics;
using System.Drawing;
using DIKUArcade.Entities;

public class GameTimerTest : ITestable {
    public void RunTest() {
        var winArgs = new WindowArgs() {
            Title="GameTimerTest"
        };
        var win = new Window(winArgs);
        var timer = new GameTimer();
        var fps = new Text("FPS:", new Vector2(0.25f, 0.66f));
        var ups = new Text("UPS:", new Vector2(0.25f, 0.33f));
        Action<WindowContext> render = ctx => {
            fps.Render(ctx);
            ups.Render(ctx);
        };
        foreach (var text in new []{fps, ups}) {
            text.SetColor(255, 255, 255);
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