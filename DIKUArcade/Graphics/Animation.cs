using System;
using DIKUArcade.Entities;
using DIKUArcade.Timers;

namespace DIKUArcade.Graphics {
    public class Animation {
        /// <summary>
        /// Duration of this animation, in milliseconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Position and Extent of this animation
        /// </summary>
        public StationaryShape Shape { get; set; }

        /// <summary>
        /// ImageStride used for animation
        /// </summary>
        public ImageStride Stride { get; set; }

        private double timeOfCreation;

        public Animation() {
            timeOfCreation = StaticTimer.GetElapsedMilliseconds();
        }

        /// <summary>
        /// The animation is still considered active if the specified duration
        /// in milliseconds has not yet passed.
        /// </summary>
        public bool IsActive() {
            return timeOfCreation + Duration > StaticTimer.GetElapsedMilliseconds();
        }

        public void RenderAnimation() {
            Stride.Render(Shape);
        }

        public void ResetAnimation() {
            timeOfCreation = StaticTimer.GetElapsedMilliseconds();
        }
    }
}