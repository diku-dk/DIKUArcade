namespace DIKUArcade.Graphics;

using System;
using System.Text;
using System.Numerics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Font;

/// <summary>
/// Represents a text object that can be rendered on the screen. 
/// Supports customization of font size, font family, and text color.
/// </summary>
public class ImageText : IBaseImage {
    private Lowlevel.PathCollection path;
    private int size = 50;
    private Lowlevel.FontFamily fontFamily = FontFamily.DefaultFontFamilies[2].fontFamily;
    private Lowlevel.Font font;
    private string text;
    internal Vector2 LowlevelMeasurements { get; private set; }
    private Lowlevel.Color color = Lowlevel.Color.White;
    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class with the specified text, font size, and font family.
    /// </summary>
    /// <param name="text">The text string to be rendered.</param>
    /// <param name="size">The size of the font. Must be a positive integer.</param>
    /// <param name="fontFamily">The font family to be used for rendering the text.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified font size is less than zero.</exception>
    public ImageText(string text, int size, FontFamily fontFamily) {
        if (size < 0) {
            throw new ArgumentOutOfRangeException("Font size must be a positive integer.");
        }

        this.text = text;
        this.size = size;
        this.fontFamily = fontFamily.fontFamily;
        font = Lowlevel.makeFont(this.fontFamily, size);
        path = Lowlevel.createText(text, font);
        LowlevelMeasurements = LowlevelMeasure();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class with the specified text and font size.
    /// Uses a predefined default font family.
    /// </summary>
    /// <param name="text">The text string to be rendered.</param>
    /// <param name="size">The size of the font. Must be a positive integer.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified font size is less than zero.</exception>
    public ImageText(string text, int size) {
        if (size < 0) {
            throw new ArgumentOutOfRangeException("Font size must be a positive integer.");
        }

        this.text = text;
        this.size = size;
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        LowlevelMeasurements = LowlevelMeasure();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class with the specified text.
    /// Uses a default font size of 50 and a predefined default font family.
    /// </summary>
    /// <param name="text">The text string to be rendered.</param>
    public ImageText(string text) {
        this.text = text;
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        LowlevelMeasurements = LowlevelMeasure();
    }

    /// <summary>
    /// Calculates the ideal extent of the text object relative to the specified width and height.
    /// </summary>
    /// <param name="width">The width to be used for scaling.</param>
    /// <param name="height">The height to be used for scaling.</param>
    /// <returns>The ideal extent of the text object as a <see cref="Vector2"/>.</returns>
    public Vector2 IdealExtent(int width, int height) {
        return LowlevelMeasurements / new Vector2(width, height);
    }
    
    // Very hacky but it works for most cases, for some reason spaces ruins the text measurements.
    private string Replacer(string input) {
        StringBuilder result = new StringBuilder();

        foreach (char c in input) {
            if (c == ' ') {
                result.Append('M');
            }
            else if (c == '\t') {
                result.Append("MM");
            }
            else {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Measures the size of the text using the current font and text string.
    /// </summary>
    /// <returns>A <see cref="Vector2"/> representing the width and height of the text.</returns>
    private Vector2 LowlevelMeasure() {
        return Lowlevel.measureTextCSharp(font, Replacer(text));
    }

    /// <summary>
    /// Sets the text string for this <see cref="Text"/> object.
    /// </summary>
    /// <param name="text">The new text string to be displayed.</param>
    public void SetText(string text) {
        this.text = text;
        path = Lowlevel.createText(this.text, font);
        LowlevelMeasurements = LowlevelMeasure();
    }

    /// <summary>
    /// Sets the font size for this <see cref="Text"/> object.
    /// </summary>
    /// <param name="size">The new font size. Must be a positive integer.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified font size is less than zero.</exception>
    public void SetFontSize(int size) {
        if (size < 0) {
            throw new ArgumentOutOfRangeException("Font size must be a positive integer.");
        }

        this.size = size;
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        LowlevelMeasurements = LowlevelMeasure();
    }

    /// <summary>
    /// Sets the font family for this <see cref="Text"/> object.
    /// </summary>
    /// <param name="fontFamily">The new font family to be used.</param>
    public void SetFont(FontFamily fontFamily) {
        this.fontFamily = fontFamily.fontFamily;
        font = Lowlevel.makeFont(this.fontFamily, size);
        path = Lowlevel.createText(text, font);
        LowlevelMeasurements = LowlevelMeasure();
    }

    /// <summary>
    /// Changes the text color using RGB values.
    /// </summary>
    /// <param name="r">The red component of the color (0-255).</param>
    /// <param name="g">The green component of the color (0-255).</param>
    /// <param name="b">The blue component of the color (0-255).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the color values are not within the range of 0 to 255.</exception>
    public void SetColor(byte r, byte g, byte b) {
        color = Lowlevel.fromRgb(r, g, b);
    }

    /// <summary>
    /// Changes the text color using RGBA values.
    /// </summary>
    /// <param name="r">The red component of the color (0-255).</param>
    /// <param name="g">The green component of the color (0-255).</param>
    /// <param name="b">The blue component of the color (0-255).</param>
    /// <param name="a">The alpha (opacity) component of the color (0-255).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the color values are not within the range of 0 to 255.</exception>
    public void SetColor(byte r, byte g, byte b, byte a) {
        color = Lowlevel.fromRgba(r, g, b, a);
    }

    /// <summary>
    /// Changes the text color using a <see cref="Lowlevel.Color"/> object.
    /// </summary>
    /// <param name="color">The new color to be used.</param>
    internal void SetColor(Lowlevel.Color color) {
        this.color = color;
    }

    /// <summary>
    /// Renders the text onto the currently active drawing window using the provided <see cref="WindowContext"/>.
    /// </summary>
    /// <param name="context">The <see cref="WindowContext"/> in which the text will be rendered.</param>
    /// <param name="shape">The <see cref="Shape"/> that specifies the position and extent of the rendered text.</param>
    public void Render(WindowContext context, Shape shape) {
        var windowMatrix = context.Camera.WindowMatrix(shape.Position + new Vector2(0, shape.Extent.Y), shape.Extent, LowlevelMeasurements);
        var newPath = Lowlevel.transformPath(path, windowMatrix);
        Lowlevel.renderBrushPath(color, newPath, context.LowlevelContext);
    }
}
