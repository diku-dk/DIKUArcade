using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using DIKUArcade.Entities;

namespace DIKUArcade.Graphics {
    public class Texture {
        /// <summary>
        /// OpenGL texture handle
        /// </summary>
        private int textureId;

        public Texture(string filename) {
            // create a texture id
            textureId = GL.GenTexture();

            // bind this new texture id
            BindTexture();

            // find base path
            var dir = new DirectoryInfo(Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location));

            while (dir.Name != "bin") {
                dir = dir.Parent;
            }
            dir = dir.Parent;

            // load image file
            var path = Path.Combine(dir.FullName.ToString(), filename);
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Error: The file \"{path}\" does not exist.");
            }
            Bitmap image = new Bitmap(path);
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            // attach it to OpenGL context
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            // set texture properties, filters, blending functions, etc.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);

            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);

            // unbind the texture
            UnbindTexture();
        }

        public Texture(string filename, int currentStride, int stridesInImage) {
            if (currentStride < 0 || currentStride >= stridesInImage || stridesInImage < 0) {
                throw new ArgumentOutOfRangeException(
                    $"Invalid stride numbers: ({currentStride}/{stridesInImage})");
            }
            // create a texture id
            textureId = GL.GenTexture();

            // bind this new texture id
            BindTexture();

            // find base path
            var dir = new DirectoryInfo(Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location));

            while (dir.Name != "bin") {
                dir = dir.Parent;
            }
            dir = dir.Parent;

            // load image file
            var path = Path.Combine(dir.FullName.ToString(), filename);
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Error: The file \"{path}\" does not exist.");
            }
            Bitmap image = new Bitmap(path);
            var width = (int)((float)image.Width / (float)stridesInImage);
            var posX = currentStride * width;
            BitmapData data = image.LockBits(new Rectangle(posX, 0, width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            // attach it to OpenGL context
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            // set texture properties, filters, blending functions, etc.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);

            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);

            // unbind the texture
            UnbindTexture();
        }

        private void BindTexture() {
            GL.BindTexture(TextureTarget.Texture2D, textureId);
        }

        private void UnbindTexture() {
            GL.BindTexture(TextureTarget.Texture2D, 0); // 0 is invalid texture id
        }

        private Matrix4 CreateMatrix(Shape shape) {
            // ensure that rotation is performed around the center of the shape
            // instead of the bottom-left corner
            var halfX = shape.Extent.X / 2.0f;
            var halfY = shape.Extent.Y / 2.0f;

            return Matrix4.CreateTranslation(-halfX, -halfY, 0.0f) *
                   Matrix4.CreateRotationZ(shape.Rotation) *
                   Matrix4.CreateTranslation(shape.Position.X + halfX, shape.Position.Y + halfY,
                       0.0f);
        }

        public void Render(Shape shape) {
            // bind this texture
            BindTexture();

            // render this texture
            Matrix4 modelViewMatrix = CreateMatrix(shape);
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
