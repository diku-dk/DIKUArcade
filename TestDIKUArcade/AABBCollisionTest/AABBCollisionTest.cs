namespace TestDIKUArcade.AabbCollisionTest;

using System;
using DIKUArcade.GUI;

public class AabbCollisionTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "AabbCollisionTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press 'Up', 'Down', 'Left' and 'Right' to move.";
        Console.WriteLine(help);
    }
}