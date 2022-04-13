namespace TestDIKUArcade.AnimationsTest;

using DIKUArcade.GUI;
using System;

public class AnimationsTest : ITestable {

    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "AnimationsTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press 'Space' to add an animation.";
        Console.WriteLine(help);
    }
}

