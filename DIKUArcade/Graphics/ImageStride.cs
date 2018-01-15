using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Timers;
using DIKUArcade.Entities;
using DIKUArcade.Utilities;

namespace DIKUArcade.Graphics {
    /// <summary>
    /// Image stride to show animations based on a list of textures
    /// </summary>
    public class ImageStride : IBaseImage {
        private int animFrequency;

        private double lastTime;
        private bool animate;

        private List<Texture> textures;
        private readonly int maxImageCount;
        private int currentImageCount = 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="imageFiles">List of image files to include in strides</param>
        /// <param name="milliseconds">Time between consecutive frames</param>
        public ImageStride(int milliseconds, params string[] imageFiles) {
            if (milliseconds < 0) {
                throw new ArgumentException("milliseconds must be a positive integer");
            }
            animFrequency = milliseconds;
            animate = true;

            int imgs = imageFiles.Length;
            if (imgs == 0) {
                throw new ArgumentNullException("at least one image file must be specified");
            }
            maxImageCount = imgs - 1;
            currentImageCount = RandomGenerator.Generator.Next(imgs);

            textures = new List<Texture>(imgs);
            foreach (string imgFile in imageFiles)
            {
                textures.Add(new Texture(imgFile));
            }
        }

        public ImageStride(int milliseconds, params Image[] images) {
            if (milliseconds < 0) {
                throw new ArgumentException("milliseconds must be a positive integer");
            }
            animFrequency = milliseconds;
            animate = true;

            int imgs = images.Length;
            if (imgs == 0) {
                throw new ArgumentNullException("at least one image file must be specified");
            }

            maxImageCount = imgs - 1;
            currentImageCount = RandomGenerator.Generator.Next(imgs);

            textures = new List<Texture>(imgs);
            foreach (Image img in images)
            {
                textures.Add(img.GetTexture());
            }
        }

        public void StartAnimation() {
            animate = true;
            lastTime = StaticTimer.GetCurrentTimeFrame();
        }

        public void StopAnimation() {
            animate = false;
        }

        public void SetAnimationFrequency(int milliseconds) {
            if (milliseconds < 0) {
                throw new ArgumentException("milliseconds must be a positive integer");
            }
            animFrequency = milliseconds;
        }

        public void ChangeAnimationFrequency(int millisecondsChange) {
            animFrequency += millisecondsChange;
            if (animFrequency < 0) {
                animFrequency = 0;
            }
        }

        public void Render(Shape shape) {
            // measure elapsed time
            double elapsed = StaticTimer.GetCurrentTimeFrame();

            // the desired number of milliseconds has passed, change texture stride
            if (animFrequency > 0 && animate && elapsed - lastTime > animFrequency) {
                lastTime = elapsed;

                currentImageCount =
                    (currentImageCount >= maxImageCount) ? 0 : currentImageCount + 1;
            }

            // render the current texture object
            textures[currentImageCount].Render(shape);
        }
    }
}