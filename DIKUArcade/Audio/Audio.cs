namespace DIKUArcade.Audio;

using System;
using System.IO;
using System.Reflection;

/// <summary>
/// Abstract base class representing audio within the game. Handles creation and deletion
/// of temporary audio files from embedded resources, storing their paths for playback.
/// </summary>
/// <remarks>
/// Declares abstract members for audio control (volume, pitch, pan) and playback
/// (play, stop, pause, resume), which must be implemented by derived classes.
/// Implements <see cref="IDisposable"/> to ensure cleanup of temporary files.
/// Designed for inheritance for specific audio subclasses <see cref="SoundEffectAudio"/> and 
/// <see cref="MusicAudio"/>
/// </remarks>
public abstract class Audio : IDisposable {
    protected float volume = 0.8f;
    protected float pitch = 1.0f;
    protected float pan = 0.5f;

    /// <summary>
    /// Gets the path of the audio's temporary audio file.
    /// </summary>
    protected string Path {
        get; private set;
    }

    /// <summary>
    /// Gets the current audio status of the audio instance.
    /// </summary>
    public AudioStatus Status { get; protected set; } = AudioStatus.Stopped;

    /// <summary>
    /// Sets up audio infrastructure for subclasses. Creates the temporary audio file, adds audio
    /// instance to the <see cref="AudioManager"/> and initializes <see cref="AudioDevice"/>, 
    /// if needed.
    /// </summary>
    /// <remarks>
    /// This constructor does not initialize an audio instance. It sets up common audio logic
    /// for audio classes.
    /// </remarks>
    /// <param name="assembly">The calling assembly</param>
    /// <param name="manifestResourceName">The name of the manifest resource</param>
    /// <exception cref="Exception">
    /// Thrown if audio initialization fails. Automatically
    /// disposes audio instances and temporary files.
    /// </exception>
    protected Audio(string manifestResourceName, Assembly assembly) {
        try {
            if (!AudioDevice.IsInitialized) {
                AudioDevice.OpenAudioDevice();
            }
            Path = CreateTempFile(assembly, manifestResourceName);
            AudioManager.AddAudio(this);
        } catch (Exception e) {
            if (this.Path != null) {
                this.Dispose();
            }

            AudioManager.CleanUp();
            throw new Exception($"Problem creating Audio instance: {e.Message}");
        }
    }

    /// <summary>
    /// Gets stream from the manifest resource and creates a temporary audio file. 
    /// </summary>
    /// <param name="assembly">The calling assembly</param>
    /// <param name="manifestResourceName">The name of the manifest resource</param>
    /// <returns>
    /// The path to the temporary audio file.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if manifest resource doesn't exist
    /// </exception>
    private string CreateTempFile(Assembly assembly, string manifestResourceName) {
        using (Stream? stream = assembly.GetManifestResourceStream(manifestResourceName)) {
            if (stream is null) {
                throw new ArgumentNullException($"Resource: {manifestResourceName} does not exist. "
                        + "Make sure the name is correct and you have embedded the file using the "
                        + ".csproj file.");
            }

            string[] splitString = manifestResourceName.Split(".");
            string fileName = splitString[splitString.Length - 2];
            string fileType = splitString[splitString.Length - 1];
            string filePath = $"./tmp/{fileName}.{fileType}";

            // Copy stream data to temporary file
            using (FileStream file = File.Create(filePath)) {
                stream.CopyTo(file);
            }

            return filePath;
        }
    }

    /// <summary>
    /// Deletes the temporary audio file associated to the audio instance.
    /// </summary>
    protected void DeleteTempFile() {
        File.Delete(Path);
    }

    /// <summary>
    /// Property to be overridden by subclasses to control volume.
    /// </summary>
    public abstract float Volume {
        get; set;
    }

    /// <summary>
    /// Property to be overridden by subclasses to control pitch.
    /// </summary>
    public abstract float Pitch {
        get; set;
    }

    /// <summary>
    /// Property to be overridden by subclasses to control pan.
    /// </summary>
    public abstract float Pan {
        get; set;
    }

    /// <summary>
    /// Method to be overridden by subclasses for playing audio.
    /// </summary>
    public abstract void Play();

    /// <summary>
    /// Method to be overridden by subclasses for stopping audio.
    /// </summary>
    public abstract void Stop();

    /// <summary>
    /// Method to be overridden by subclasses for pausing audio.
    /// </summary>
    public abstract void Pause();

    /// <summary>
    /// Method to be overridden by subclasses for resuming audio.
    /// </summary>
    public abstract void Resume();

    /// <summary>
    /// Method to be overridden by subclasses for disposing audio resources.
    /// </summary>
    public abstract void Dispose();
}
