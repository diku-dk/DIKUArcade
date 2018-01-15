using System;
using System.Collections.Generic;
using DIKUArcade;

namespace SimpleSpaceShuttle {
    internal class Program {
        public static void Main(string[] args) {
            var window = new Window("Simple space-shuttle game", 500, AspectRatio.R1X1);
            var game = new SpaceShuttleGame(window);

            game.GameLoop();
        }
    }
}