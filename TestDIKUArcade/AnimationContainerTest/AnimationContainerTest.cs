namespace TestDIKUArcade.AnimationContainerTest;

using System;
using DIKUArcade.GUI;

public class AnimationContainerTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "AnimationContainerTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press '1', '2', '3' or '4' to add a smoke animation.";
        Console.WriteLine(help);
    }
}
