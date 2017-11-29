using System;
using System.Diagnostics;

namespace DIKUArcade.Graphics {
    /// <summary>
    ///     Image stride to show animations
    /// </summary>
    public class ImageStride {
        private int animFrequency;

        private double lastTime;

        // TODO: Might be better to have static/singleton access to a global timer object
        // avoid having multiple (read: many) instances!
        private Stopwatch timer;

        /// <summary>
        /// </summary>
        /// <param name="milliseconds">Time between consecutive frames</param>
        public ImageStride(int milliseconds) {
            animFrequency = milliseconds;
            timer = new Stopwatch();
            // TODO: start the timer here?
        }

        public void StartAnimationTimer() {
            timer.Restart();
            lastTime = timer.ElapsedMilliseconds;
        }

        public void StopAnimationTimer() {
            timer.Stop();
        }

        public void RenderNextFrame() {
            double elapsed = timer.ElapsedMilliseconds;

            // the desired number of milliseconds has passed,
            // take some action (here: render next frame)
            if (elapsed - lastTime > animFrequency) {
                Console.Write("tic ");
                lastTime = elapsed;
            }
            // otherwise render the current frame again
            else {
                // do something...
            }
        }
    }
}