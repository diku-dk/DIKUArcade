namespace TestDIKUArcade.RenderTextTest;

using System;
using DIKUArcade.GUI;

public class RenderTextTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "RenderTextTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Look and see if the text renders.";
        Console.WriteLine(help);
    }
}