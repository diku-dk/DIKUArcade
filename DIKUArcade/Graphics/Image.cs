using System;
using DIKUArcade.Entities;

namespace DIKUArcade.Graphics {
    public class Image : IBaseImage {

        private Texture texture;

        public Image(string imageFile) {
            this.texture = new Texture(imageFile);
        }

        public void Render(Shape shape) {
            texture.Render(shape);
        }

        public Texture GetTexture() {
            return texture;
        }
    }
}