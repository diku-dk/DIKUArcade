using System;

namespace DIKUArcade.Utilities {
    public class RandomGenerator {
        public static System.Random Generator { get; private set; }

        static RandomGenerator() {
            if (RandomGenerator.Generator == null) {
                RandomGenerator.Generator = new Random();
            }
        }
    }
}