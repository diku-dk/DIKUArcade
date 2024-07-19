namespace TestDIKUArcade.DIKUGameTest;

using System;
using DIKUArcade.GUI;

public class DIKUGameTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "DIKUGameTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press '1' and '2' to change the background color and press 'F1' to" + 
                   "crash.";
        Console.WriteLine(help);
    }
}