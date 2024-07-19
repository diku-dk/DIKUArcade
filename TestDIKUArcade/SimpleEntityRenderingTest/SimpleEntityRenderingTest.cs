namespace TestDIKUArcade.SimpleEntityRenderingTest;

using System;
using DIKUArcade.GUI;

public class SimpleEntityRenderingTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "SimpleEntityRenderingTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press 'Left' and 'Right' to move side to side. " + 
                   "'-' and '+' on the KeyPad will scale them up and down. " +
                   "'U' and 'I' will stretch the image on the X axis.";
        Console.WriteLine(help);
    }
}