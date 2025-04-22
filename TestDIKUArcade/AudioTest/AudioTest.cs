namespace TestDIKUArcade.AudioTest;

using System;
using DIKUArcade.GUI;

public class AudioTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "AudioTest",
            Width = 600
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Click the on keys shown in the game window to play audio!";
        Console.WriteLine(help);
    }
}

