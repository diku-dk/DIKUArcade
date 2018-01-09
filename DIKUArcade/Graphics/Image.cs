using System;

namespace DIKUArcade.Graphics {
    public class Image : IBaseImage {

        private Texture texture;

        public Image(string imageFile) {
            this.texture = new Texture(imageFile);
        }

        public void Render() {
            texture.Render();
        }
    }
}