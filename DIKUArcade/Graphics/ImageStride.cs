using System;
using System.Diagnostics;

namespace DIKUArcade.Graphics {
    /// <summary>
    ///     Image stride to show animations
    /// </summary>
    public class ImageStride {
        private int _animFrequency;

        private double _lastTime;

        // TODO: Might be better to have static/singleton access to a global timer object
        // avoid having multiple (read: many) instances!
        private Stopwatch _timer;

        /// <summary>
        /// </summary>
        /// <param name="milliseconds">Time between consecutive frames</param>
        public ImageStride(int milliseconds) {
            _animFrequency = milliseconds;
            _timer = new Stopwatch();
            // TODO: start the timer here?
        }

        public void StartAnimationTimer() {
            _timer.Restart();
            _lastTime = _timer.ElapsedMilliseconds;
        }

        public void StopAnimationTimer() {
            _timer.Stop();
        }

        public void RenderNextFrame() {
            double elapsed = _timer.ElapsedMilliseconds;

            // the desired number of milliseconds has passed,
            // take some action (here: render next frame)
            if (elapsed - _lastTime > _animFrequency) {
                Console.Write("tic ");
                _lastTime = elapsed;
            }
            // otherwise render the current frame again
            else {
                // do something...
            }
        }
    }
}