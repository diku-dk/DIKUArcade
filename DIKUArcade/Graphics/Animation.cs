namespace DIKUArcade.Graphics;

using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Timers;

/// <summary>
/// Represents an animation consisting of a series of images that are rendered in sequence
/// over a specified duration. The animation is controlled by its duration and the images
/// used in the animation stride.
/// </summary>
public class Animation {
    /// <summary>
    /// Gets or sets the total duration of the animation in milliseconds.
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Gets or sets the shape defining the position and extent of the animation on the screen.
    /// This should be a <see cref="StationaryShape"/> that defines where the animation appears.
    /// </summary>
    public StationaryShape? Shape { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="ImageStride"/> used for animating. This contains the sequence
    /// of images that are displayed as part of the animation.
    /// </summary>
    public ImageStride? Stride { get; set; }

    private double timeOfCreation;

    /// <summary>
    /// Initializes a new instance of the <see cref="Animation"/> class.
    /// Sets the time of creation to the current elapsed time.
    /// </summary>
    public Animation() {
        timeOfCreation = StaticTimer.GetElapsedMilliseconds();
    }

    /// <summary>
    /// Determines whether the animation is still active based on the elapsed time.
    /// The animation is considered active if the specified duration has not yet passed.
    /// </summary>
    /// <returns>
    /// True if the animation is active; otherwise, false.
    /// </returns>
    public bool IsActive() {
        return timeOfCreation + Duration > StaticTimer.GetElapsedMilliseconds();
    }

    /// <summary>
    /// Renders the animation on the specified window context. The animation is rendered using
    /// the image stride and shape properties.
    /// </summary>
    /// <param name="context">
    /// The <see cref="WindowContext"/> where the animation will be rendered.
    /// </param>
    public void RenderAnimation(WindowContext context) {
        Stride?.Render(context, Shape!);
    }

    /// <summary>
    /// Resets the animation by setting the time of creation to the current elapsed time.
    /// This effectively restarts the animation from the beginning.
    /// </summary>
    public void ResetAnimation() {
        timeOfCreation = StaticTimer.GetElapsedMilliseconds();
    }
}
