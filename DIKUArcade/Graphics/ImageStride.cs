namespace DIKUArcade.Graphics;

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using DIKUArcade.Timers;
using DIKUArcade.Entities;
using DIKUArcade.GUI;

/// <summary>
/// Represents an animated image based on a sequence of textures, displaying a different texture
/// at specified intervals to create animation effects. The animation is controlled by a frequency 
/// in milliseconds and can be started, stopped, or adjusted dynamically.
/// </summary>
public class ImageStride : IBaseImage {

    private static Random generator = new Random();
    private int animFrequency;
    private double lastTime;
    private bool animate;
    private List<Texture> textures = new List<Texture>();
    private int maxImageCount;
    private int currentImageCount;

    /// <summary>
    /// This value is used for adding a random offset to the animation timer to ensure that
    /// multiple objects with the same animation frequency do not change textures at the same time.
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
            throw new ArgumentNullException("at least one image file must be specified");
        }

        currentImageCount = generator.Next(count);
        timerOffset = generator.Next(100);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageStride"/> class with a specific animation frequency
    /// and a list of <see cref="Image"/> objects to use for the animation frames.
    /// </summary>
    /// <param name="milliseconds">
    /// The frequency of animation changes in milliseconds.
    /// </param>
    /// <param name="images">
    /// A list of <see cref="Image"/> objects representing the frames of the animation.
    /// </param>
    public ImageStride(int milliseconds, List<Image> images) {
        Init(milliseconds, images);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageStride"/> class with a specific animation frequency
    /// and an enumerable collection of <see cref="Image"/> objects to use for the animation frames.
    /// </summary>
    /// <param name="milliseconds">
    /// The frequency of animation changes in milliseconds.
    /// </param>
    /// <param name="images">
    /// An enumerable collection of <see cref="Image"/> objects representing the frames of the animation.
    /// </param>
    public ImageStride(int milliseconds, IEnumerable<Image> images) {
        Init(milliseconds, images);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageStride"/> class with a specific animation frequency
    /// and a variable number of <see cref="Image"/> objects to use for the animation frames.
    /// </summary>
    /// <param name="milliseconds">
    /// The frequency of animation changes in milliseconds.
    /// </param>
    /// <param name="images">
    /// An array of <see cref="Image"/> objects representing the frames of the animation.
    /// </param>
    public ImageStride(int milliseconds, params Image[] images) {
        Init(milliseconds, new List<Image>(images));
    }

    /// <summary>
    /// Creates a list of images representing different strides of an animated image from a single image file.
    /// </summary>
    /// <param name="numStrides">
    /// The total number of strides (frames) in the image.
    /// </param>
    /// <param name="manifestResourceName">
    /// he name of the embedded resource that makes up the image of the strides.
    /// </param>
    /// <returns>
    /// A list of <see cref="Image"/> objects, each corresponding to a stride of the image.
    /// </returns>
    public static List<Image> CreateStrides(int numStrides, string manifestResourceName) {
        var res = new List<Image>();
        var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(manifestResourceName);
        
        if (stream is null) {
            throw new Exception($"Resouce with name {manifestResourceName} does not exists. " +
             "Make sure the name is correct or you have remebered to embed the file using the " +
             ".csproj file.");
        }

        byte[] buffer = new byte[stream.Length];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(buffer, 0, bytesRead);

        for (int i = 0; i < numStrides; i++) {
            res.Add(new Image(new Texture(readOnlySpan, i, numStrides)));
        }
        return res;
    }

    /// <summary>
    /// Creates a list of images representing different strides of an animated image from a single image file.
    /// </summary>
    /// <param name="numStrides">
    /// The total number of strides (frames) in the image.
    /// </param>
    /// <param name="bytes">
    /// The bytes that make up the image of the strides.
    /// </param>
    /// <returns>
    /// A list of <see cref="Image"/> objects, each corresponding to a stride of the image.
    /// </returns>
    public static List<Image> CreateStrides(int numStrides, ReadOnlySpan<byte> bytes) {
        var res = new List<Image>();

        for (int i = 0; i < numStrides; i++) {
            res.Add(new Image(new Texture(bytes, i, numStrides)));
        }

        return res;
    }

    /// <summary>
    /// Starts or restarts the animation of this <see cref="ImageStride"/> object.
    /// </summary>
    public void StartAnimation() {
        animate = true;
        lastTime = StaticTimer.GetElapsedMilliseconds();
    }

    /// <summary>
    /// Stops the animation of this <see cref="ImageStride"/> object.
    /// </summary>
    public void StopAnimation() {
        animate = false;
    }

    /// <summary>
    /// Sets the animation frequency for this <see cref="ImageStride"/> object.
    /// </summary>
    /// <param name="milliseconds">
    /// The new frequency of animation changes in milliseconds.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided milliseconds is less than 0.
    /// </exception>
    public void SetAnimationFrequency(int milliseconds) {
        if (milliseconds < 0) {
            throw new ArgumentException("milliseconds must be a positive integer");
        }
        animFrequency = milliseconds;
    }

    /// <summary>
    /// Changes the animation frequency for this <see cref="ImageStride"/> object relatively.
    /// </summary>
    /// <param name="millisecondsChange">
    /// The amount by which to change the animation frequency in milliseconds.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the resulting frequency is less than 0.
    /// </exception>
    public void ChangeAnimationFrequency(int millisecondsChange) {
        animFrequency += millisecondsChange;
        if (animFrequency < 0) {
            animFrequency = 0;
        }
    }

    /// <summary>
    /// Renders the current frame of this <see cref="ImageStride"/> object in the specified window context.
    /// </summary>
    /// <param name="context">
    /// The context of the window where the image will be rendered.
    /// </param>
    /// <param name="shape">
    /// The shape that defines the position and dimensions of the image.
    /// </param>
    public void Render(WindowContext context, Shape shape) {
        // Measure elapsed time
        double elapsed = StaticTimer.GetElapsedMilliseconds() + timerOffset;

        // Change texture stride if the desired number of milliseconds has passed
        if (animFrequency > 0 && animate && elapsed - lastTime > animFrequency) {
            lastTime = elapsed;
            currentImageCount =
                (currentImageCount >= maxImageCount) ? 0 : currentImageCount + 1;
        }

        // Render the current texture object
        var extent = context.Camera.WindowExtent(shape);
        var position = context.Camera.WindowPosition(shape, extent);
        textures[currentImageCount].Render(
            context,
            (int) MathF.Round(position.X, MidpointRounding.AwayFromZero),
            (int) MathF.Round(position.Y, MidpointRounding.AwayFromZero),
            (int) MathF.Ceiling(extent.X) + 1,
            (int) MathF.Ceiling(extent.Y) + 1
        );
    }
}
