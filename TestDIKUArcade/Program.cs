using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Strategies;
using OpenTK.Input;

namespace TestDIKUArcade {
    internal class Program {
        public static void Main(string[] args) {

            // Testing DIKUArcade...
            // ---- uncomment ONE line at a time ----
            //

            //TestSimpleEntityRendering.MainFunction();
            //TestImageStrideFormation.MainFunction();
            //TestEntityRotation.MainFunction();
            TestRenderText.MainFunction();
        }
    }
}