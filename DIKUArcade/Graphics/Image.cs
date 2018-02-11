using System;
using DIKUArcade.Entities;

namespace DIKUArcade.Graphics {
    public class Image : IBaseImage {

        private Texture texture;

        public Image(string imageFile) {
            texture = new Texture(imageFile);
        }

        public Image(Texture texture) {
            this.texture = texture;
        }

        public void Render(Shape shape) {
            texture.Render(shape);
        }

        public Texture GetTexture() {
            return texture;
        }
    }
}