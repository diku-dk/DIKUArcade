using System;
using DIKUArcade.Entities;
using System.Numerics;

namespace DIKUArcade.Graphics {
    public class Text {

        /// <summary>
        /// The string value for the text
        /// </summary>
        private string text;

        /// <summary>
        /// The font size for the text string
        /// </summary>
        private int fontSize;

        /// <summary>
        /// The position and size of the text
        /// </summary>
        private StationaryShape shape;

        public Text(string text, Vector2 pos, Vector2 extent) {
            this.text = text;
            shape = new StationaryShape(pos, extent);
            fontSize = 50;
        }

        // This method assumes that
        private void CreateBitmapTexture() {

        }

        public StationaryShape GetShape() {
            return shape;
        }

        /// <summary>
        /// Set the text string for this Text object.
        /// </summary>
        /// <param name="newText">The new text string</param>
        public void SetText(string newText) {
            text = newText;
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
            fontSize = newSize;
            CreateBitmapTexture();
        }

        /// <summary>
        /// Set the font for this Text object, if the font is installed.
        /// If the font is not installed defaults to Arial.
        /// </summary>
        /// <param name="fontfamily">The name of the font family</param>
        public void SetFont(string fontfamily) {
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vector3 containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetColor(Vector3 vec) {
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vec4I containing the ARGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be
        /// between 0 and 255.</exception>
        public void SetColor(int a, int r, int g, int b) {

        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="newColor">System.Drawing.Color containing new color channel values.</param>
        public void SetColor(System.Drawing.Color newColor) {

        }

        
        public void ScaleText(float scale) {
        }
        
        public void RenderText() {
            
        }
    }
}
