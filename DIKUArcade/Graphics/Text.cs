namespace DIKUArcade.Graphics;

using System;
using DIKUArcade.Entities;
using System.Numerics;
using DIKUArcade.GUI;
using System.Reflection;
using System.IO;
using System.Linq;
using SixLabors.Fonts;

public class Text : IBaseImage {
    private Lowlevel.PathCollection path;
    private int size = 50;
    private Lowlevel.FontFamily fontFamily;
    private Lowlevel.Font font;
    private string text;
    private Lowlevel.Color color = Lowlevel.Color.White;
    private Vector2 originalExtent;
    private readonly string[] fonts = {
        "DIKUArcade.Fonts.Pixeldroid.Botic.PixeldroidBoticRegular.ttf",
        "DIKUArcade.Fonts.Pixeldroid.Console.PixeldroidConsoleRegular.ttf",
        "DIKUArcade.Fonts.Pixeldroid.Console.PixeldroidConsoleRegularMono.ttf",
        "DIKUArcade.Fonts.Pixeldroid.Menu.PixeldroidMenuRegular.ttf"
    };
    private readonly Lowlevel.FontFamily[] fontFamilies;
    public Text(string text) {
        var assembly = Assembly.GetExecutingAssembly();

        fontFamilies = Lowlevel.createFontFamilies(
            fonts.Select(font => assembly.GetManifestResourceStream(font)!)
        ).ToArray();

        this.text = text;
        fontFamily = fontFamilies[2];
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        originalExtent = LowlevelMeasure();
    }
    
    
    public Vector2 IdealExtent(int width, int height) {
        return originalExtent / new Vector2(width, height);
    }

    public Vector2 IdealExtent() {
        return originalExtent / Window.CurrentFocus().WindowSize;
    }

    private Vector2 LowlevelMeasure() {
        return Lowlevel.measureTextCSharp(font, text) + Vector2.UnitY * 5;
    }

    /// <summary>
    /// Set the text string for this Text object.
    /// </summary>
    /// <param name="newText">The new text string</param>
    public void SetText(string text) {
        this.text = text;
        path = Lowlevel.createText(text, font);
        originalExtent = LowlevelMeasure();
    }

    /// <summary>
    /// Set the font size for this Text object.
    /// </summary>
    /// <param name="newSize">The new font size</param>
    /// <exception cref="ArgumentOutOfRangeException">Font size must be a
    /// positive integer.</exception>
    public void SetFontSize(int size) {
        if (size < 0) {
            // ReSharper disable once NotResolvedInText
            throw  new ArgumentOutOfRangeException("Font size must be a positive integer.");
        }

        this.size = size;
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        originalExtent = LowlevelMeasure();
    }

    /// <summary>
    /// Set the font for this Text object, if the font is installed.
    /// If the font is not installed defaults to Arial.
    /// </summary>
    /// <param name="fontfamily">The name of the font family</param>
    public void SetFont(Lowlevel.FontFamily fontFamily) {
        this.fontFamily = fontFamily;
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        originalExtent = LowlevelMeasure();
    }

    /// <summary>
    /// Change text color
    /// </summary>
    /// <param name="vec">Vector3 containing the RGB color values.</param>
    /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
    /// between 0 and 1.</exception>
    public void SetColor(int r, int g, int b) {
        color = Lowlevel.fromRgb(r, g, b);
    }

    /// <summary>
    /// Change text color
    /// </summary>
    /// <param name="vec">Vec4I containing the RGBA color values.</param>
    /// <exception cref="ArgumentOutOfRangeException">Color values must be
    /// between 0 and 255.</exception>
    public void SetColor(int r, int g, int b, int a) {
        color = Lowlevel.fromRgba(r, g, b, a);
    }

    /// <summary>
    /// Change text color
    /// </summary>
    /// <param name="newColor">System.Drawing.Color containing new color channel values.</param>
    public void SetColor(Lowlevel.Color color) {
        this.color = color;
    }

    public void Render(Shape shape, WindowContext context) {
        var windowMatrix = context.Camera.WindowMatrix(shape, originalExtent);
        var newPath = Lowlevel.transformPath(path, windowMatrix);
        Lowlevel.renderBrushPath(color, newPath, context.LowlevelContext);
    }

    public void Render(Shape shape) {
        var window = Window.CurrentFocus();
        if (window is null || window.WindowContext is null)
            throw new Exception("The window context must not be null.");
        
        Render(shape, window.WindowContext.Value);
    }
}

