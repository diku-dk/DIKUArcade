using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using OpenTK.Mathematics;

namespace DIKUArcade.Graphics {
    public class Text {
        // TODO: Add method for centering text (vertically, horizontally) within its shape!
        /// <summary>
        /// OpenGL texture handle
        /// </summary>
        private int textureId;

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

        /// <summary>
        /// The color for the text
        /// </summary>
        private System.Drawing.Color color;

        /// <summary>
        /// The font family of the text.
        /// </summary>
        private System.Drawing.Font font;

        public Text(string text, Vec2F pos, Vec2F extent) {
            this.text = text;
            shape = new StationaryShape(pos, extent);
            color = System.Drawing.Color.Black;
            fontSize = 50;
            font = new Font("Arial", fontSize);

            // create a texture id
            textureId = GL.GenTexture();

            // bind this new texture id
            BindTexture();

            // set texture properties, filters, blending functions, etc.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);

            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);

            // unbind this new texture
            UnbindTexture();

            // create a texture
            CreateBitmapTexture();
        }

        // This method assumes that
        private void CreateBitmapTexture() {
            BindTexture();

            System.Drawing.Bitmap textBmp = new System.Drawing.Bitmap(500, 500); // match window size

            // just allocate memory, so we can update efficiently using TexSubImage2D
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textBmp.Width, textBmp.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

            using (System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(textBmp))
            {
                gfx.Clear(System.Drawing.Color.Transparent);
                // TODO: Could create an enumeration for choosing btw different font families!
                Font drawFont = font;
                SolidBrush drawBrush = new SolidBrush(color);

                // TODO: Maybe we should not use shape.Position here, because different coordinate system !!?
                System.Drawing.PointF drawPoint = new System.Drawing.PointF(shape.Position.X, shape.Position.Y);

                gfx.DrawString(text, drawFont, drawBrush, drawPoint); // Draw as many strings as you need
            }

            BitmapData data = textBmp.LockBits(new System.Drawing.Rectangle(0, 0, textBmp.Width, textBmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textBmp.Width, textBmp.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            textBmp.UnlockBits(data);

            UnbindTexture();
        }

        private void BindTexture() {
            GL.BindTexture(TextureTarget.Texture2D, textureId);
        }

        private void UnbindTexture() {
            GL.BindTexture(TextureTarget.Texture2D, 0); // 0 is invalid texture id
        }

        public StationaryShape GetShape() {
            return shape;
        }

        #region ChangeTextProperties

        /// <summary>
        /// Set the text string for this Text object.
        /// </summary>
        /// <param name="newText">The new text string</param>
        public void SetText(string newText) {
            text = newText;
            CreateBitmapTexture();
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
            // The loop below checks if said font is installed, if not defaults to Arial.
            var fontsCollection = new InstalledFontCollection();
            foreach (var fontFamily in fontsCollection.Families) {
                if (fontFamily.Name == fontfamily) {
                    font = new Font(fontfamily, fontSize);
                    break;
                }
            }

            CreateBitmapTexture();
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vec3F containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetColor(Vec3F vec) {
            if (vec.X < 0.0f || vec.X > 1.0f ||
                vec.Y < 0.0f || vec.Y > 1.0f ||
                vec.Z < 0.0f || vec.Z > 1.0f) {
                throw new ArgumentOutOfRangeException($"RGB Color values must be between 0 and 1: {vec}");
            }
            color = System.Drawing.Color.FromArgb((int)(vec.X * 255.0f), (int)(vec.Y * 255.0f), (int)(vec.Z * 255.0f));
            CreateBitmapTexture();
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vec3I containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be
        /// between 0 and 255.</exception>
        public void SetColor(Vec3I vec) {
            if (vec.X < 0 || vec.X > 255 ||
                vec.Y < 0 || vec.Y > 255 ||
                vec.Z < 0 || vec.Z > 255) {
                throw new ArgumentOutOfRangeException($"RGB Color values must be between 0 and 255: {vec}");
            }
            color = System.Drawing.Color.FromArgb(vec.X, vec.Y, vec.Z);
            CreateBitmapTexture();
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vec4I containing the ARGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be
        /// between 0 and 255.</exception>
        public void SetColor(int a, int r, int g, int b) {
            if (a < 0 || a > 255 ||
                r < 0 || r > 255 ||
                g < 0 || g > 255 ||
                b < 0 || b > 255) {
                throw new ArgumentOutOfRangeException($"ARGB Color values must be between 0 and 255: {a} {r} {g} {b}");
            }
            color = System.Drawing.Color.FromArgb(a, r, g, b);
            CreateBitmapTexture();
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vec4I containing the ARGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Color values must be
        /// between 0 and 255.</exception>
        public void SetColor(Vec4I vec) {
            if (vec.X < 0 || vec.X > 255 ||
                vec.Y < 0 || vec.Y > 255 ||
                vec.Z < 0 || vec.Z > 255 ||
                vec.W < 0 || vec.W > 255) {
                throw new ArgumentOutOfRangeException($"ARGB Color values must be between 0 and 255: {vec}");
            }
            color = System.Drawing.Color.FromArgb(vec.X, vec.Y, vec.Z, vec.W);
            CreateBitmapTexture();
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="vec">Vec3F containing the RGB color values.</param>
        /// <exception cref="ArgumentOutOfRangeException">Normalized color values must be
        /// between 0 and 1.</exception>
        public void SetColor(Vec4F vec) {
            if (vec.X < 0.0f || vec.X > 1.0f ||
                vec.Y < 0.0f || vec.Y > 1.0f ||
                vec.Z < 0.0f || vec.Z > 1.0f ||
                vec.W < 0.0f || vec.W > 1.0f) {
                throw new ArgumentOutOfRangeException($"ARGB Color values must be between 0 and 1: {vec}");
            }
            color = System.Drawing.Color.FromArgb((int)(vec.X * 255.0f), (int)(vec.Y * 255.0f), (int)(vec.Z * 255.0f), (int)(vec.W * 255.0f));
            CreateBitmapTexture();
        }

        /// <summary>
        /// Change text color
        /// </summary>
        /// <param name="newColor">System.Drawing.Color containing new color channel values.</param>
        public void SetColor(System.Drawing.Color newColor) {
            color = newColor;
            CreateBitmapTexture();
        }

        #endregion

        private Matrix4 CreateMatrix() {
            // ensure that rotation is performed around the center of the shape
            // instead of the bottom-left corner
            var halfX = shape.Extent.X / 2.0f;
            var halfY = shape.Extent.Y / 2.0f;

            return Matrix4.CreateTranslation(-halfX, -halfY, 0.0f) *
                   Matrix4.CreateRotationZ(shape.Rotation) *
                   Matrix4.CreateTranslation(shape.Position.X + halfX, shape.Position.Y + halfY,
                       0.0f);
        }
        
        public void ScaleText(float scale) {
            shape.Position *= scale;
            shape.Scale(scale);
        }
        
        public void RenderText() {
            // bind this texture
            BindTexture();

            // render this texture
            Matrix4 modelViewMatrix = CreateMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);

            GL.Color4(1f,1f,1f,1f);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1); GL.Vertex2(0.0f, 0.0f);                      // Top Left
            GL.TexCoord2(0, 0); GL.Vertex2(0.0f, shape.Extent.Y);            // Bottom Left
            GL.TexCoord2(1, 0); GL.Vertex2(shape.Extent.X, shape.Extent.Y);  // Bottom Right
            GL.TexCoord2(1, 1); GL.Vertex2(shape.Extent.X, 0.0f);            // Top Right

            GL.End();

            // unbind this texture
            UnbindTexture();
        }
    }
}
