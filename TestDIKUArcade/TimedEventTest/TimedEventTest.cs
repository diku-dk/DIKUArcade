namespace TestDIKUArcade.TimedEventTest;

using System;
using DIKUArcade.GUI;

public class TimedEventTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "TimedEventTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press 'T' to add timed event with ID 1 and 'Y' for a random ID.";
        Console.WriteLine(help);
    }
}