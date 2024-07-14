using System;
using DIKUArcade.Entities;
using System.Numerics;
using DIKUArcade.GUI;

namespace DIKUArcade.Graphics {
    public class Text {

        /// <summary>
        /// The string value for the text
        /// </summary>

        private Lowlevel.PathCollection path;
        private int size = 50;
        private Lowlevel.FontFamily fontFamily;
        private Lowlevel.Font font;
        private string text;
        private Lowlevel.Color color = Lowlevel.Color.Black;
        public Vector2 Position { get; set; }
        static Matrix3x2 Move(Vector2 extent, int width, int height) {
            return new Matrix3x2(
                width, 0.0f,
                0.0f, -height,
                0.0f, height + -extent.Y
            );
        }

        public Text(string text, Vector2 position) {
            if (Lowlevel.fontFamilies.Count == 0) {
                throw new Exception("There are no fonts available.");
            }
            Position = position;
            this.text = text;
            fontFamily = Lowlevel.fontFamilies[0];
            font = Lowlevel.makeFont(fontFamily, size);
            path = Lowlevel.createText(text, font);
        }

        /// <summary>
        /// Set the text string for this Text object.
        /// </summary>
        /// <param name="newText">The new text string</param>
        public void SetText(string text) {
            this.text = text;
            path = Lowlevel.createText(text, font);
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
                throw  new ArgumentOutOfRangeException("Font size must be a positive integer");
            }
            this.size = size;
            font = Lowlevel.makeFont(fontFamily, size);
            path = Lowlevel.createText(text, font);
        }

        /// <summary>
        /// Set the font for this Text object, if the font is installed.
        /// If the font is not installed defaults to Arial.
        /// </summary>
        /// <param name="fontfamily">The name of the font family</param>
        public void SetFont(Lowlevel.FontFamily fontfamily) {
            this.fontFamily = fontfamily;
            font = Lowlevel.makeFont(fontFamily, size);
            path = Lowlevel.createText(text, font);
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
            // lowlevelText.Scale(new Vector2(scale, scale));
        }
        
        public void RenderText(WindowContext ctx) {
            Lowlevel.renderBrushPath(color, path, ctx.Get());
        }
    }
}
