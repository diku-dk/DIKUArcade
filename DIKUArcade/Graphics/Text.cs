using System;
using DIKUArcade.Entities;
using System.Numerics;
using DIKUArcade.GUI;

namespace DIKUArcade.Graphics {
    public class Text {

        /// <summary>
        /// The string value for the text
        /// </summary>

        private Lowlevel.Text lowlevelText;

        public Vector2 Position { get; private set; }

        public Text(string text, Vector2 position) {
            var fontSize = 50;
            if (Lowlevel.Text.FontFamilies.Count == 0) {
                throw new Exception("There are no fonts available.");
            }
            var fontfamily = Lowlevel.Text.FontFamilies[0];
            lowlevelText = new Lowlevel.Text(Vector2.Zero, text, Lowlevel.Color.White, fontfamily, fontSize);
            Position = position;
        }

        public Vector2 Extent(Vector2 windowSize) {
            return lowlevelText.Extent / windowSize;
        }

        /// <summary>
        /// Set the text string for this Text object.
        /// </summary>
        /// <param name="newText">The new text string</param>
        public void SetText(string newText) {
            lowlevelText.Text = newText;
        }

        /// <summary>
        /// Set the font size for this Text object.
        /// </summary>
        /// <param name="newSize">The new font size</param>
        /// <exception cref="ArgumentOutOfRangeException">Font size must be a
        /// positive integer.</exception>
        public void SetFontSize(int newSize) {
            if (newSize < 0) {
                // ReSharper disable once NotResolvedInText
                throw  new ArgumentOutOfRangeException("Font size must be a positive integer");
            }
            lowlevelText.Size = newSize;
        }

        /// <summary>
        /// Set the font for this Text object, if the font is installed.
        /// If the font is not installed defaults to Arial.
        /// </summary>
        /// <param name="fontfamily">The name of the font family</param>
        public void SetFont(Lowlevel.FontFamily fontfamily) {
            lowlevelText.FontFamily = fontfamily;
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vector3 containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetColor(int r, int g, int b) {
            lowlevelText.Color = Lowlevel.fromRgb(r, g, b);
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vec4I containing the RGBA color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be
        /// between 0 and 255.</exception>
        public void SetColor(int r, int g, int b, int a) {
            lowlevelText.Color = Lowlevel.fromRgba(r, g, b, a);
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="newColor">System.Drawing.Color containing new color channel values.</param>
        public void SetColor(Lowlevel.Color newColor) {
            lowlevelText.Color = newColor;
        }

        
        public void ScaleText(float scale) {
            lowlevelText.Scale(new Vector2(scale, scale));
        }
        
        public void RenderText(WindowContext ctx) {
            lowlevelText.SetPosition(Position);
            lowlevelText.Render(ctx.Get());
        }
    }
}
