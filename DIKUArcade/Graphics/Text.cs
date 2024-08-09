namespace DIKUArcade.Graphics;

using System;
using System.IO;
using System.Numerics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using System.Reflection;
using System.Linq;

/// <summary>
/// Represents a text object that can be rendered on the screen. 
/// Supports customization of font size, font family, and text color.
/// </summary>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class with the specified text.
    /// The default font size is set to 50, and the default font family is set to a predefined family.
    /// </summary>
    /// <param name="text">
    /// The text string to be rendered.
    /// </param>
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

    /// <summary>
    /// Calculates the ideal extent of the text object relative to the specified width and height.
    /// </summary>
    /// <param name="width">
    /// The width to be used for scaling.
    /// </param>
    /// <param name="height">
    /// The height to be used for scaling.
    /// </param>
    /// <returns>
    /// The ideal extent of the text object as a <see cref="Vector2"/>.
    /// </returns>
    public Vector2 IdealExtent(int width, int height) {
        return originalExtent / new Vector2(width, height);
    }

    private Vector2 LowlevelMeasure() {
        return Lowlevel.measureTextCSharp(font, text) + Vector2.UnitY * 5;
    }

    /// <summary>
    /// Sets the text string for this <see cref="Text"/> object.
    /// </summary>
    /// <param name="newText">
    /// The new text string to be displayed.
    /// </param>
    public void SetText(string text) {
        this.text = text;
        path = Lowlevel.createText(text, font);
        originalExtent = LowlevelMeasure();
    }

    /// <summary>
    /// Sets the font size for this <see cref="Text"/> object.
    /// </summary>
    /// <param name="newSize">
    /// The new font size.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the specified font size is less than zero.
    /// </exception>
    public void SetFontSize(int size) {
        if (size < 0) {
            throw new ArgumentOutOfRangeException("Font size must be a positive integer.");
        }

        this.size = size;
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        originalExtent = LowlevelMeasure();
    }

    public void SetFont(Lowlevel.FontFamily fontFamily) {
        this.fontFamily = fontFamily;
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        originalExtent = LowlevelMeasure();
    }

    /// <summary>
    /// Changes the text color using RGB values.
    /// </summary>
    /// <param name="r">
    /// The red component of the color.
    /// </param>
    /// <param name="g">
    /// The green component of the color.
    /// </param>
    /// <param name="b">
    /// The blue component of the color.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if any of the color values are not within the range of 0 to 255.
    /// </exception>
    public void SetColor(byte r, byte g, byte b) {
        color = Lowlevel.fromRgb(r, g, b);
    }

    /// <summary>
    /// Changes the text color using RGBA values.
    /// </summary>
    /// <param name="r">
    /// The red component of the color.
    /// </param>
    /// <param name="g">
    /// The green component of the color.
    /// </param>
    /// <param name="b">
    /// The blue component of the color.
    /// </param>
    /// <param name="a">
    /// The alpha (opacity) component of the color.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if any of the color values are not within the range of 0 to 255.
    /// </exception>
    public void SetColor(byte r, byte g, byte b, byte a) {
        color = Lowlevel.fromRgba(r, g, b, a);
    }

    /// <summary>
    /// Changes the text color using a <see cref="Lowlevel.Color"/> object.
    /// </summary>
    /// <param name="newColor">
    /// The new color to be used.
    /// </param>
    public void SetColor(Lowlevel.Color color) {
        this.color = color;
    }

    /// <summary>
    /// Renders the text onto the currently active drawing window using the provided <see cref="WindowContext"/>.
    /// </summary>
    /// <param name="context">
    /// The <see cref="WindowContext"/> in which the text will be rendered.
    /// </param>
    /// <param name="shape">
    /// The <see cref="Shape"/> that specifies the position and extent of the rendered text.
    /// </param>
    public void Render(WindowContext context, Shape shape) {
        var windowMatrix = context.Camera.WindowMatrix(shape, originalExtent);
        var newPath = Lowlevel.transformPath(path, windowMatrix);
        Lowlevel.renderBrushPath(color, newPath, context.LowlevelContext);
    }
}
