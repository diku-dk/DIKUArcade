namespace DIKUArcade.Graphics;

using System;
using System.IO;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

public class Texture {
    private readonly Lowlevel.Image originalImage;
    private Lowlevel.Image image;
    private int? prevWidth;
    private int? prevHeight;
    internal readonly int originalWidth;
    internal readonly int originalHeight;
    internal Vector2 origianlExtent;

    public Texture(string filename) {
        originalImage = Lowlevel.createImage(File.ReadAllBytes(filename));
        image = originalImage;

        var (width, height) = Lowlevel.measureImage(originalImage);
        originalWidth = width;
        originalHeight = height;
        origianlExtent = new Vector2(originalWidth, originalHeight);
    }

    public Texture(ReadOnlySpan<byte> bytes) {
        originalImage = Lowlevel.createImage(bytes);
        image = originalImage;

        var (width, height) = Lowlevel.measureImage(originalImage);
        originalWidth = width;
        originalHeight = height;
        origianlExtent = new Vector2(originalWidth, originalHeight);
    }

    public Texture(string filename, int currentStride, int stridesInImage) {
        if (currentStride < 0 || currentStride >= stridesInImage || stridesInImage < 0) {
            throw new ArgumentOutOfRangeException(
                $"Invalid stride numbers: ({currentStride}/{stridesInImage})");
        }
        originalImage = Lowlevel.createImage(File.ReadAllBytes(filename));
        var (w, height) = Lowlevel.measureImage(originalImage);
        var width = w / stridesInImage;
        originalImage = Lowlevel.cropImage(originalImage, width * currentStride, 0, width, height);
        image = originalImage;

        originalWidth = width;
        originalHeight = height;
        origianlExtent = new Vector2(originalWidth, originalHeight);
    }
    
    public void Render(WindowContext context, int x, int y, int width, int height) {
        if (width != prevWidth || height != prevHeight) {
            image = Lowlevel.setSizeImage(originalImage, width, height);
            prevWidth = width;
            prevHeight = height;
        }

        Lowlevel.renderImage(x, y, image, context.LowlevelContext);
    }
}

