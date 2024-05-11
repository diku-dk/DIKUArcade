namespace DIKUArcade.Utilities;

using System;

public class RandomGenerator {
    public static Random Generator { get; private set; }

    static RandomGenerator() {
        if (RandomGenerator.Generator == null) {
            RandomGenerator.Generator = new Random();
        }
    }
}
