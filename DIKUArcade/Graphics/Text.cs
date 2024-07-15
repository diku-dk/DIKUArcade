namespace DIKUArcade.Graphics;

using System;
using DIKUArcade.Entities;
using System.Numerics;
using DIKUArcade.GUI;
using System.Threading;
using System.Threading.Tasks;

public class Text {

    private Lowlevel.PathCollection path;
    private int size = 50;
    private Lowlevel.FontFamily fontFamily;
    private Lowlevel.Font font;
    private string text;
    private Lowlevel.Color color = Lowlevel.Color.White;
    private readonly Window window;
    private Matrix3x2 viewMatrix;
    private Matrix3x2 windowMatrix;
    private readonly object lockShape = new object();
    private StationaryShape shape = new StationaryShape(0, 0, 0, 0);
    public StationaryShape Shape {
        get {
            lock (lockShape) {
                return shape;
            }
        }
        set {
            lock (lockShape) {
                shape = value;
            }
        }
    }

    private Vector2 LowlevelExtent {
        get => window.MeasureText(text, font);
    }

    private void UpdateExtent(Vector2 extent) {
        viewMatrix = window.View(Shape.Extent);
        UpdatePosition(Shape.Position);
    }

    private void UpdatePosition(Vector2 position) {
        var windowPosition = Vector2.Transform(Shape.Position, viewMatrix);
        windowMatrix = Matrix3x2.CreateTranslation(windowPosition);
    }

    // This is completely insane. For some reason MeasureText gives the wrong size of text in the
    // beginning.
    // This should be thread safe since the window size is never changed because the window can not
    // be resized.
    private void Update() {
        if (Shape.Extent != Vector2.Zero)
            return;

        Shape.Extent = LowlevelExtent / window.WindowSize;
        Thread.Sleep(100);
    }

    public Text(Window window, string text, Vector2 position) {
        if (Lowlevel.fontFamilies.Count == 0) {
            throw new Exception("There are no fonts available.");
        }
        
        Shape.onExtentSet += UpdateExtent;
        Shape.onPositionSet += UpdatePosition;
        this.window = window;
        this.text = text;
        fontFamily = Lowlevel.fontFamilies[0];
        font = Lowlevel.makeFont(fontFamily, size);
        path = Lowlevel.createText(text, font);
        Shape.Extent = LowlevelExtent / window.WindowSize;
        Shape.Position = position;

        Task.Run(() => Update());
    }

    /// <summary>
    /// Set the text string for this Text object.
    /// </summary>
    /// <param name="newText">The new text string</param>
    public void SetText(string text) {
        this.text = text;
        path = Lowlevel.createText(text, font);
        Shape.Extent = LowlevelExtent / window.WindowSize;
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
        Shape.Extent = LowlevelExtent / window.WindowSize;
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
        Shape.Extent = LowlevelExtent / window.WindowSize;
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

    
    public void ScaleText(float scale) {
        Shape.Extent *= scale;
    }
    
    public void RenderText() {
        if (window.WindowContext is null)
            return;
        else if (Shape.Extent == Vector2.Zero)
            SetText(text);
        var newPath = Lowlevel.transformPath(path, windowMatrix);
        Lowlevel.renderBrushPath(color, newPath, window.WindowContext.Value.Get());
    }
}

