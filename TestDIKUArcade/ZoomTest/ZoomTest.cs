namespace TestDIKUArcade.ZoomTest;

using System;
using DIKUArcade.GUI;

public class ZoomTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "ZoomTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "It zooms in.";
        Console.WriteLine(help);
    }
}