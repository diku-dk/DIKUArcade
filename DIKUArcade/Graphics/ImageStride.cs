using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Timers;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Utilities;

namespace DIKUArcade.Graphics {
    /// <summary>
    /// Image stride to show animations based on a list of textures
    /// and an animation frequency.
    /// </summary>
    public class ImageStride : IBaseImage {
        private int animFrequency;

        private double lastTime;
        private bool animate;

        private List<Texture> textures;
        private readonly int maxImageCount;
        private int currentImageCount;

        /// <summary>
        /// This value is only added for random animation offset,
        /// e.g. 100 objects created at the same time with the same
        /// animation frequency will not change texture at the exact
        /// same time.
        /// </summary>
        private double timerOffset;

        /// <summary>
        ///
        /// </summary>
        /// <param name="milliseconds">Time between consecutive frames</param>
        /// <param name="imageFiles">List of image files to include in strides</param>
        public ImageStride(int milliseconds, params string[] imageFiles) {
            if (milliseconds < 0) {
                throw new ArgumentException("milliseconds must be a positive integer");
            }
            animFrequency = milliseconds;
            animate = true;

            int imgs = imageFiles.Length;
            if (imgs == 0) {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("At least one image file must be specified");
            }
            maxImageCount = imgs - 1;
            currentImageCount = RandomGenerator.Generator.Next(imgs);
            timerOffset = RandomGenerator.Generator.Next(100);

            textures = new List<Texture>(imgs);
            foreach (string imgFile in imageFiles)
            {
                textures.Add(new Texture(imgFile));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="milliseconds">Time between consecutive frames</param>
        /// <param name="images">List of images to include in strides</param>
        public ImageStride(int milliseconds, params Image[] images) {
            if (milliseconds < 0) {
                throw new ArgumentException("milliseconds must be a positive integer");
            }
            animFrequency = milliseconds;
            animate = true;

            int imgs = images.Length;
            if (imgs == 0) {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("at least one image file must be specified");
            }

            maxImageCount = imgs - 1;
            currentImageCount = RandomGenerator.Generator.Next(imgs);
            timerOffset = RandomGenerator.Generator.Next(100);

            textures = new List<Texture>(imgs);
            foreach (Image img in images)
            {
                textures.Add(img.GetTexture());
            }
        }

        public ImageStride(int milliseconds, List<Image> images) {
            if (milliseconds < 0) {
                throw new ArgumentException("milliseconds must be a positive integer");
            }
            animFrequency = milliseconds;
            animate = true;

            int imgs = images.Count;
            if (imgs == 0) {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("at least one image file must be specified");
            }

            maxImageCount = imgs - 1;
            currentImageCount = RandomGenerator.Generator.Next(imgs);
            timerOffset = RandomGenerator.Generator.Next(100);

            textures = new List<Texture>(imgs);
            foreach (Image img in images)
            {
                textures.Add(img.GetTexture());
            }
        }

        /// <summary>
        /// Create a List of images from an image stride file.
        /// </summary>
        /// <param name="numStrides">Total number of strides in the image</param>
        /// <param name="imagePath">The relative path to the image</param>
        /// <returns>A list of Image objects, each corresponding to a stride of the image.</returns>
        public static List<Image> CreateStrides(int numStrides, string imagePath) {
            var res = new List<Image>();

            for (int i = 0; i < numStrides; i++) {
                res.Add(new Image(new Texture(imagePath, i, numStrides)));
            }
            return res;
        }

        /// <summary>
        /// Restart animation for this ImageStride object
        /// </summary>
        public void StartAnimation() {
            animate = true;
            lastTime = StaticTimer.GetElapsedMilliseconds();
        }

        /// <summary>
        /// Halt animation for this ImageStride object
        /// </summary>
        public void StopAnimation() {
            animate = false;
        }

        /// <summary>
        /// Change the animation frequency for this ImageStride object
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <exception cref="ArgumentException">milliseconds must be a positive integer</exception>
        public void SetAnimationFrequency(int milliseconds) {
            if (milliseconds < 0) {
                throw new ArgumentException("milliseconds must be a positive integer");
            }
            animFrequency = milliseconds;
        }

        /// <summary>
        /// Relatively change the animation frequency for this ImageStride object
        /// </summary>
        /// <param name="millisecondsChange"></param>
        /// <exception cref="ArgumentException">milliseconds must be a positive integer</exception>
        public void ChangeAnimationFrequency(int millisecondsChange) {
            animFrequency += millisecondsChange;
            if (animFrequency < 0) {
                animFrequency = 0;
            }
        }

        /// <summary>
        /// Render this ImageStride object onto the currently active drawing window
        /// </summary>
        /// <param name="shape">The Shape object for the rendered image</param>
        public void Render(Shape shape) {
            // measure elapsed time
            double elapsed = StaticTimer.GetElapsedMilliseconds() + timerOffset;

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