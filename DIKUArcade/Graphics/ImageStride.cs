using System;
using DIKUArcade.Timers;

namespace DIKUArcade.Graphics {
    /// <summary>
    ///     Image stride to show animations
    /// </summary>
    public class ImageStride : IBaseImage {
        private int animFrequency;

        private double lastTime;
        private bool animate;

        private DepthTexture texture;

        /// <summary>
        ///
        /// </summary>
        /// <param name="texture">Texture object.</param>
        /// <param name="milliseconds">Time between consecutive frames</param>
        public ImageStride(DepthTexture texture, int milliseconds) {
            animFrequency = milliseconds;
            animate = true;
            this.texture = texture;
        }

        public void StartAnimation() {
            animate = true;
            lastTime = StaticTimer.GetCurrentTimeFrame();
        }

        public void StopAnimation() {
            animate = false;
        }

        public void Render() {
            double elapsed = StaticTimer.GetCurrentTimeFrame();

            // the desired number of milliseconds has passed,
            // take some action (here: render next frame)
            if (animate && elapsed - lastTime > animFrequency) {
                lastTime = elapsed;

                throw new NotImplementedException("TODO: Render ImageStride using its texture data");
            }
            // otherwise render the current frame again
            else {
                throw new NotImplementedException("TODO: Render ImageStride using its texture data");
            }
        }

        /// <summary>
        /// Change the active texture handle.
        /// </summary>
        /// <param name="tex"></param>
        /// <exception cref="ArgumentException">If argument is not of type DepthTexture.</exception>
        public void ChangeTexture(ITexture tex) {
            // TODO: Is this comparison correct?
            if (tex.GetType() != typeof(DepthTexture)) {
                throw new ArgumentException($"Argument must be of type DepthTexture: {tex.GetType()}");
            }
            // this type cast should be okay
            this.texture = (DepthTexture)tex;
        }
    }
}