using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
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
            //Console.WriteLine($"DIR:        {dir}");
            //Console.WriteLine($"DIR.Name:   {dir.Name}");
            //Console.WriteLine($"DIR.Parent: {dir.Parent}");
            //Console.WriteLine($"DIR.Root:   {dir.Root}");
            while (dir.Parent.Name != "DIKUArcade") {
                dir = dir.Parent;
            }

            // load image file
            var path = Path.Combine(dir.ToString(), filename);
            if (!System.IO.File.Exists(path)) {
                //Console.WriteLine($"filename is {path}");
                //Console.WriteLine($"DirectoryName: {System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}");
                //Console.WriteLine($"Location: {System.Reflection.Assembly.GetExecutingAssembly().Location}");
                //Console.WriteLine($"Codebase: {System.Reflection.Assembly.GetExecutingAssembly().CodeBase}");
                throw new FileNotFoundException($"Error: The file \"{filename}\" does not exist.");
            }
            Bitmap image = new Bitmap(path);
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

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
            GL.BindTexture(TextureTarget.Texture2D, this.textureId);
        }

        private void UnbindTexture() {
            GL.BindTexture(TextureTarget.Texture2D, 0); // 0 is invalid texture id
        }

        private Matrix4 CreateMatrix(Entity entity)
        {
            return  Matrix4.CreateScale(1f, 1f, 1f)*
                    Matrix4.CreateRotationZ(entity.Rotation)*
                    Matrix4.CreateTranslation(entity.Position.X, entity.Position.Y, 0.0f);
        }

        public void Render(Entity entity) {
            // bind this texture
            this.BindTexture();

            // draw this texture
            // TODO: Render Texture object");
            Matrix4 modelViewMatrix = CreateMatrix(entity);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);

            GL.Color4(1f,1f,1f,1f);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1); GL.Vertex2(0.0f, 0.0f);                       // Top Left
            GL.TexCoord2(0, 0); GL.Vertex2(0.0f, entity.Extent.Y);            // Bottom Left
            GL.TexCoord2(1, 0); GL.Vertex2(entity.Extent.X, entity.Extent.Y); // Bottom Right
            GL.TexCoord2(1, 1); GL.Vertex2(entity.Extent.X, 0.0f);            // Top Right

            GL.End();

            // unbind this texture
            this.UnbindTexture();
        }
    }
}