using System;
using System.ComponentModel;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

namespace DIKUArcade.Graphics {
    public class Image : IBaseImage {

        private Texture texture;
        private Window window;

        public Image(Window window, string imageFile) {
            this.window = window;
            texture = new Texture(imageFile);
        }

        public Image(Window window, Texture texture) {
            this.window = window;
            this.texture = texture;
        }

        public Image(string imageFile) {
            window = Window.CurrentFocus();
            texture = new Texture(imageFile);
        }

        public Image(Texture texture) {
            window = Window.CurrentFocus();
            this.texture = texture;
        }

        public void Render(Shape shape) {
        }

        public void Render(Shape shape, WindowContext ctx) {
            throw new NotImplementedException();
        }
    }
}