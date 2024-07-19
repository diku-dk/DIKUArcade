namespace TestDIKUArcade.EntityRotationTest;

using System;
using DIKUArcade.GUI;

public class EntityRotationTest : ITestable {
    public void RunTest() {
        var windowArgs = new WindowArgs() {
            Title = "EntityRotationTest"
        };

        var game = new Game(windowArgs);
        game.Run();
    }

    public void Help() {
        var help = "Press '1' and '2' to rotate the entity.";
        Console.WriteLine(help);
    }
}