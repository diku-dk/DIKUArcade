namespace DIKUArcade.Graphics;

using System;
using System.IO;

using DIKUArcade.Entities;

public class Texture {

    private Lowlevel.Image rawImage;
    private Lowlevel.Image image;

    public Texture(string filename) {
        rawImage = Lowlevel.createImage(File.ReadAllBytes(filename));
        image = rawImage;
    }

    public Texture(ReadOnlySpan<byte> bytes) {
        rawImage = Lowlevel.createImage(bytes);
        image = rawImage;
    }

    public Texture(string filename, int currentStride, int stridesInImage) {
        if (currentStride < 0 || currentStride >= stridesInImage || stridesInImage < 0) {
            throw new ArgumentOutOfRangeException(
                $"Invalid stride numbers: ({currentStride}/{stridesInImage})");
        }
        rawImage = Lowlevel.createImage(File.ReadAllBytes(filename));
        image = rawImage;
    }
    
    public void Render(Shape shape) {

    }
}

