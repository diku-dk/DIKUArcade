namespace TestDIKUArcade.KeyEventsTest;

using System;
using DIKUArcade.GUI;

public class KeyEventsTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "KeyEventsTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press '1' and '2' to change background color and 'I' to add an element to " + 
                   "the dictionary.";
        Console.WriteLine(help);
    }
}