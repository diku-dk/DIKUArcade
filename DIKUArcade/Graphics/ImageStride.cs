namespace DIKUArcade.Graphics;

using System;
using System.Collections.Generic;
using DIKUArcade.Timers;
using DIKUArcade.Entities;
using DIKUArcade.Utilities;
using DIKUArcade.GUI;

/// <summary>
/// Image stride to show animations based on a list of textures
/// and an animation frequency.
/// </summary>
public class ImageStride : IBaseImage {
    private int animFrequency;

    private double lastTime;
    private bool animate;

    private List<Texture> textures = new List<Texture>();
    private int maxImageCount;
    private int currentImageCount;

    /// <summary>
    /// This value is only added for random animation offset,
    /// e.g. 100 objects created at the same time with the same
    /// animation frequency will not change texture at the exact
    /// same time.
    /// </summary>
    private double timerOffset;

    private void Init(int milliseconds, IEnumerable<Image> images) {
        if (milliseconds < 0) {
            throw new ArgumentException("milliseconds must be a positive integer");
        }
        animFrequency = milliseconds;
        animate = true;

        int count = 0;
        foreach (Image img in images) {
            textures.Add(img.Texture);
            count++;
        }
        maxImageCount = count - 1;

        if (count == 0) {
            // ReSharper disable once NotResolvedInText
            throw new ArgumentNullException("at least one image file must be specified");
        }

        currentImageCount = RandomGenerator.Generator.Next(count);
        timerOffset = RandomGenerator.Generator.Next(100);
    }

    public ImageStride(int milliseconds, List<Image> images) {
        Init(milliseconds, images);
    }

    public ImageStride(int milliseconds, IEnumerable<Image> images) {
        Init(milliseconds, images);
    }

    public ImageStride(int milliseconds, params Image[] images) {
        Init(milliseconds, new List<Image>(images));
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
    public void Render(Shape shape, WindowContext context) {
        // measure elapsed time
        double elapsed = StaticTimer.GetElapsedMilliseconds() + timerOffset;

        // the desired number of milliseconds has passed, change texture stride
        if (animFrequency > 0 && animate && elapsed - lastTime > animFrequency) {
            lastTime = elapsed;

            currentImageCount =
                (currentImageCount >= maxImageCount) ? 0 : currentImageCount + 1;
        }

        // render the current texture object
        var position = context.Camera.WindowPosition(shape);
        var originalExtent = textures[currentImageCount].originalExtent;
        var extent = originalExtent * context.Camera.WindowExtentScaling(shape, originalExtent);
        textures[currentImageCount].Render(
            context,
            (int) position.X,
            (int) position.Y,
            (int) extent.X,
            (int) extent.Y
        );
    }

    public void Render(Shape shape) {
        var window = Window.CurrentFocus();
        if (window is null || window.WindowContext is null)
            throw new Exception("The window context must not be null.");
        
        Render(shape, window.WindowContext.Value);
    }
}