namespace DIKUArcade.Graphics;

using System.Numerics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Font;

/// <summary>
/// Represents a text object that can be rendered on the screen, with support for positioning,
/// scaling, and custom font settings. This class wraps the <see cref="ImageText"/> class 
/// to handle the rendering of text with a specified font and color.
/// </summary>
public class Text {
    private readonly ImageText imageText;
    private readonly StationaryShape shape = new StationaryShape(Vector2.Zero, Vector2.One);

    /// <summary>
    /// Gets or sets the scale of the text. This affects the size of the rendered text.
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>
    /// Gets or sets the position of the text on the screen.
    /// </summary>
    public Vector2 Position { get; set; } = Vector2.Zero;

    public Vector2 Extent { get => Scale * idealExtent; }

    
    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class with the specified text, position, scale, and font family.
    /// </summary>
    /// <param name="text">The text string to be rendered.</param>
    /// <param name="position">The position of the text on the screen.</param>
    /// <param name="scale">The scale factor for the text size.</param>
    /// <param name="fontFamily">The font family to be used for rendering the text.</param>
    public Text(string text, Vector2 position, float scale, FontFamily fontFamily) {
        imageText = new ImageText(text, 100, fontFamily);
        Position = position;
        Scale = new Vector2(scale, scale);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class with the specified text, position, and scale.
    /// Uses a default font family.
    /// </summary>
    /// <param name="text">The text string to be rendered.</param>
    /// <param name="position">The position of the text on the screen.</param>
    /// <param name="scale">The scale factor for the text size.</param>
    public Text(string text, Vector2 position, float scale) {
        imageText = new ImageText(text, 100);
        Position = position;
        Scale = new Vector2(scale, scale);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class with the specified text and position.
    /// Uses a default font family and scale.
    /// </summary>
    /// <param name="text">The text string to be rendered.</param>
    /// <param name="position">The position of the text on the screen.</param>
    public Text(string text, Vector2 position) {
        imageText = new ImageText(text, 100);
        Position = position;
    }

    /// <summary>
    /// Sets the text string for this <see cref="Text"/> object.
    /// </summary>
    /// <param name="text">The new text string to be displayed.</param>
    public void SetText(string text) {
        imageText.SetText(text);
    }

    /// <summary>
    /// Sets the font family for this <see cref="Text"/> object.
    /// </summary>
    /// <param name="fontFamily">The new font family to be used.</param>
    public void SetFont(FontFamily fontFamily) {
        imageText.SetFont(fontFamily);
    }

    /// <summary>
    /// Changes the text color using RGB values.
    /// </summary>
    /// <param name="r">The red component of the color (0-255).</param>
    /// <param name="g">The green component of the color (0-255).</param>
    /// <param name="b">The blue component of the color (0-255).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the color values are not within the range of 0 to 255.</exception>
    public void SetColor(byte r, byte g, byte b) {
        imageText.SetColor(r, g, b);
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
        imageText.SetColor(r, g, b, a);
    }

    private int prevWidth = 0;
    private int prevHeight = 0;
    private Vector2 idealExtent = Vector2.Zero;

    /// <summary>
    /// Renders the text onto the currently active drawing window using the provided <see cref="WindowContext"/>.
    /// The position and scale of the text are taken into account when rendering.
    /// </summary>
    /// <param name="context">The <see cref="WindowContext"/> in which the text will be rendered.</param>
    public void Render(WindowContext context) {
        if (context.Window.Width != prevWidth || context.Window.Height != prevHeight) {
            idealExtent = imageText.IdealExtent(context.Window.Width, context.Window.Height);
        }

        shape.Position = Position;
        shape.Extent = Scale * idealExtent;
        imageText.Render(context, shape);
    }
}
