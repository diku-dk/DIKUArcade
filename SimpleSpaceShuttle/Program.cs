using System;
using System.Collections.Generic;
using DIKUArcade;

// TODO: No game project should ever need to reference OpenTK! (OpenTK.Input...)

namespace SimpleSpaceShuttle {
    internal class Program {
        public static void Main(string[] args) {
            var window = new Window("Simple space-shuttle game", 500, AspectRatio.R1X1);
            var game = new SpaceShuttleGame(window);

            game.GameLoop();
        }
    }
}