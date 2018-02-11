using System;
using DIKUArcade.Entities;

namespace DIKUArcade.Graphics {
    public class AnimationContainer {
        internal class OccupyValue<T> {
            public bool Occupied { get; set; }
            public T Value;
        }

        private OccupyValue<Animation>[] container;
        private int size;

        public AnimationContainer(int size) {
            if (size < 0) {
                throw new ArgumentOutOfRangeException(
                    $"Cannot instantiate Animation container with negative size: {size}");
            }

            container = new OccupyValue<Animation>[size];
            this.size = size;
            ResetContainer();
        }

        /// <summary>
        /// Clear this container of all bound animation objects
        /// </summary>
        public void ResetContainer() {
            for (int i = 0; i < size; i++) {
                container[i] = new OccupyValue<Animation>()
                    {Occupied = false, Value = new Animation() {
                        Duration = 0, Shape = new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f)
                    }};
            }
        }

        /// <summary>
        /// Add an animation to this container. Return true if successful, otherwise false.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="duration"></param>
        /// <param name="stride"></param>
        public bool AddAnimation(Shape shape, int duration, ImageStride stride) {
            for (int i = 0; i < size; i++) {
                var anim = container[i];
                if (!anim.Occupied) {
                    anim.Occupied = true;
                    anim.Value.Shape.Position = shape.Position;
                    anim.Value.Shape.Extent = shape.Extent;
                    anim.Value.Duration = duration;
                    anim.Value.Stride = stride;
                    anim.Value.ResetAnimation();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Render all animation objects currently bound to this container
        /// </summary>
        public void RenderAnimations() {
            foreach (var animation in container) {
                if (animation.Occupied && animation.Value.IsActive()) {
                    animation.Value.RenderAnimation();
                } else {
                    animation.Occupied = false;
                }
            }
        }
    }
}