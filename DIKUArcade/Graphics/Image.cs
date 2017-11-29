using System;

namespace DIKUArcade.Graphics {
    public class Image : IBaseImage {

        private Texture texture;

        public Image(Texture texture) {
            this.texture = texture;
        }

        public void Render() {
            throw new NotImplementedException("TODO: Render Image using its texture data");
        }

        /// <summary>
        /// Change the active texture handle.
        /// </summary>
        /// <param name="tex"></param>
        /// <exception cref="ArgumentException">If argument is not of type Texture.</exception>
        public void ChangeTexture(ITexture tex) {
            // TODO: Is this comparison correct?
            if (tex.GetType() != typeof(Texture)) {
                throw new ArgumentException($"Argument must be of type Texture: {tex.GetType()}");
            }
            // this type cast should be okay
            this.texture = (Texture)tex;
        }
    }
}