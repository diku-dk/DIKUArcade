using System;
using System.Threading;

namespace DIKUArcade.Graphics
{
    /// <summary>
    /// Image stride to show animations
    /// </summary>
    public class ImageStride
    {
        // TODO: Might be better to have static/singleton access to a global timer object
        // avoid having multiple (read: many) instances!
        private System.Diagnostics.Stopwatch _timer;
        private double _lastTime;
        private int _animFrequency;

        /// <summary>
        ///
        /// </summary>
        /// <param name="milliseconds">Time between consecutive frames</param>
        public ImageStride(int milliseconds)
        {
            _animFrequency = milliseconds;
            _timer = new System.Diagnostics.Stopwatch();
            // TODO: start the timer here?
        }

        public void StartAnimationTimer()
        {
            _timer.Restart();
            _lastTime = _timer.ElapsedMilliseconds;
        }
        public void StopAnimationTimer()
        {
            _timer.Stop();
        }

        public void RenderNextFrame()
        {
            double elapsed = _timer.ElapsedMilliseconds;

            // the desired number of milliseconds has passed,
            // take some action (here: render next frame)
            if (elapsed - _lastTime > _animFrequency)
            {
                System.Console.Write("tic ");
                _lastTime = elapsed;
            }
            // otherwise render the current frame again
            else
            {
                // do something...
            }
        }
    }
}