namespace DIKUArcade.Audio;

using System;
using System.Reflection;
using Raylib_cs;

/// <summary>
/// Represents music in the game. Inherits from <see cref="Audio"/> class and
/// implements the abstract members to work with music. This class extends the <c>Audio</c>
/// class by adding a <see cref="Update"/> method that updates the music buffer for streaming
/// the music.
/// </summary>
/// <remarks>
/// The class is designed to work with longer audio files like .mp3 or .ogg files. For shorter
/// audio files please refer to the <see cref="SoundEffectAudio"/> class. OGG files are 
/// recommended for better sound quality.
/// </remarks>
public class MusicAudio : Audio {
    private Music music;

    /// <summary>
    /// Gets or sets whether the music should loop. 
    /// </summary>
    public bool Loop {
        get; set;
    }

    /// <summary>
    /// Overrides abstract property from <see cref="Audio"/> class to get or set the volume
    /// of the music instance.
    /// </summary>
    /// <remarks>
    /// Base volume is <c>0.8f</c>, min. volume is <c>0.0f</c> and max. volume is <c>1.0f</c>.
    /// </remarks>
    public override float Volume {
        get => volume;
        set {
            volume = Math.Clamp(value, 0.0f, 1.0f);
            Raylib.SetMusicVolume(music, volume);
        }
    }

    /// <summary>
    /// Overrides abstract property from <see cref="Audio"/> class to get or set the pitch
    /// of the music instance.
    /// </summary>
    /// <remarks>
    /// Base pitch is <c>1.0f</c>, min. pitch is <c>0.5f</c> and max. pitch is <c>1.5f</c>.
    /// </remarks>
    public override float Pitch {
        get => pitch;
        set {
            pitch = Math.Clamp(value, 0.5f, 1.5f);
            Raylib.SetMusicPitch(music, pitch);
        }
    }

    /// <summary>
    /// Overrides abstract property from <see cref="Audio"/> class to get or set the pan
    /// of the music instance. 
    /// </summary>
    /// <remarks>
    /// Base pan is <c>0.5f</c>, min. pan is <c>0.0f</c> and max. pan is <c>1.0f</c>.
    /// </remarks>
    public override float Pan {
        get => pan;
        set {
            pan = Math.Clamp(value, 0.0f, 1.0f);
            Raylib.SetMusicPan(music, pan);
        }
    }

    /// <summary>
    /// Creates a <see cref="MusicAudio"/> instance from an embedded resource, loads
    /// the music, sets the volume and sets the loop to false.
    /// </summary>
    /// <param name="manifestResourceName">The name of the manifest resource</param>
    public MusicAudio(string manifestResourceName)
        : base(manifestResourceName, Assembly.GetCallingAssembly()) {
        music = Raylib.LoadMusicStream(Path);
        Volume = volume;
        Loop = false;
    }

    /// <summary>
    /// Creates a <see cref="MusicAudio"/> instance from an embedded resource, loads
    /// the music, sets the volume and sets the loop to the <paramref name="loop"/> value.
    /// </summary>
    /// <param name="manifestResourceName">The name of the manifest resource</param>
    /// <param name="loop">whether the music should loop</param>
    public MusicAudio(string manifestResourceName, bool loop)
        : base(manifestResourceName, Assembly.GetCallingAssembly()) {
        music = Raylib.LoadMusicStream(Path);
        Volume = volume;
        Loop = loop;
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to play the music. If the music
    /// is already playing it will get reset.
    /// </summary>
    public override void Play() {
        if (Status == AudioStatus.Playing) {
            Raylib.StopMusicStream(music);
        }
        Raylib.PlayMusicStream(music);
        Status = AudioStatus.Playing;
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to stop the music.
    /// </summary>
    public override void Stop() {
        Raylib.StopMusicStream(music);
        Status = AudioStatus.Stopped;
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to pause the music. Only
    /// pauses the music if it is playing.
    /// </summary>
    public override void Pause() {
        if (Status == AudioStatus.Playing) {
            Raylib.PauseMusicStream(music);
            Status = AudioStatus.Paused;
        }
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to resume the music. Only
    /// resumes music if it is paused.
    /// </summary>
    public override void Resume() {
        if (Status == AudioStatus.Paused) {
            Raylib.ResumeMusicStream(music);
            Status = AudioStatus.Playing;
        }
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to dispose the music resources,
    /// by unloading the music, deleting the temporary file and removing itself from the
    /// <see cref="AudioManager"/>.     
    /// </summary>
    public override void Dispose() {
        Raylib.UnloadMusicStream(music);
        this.DeleteTempFile();
        AudioManager.RemoveAudio(this);
    }

    /// <summary>
    /// Updates the music buffer with new data. If <see cref="Loop"/> is false it will stop
    /// the music when it is finished.
    /// </summary>
    public void Update() {
        Raylib.UpdateMusicStream(music);
        float timePlayed = MathF.Round(Raylib.GetMusicTimePlayed(music));
        float timeLength = MathF.Round(Raylib.GetMusicTimeLength(music));
        // By default music gets looped
        if (!Loop) {
            if (timePlayed == timeLength) {
                this.Stop();
            }
        }
    }

}
