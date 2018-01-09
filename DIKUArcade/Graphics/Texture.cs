using System;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL4;

namespace DIKUArcade.Graphics {
    public class Texture {
        private int textureID;

        public Texture(string filePath) {
            // create a texture id
            textureID = 0;

            // bind this new texture id

            // load image file

            // attach it to OpenGL context

            // set texture properties, filters, blending functions, etc.

            // unbind the texture
        }

        private void BindTexture() {
            GL.BindTexture(TextureTarget.Texture2D, this.textureID);
        }

        private void UnbindTexture() {
            GL.BindTexture(TextureTarget.Texture2D, 0); // 0 is invalid texture id
        }

        public void Render() {
            // bind this texture
            this.BindTexture();

            // draw this texture
            throw new NotImplementedException("TODO: Render Texture object");

            // unbind this texture
            this.UnbindTexture();
        }
    }
}