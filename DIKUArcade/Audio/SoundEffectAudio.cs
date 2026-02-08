namespace DIKUArcade.Audio;

using System;
using System.Reflection;
using Raylib_cs;

/// <summary>
/// Represents a sound effect in the game. Inherits from <see cref="Audio"/> class and
/// implements the abstract members to work with sounds. This class extends the <c>Audio</c>
/// class by adding a <see cref="PlaySoundMulti"/> method that allows for playing the sound
/// multiple times without overlapping.
/// </summary>
/// <remarks>
/// The class is designed to work with short audio files like .wav files. For longer audio files
/// please refer to the <see cref="MusicAudio"/> class.
/// </remarks>
public class SoundEffectAudio : Audio {
    private Sound sound;
    private Sound[] soundMulti = new Sound[10];
    private int currentSound = 0;
    private bool hasInitialized = false;

    /// <summary>
    /// Overrides abstract property from <see cref="Audio"/> class to get or set the volume
    /// of the sound instance.
    /// </summary>
    /// <remarks>
    /// Base volume is <c>0.8f</c>, min. volume is <c>0.0f</c> and max. volume is <c>1.0f</c>.
    /// </remarks>
    public override float Volume {
        get => volume;
        set {
            volume = Math.Clamp(value, 0.0f, 1.0f);
            Raylib.SetSoundVolume(sound, volume);
        }
    }

    /// <summary>
    /// Overrides abstract property from <see cref="Audio"/> class to get or set the pitch
    /// of the sound instance.
    /// </summary>
    /// <remarks>
    /// Base pitch is <c>1.0f</c>, min. pitch is <c>0.5f</c> and max. pitch is <c>1.5f</c>.
    /// </remarks>
    public override float Pitch {
        get => pitch;
        set {
            pitch = Math.Clamp(value, 0.5f, 1.5f);
            Raylib.SetSoundPitch(sound, pitch);
        }
    }

    /// <summary>
    /// Overrides abstract property from <see cref="Audio"/> class to get or set the pan
    /// of the sound instance. 
    /// </summary>
    /// <remarks>
    /// Base pan is <c>0.5f</c>, min. pan is <c>0.0f</c> and max. pan is <c>1.0f</c>.
    /// </remarks>
    public override float Pan {
        get => pan;
        set {
            pan = Math.Clamp(value, 0.0f, 1.0f);
            Raylib.SetSoundPan(sound, pan);
        }
    }

    /// <summary>
    /// Creates a <see cref="SoundEffectAudio"/> instance from an embedded resource, loads
    /// the sound and sets the volume.
    /// </summary>
    /// <param name="manifestResourceName">The name of the manifest resource</param>
    public SoundEffectAudio(string manifestResourceName)
        : base(manifestResourceName, Assembly.GetCallingAssembly()) {
        sound = Raylib.LoadSound(Path);
        Volume = volume;
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to play the sound. If the sound
    /// is already playing it will get reset.
    /// </summary>
    public override void Play() {
        if (Status == AudioStatus.Playing) {
            Raylib.StopSound(sound);
        }
        Raylib.PlaySound(sound);
        Status = AudioStatus.Playing;
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to stop the sound.
    /// </summary>
    public override void Stop() {
        Raylib.StopSound(sound);
        Status = AudioStatus.Stopped;
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to pause the sound. Only
    /// pauses the sound if it is playing.
    /// </summary>
    public override void Pause() {
        if (Status == AudioStatus.Playing) {
            Raylib.PauseSound(sound);
            Status = AudioStatus.Paused;
        }
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to resume the sound. Only
    /// resumes sound if it is paused.
    /// </summary>
    public override void Resume() {
        if (Status == AudioStatus.Paused) {
            Raylib.ResumeSound(sound);
            Status = AudioStatus.Playing;
        }
    }

    /// <summary>
    /// Overrides abstract method from <see cref="Audio"/> class to dispose the sound resources,
    /// by unloading the sound, deleting the temporary file and removing itself from the
    /// <see cref="AudioManager"/>. If <see cref="PlaySoundMulti(int)"/> has been used the
    /// sound aliases will be unloaded.
    /// </summary>
    public override void Dispose() {
        if (hasInitialized) {
            for (int i = 1; i < soundMulti.Length; i++) {
                Raylib.UnloadSoundAlias(soundMulti[i]);
            }
        }
        Raylib.UnloadSound(sound);
        this.DeleteTempFile();
        AudioManager.RemoveAudio(this);
    }

    /// <summary>
    /// Allows for playing the sound in rapid succession without overlapping or static noise.
    /// </summary>
    /// <param name="size">
    /// The number of sound aliases to load. If no parameter is passed the default is 10.
    /// </param>
    /// <remarks>
    /// This is intended for very short sounds to play rapidly. Example, creating machinegun
    /// sound effects. For playing sound without succession use <see cref="Play"/>.
    /// </remarks>
    public void PlaySoundMulti(int size = 10) {
        if (!hasInitialized || size > soundMulti.Length) {
            CreateSoundAliases(size);
        }

        Raylib.PlaySound(soundMulti[currentSound]);
        currentSound++;
        if (currentSound >= size) {
            currentSound = 0;
        }
    }

    /// <summary>
    /// Creates an array of soundaliases based on the sound and the <paramref name="size"/>
    /// </summary>
    private void CreateSoundAliases(int size) {
        if (hasInitialized) {
            for (int i = 1; i < soundMulti.Length; i++) {
                Raylib.UnloadSoundAlias(soundMulti[i]);
            }
        }

        soundMulti = new Sound[size];
        soundMulti[0] = sound;
        for (int i = 1; i < soundMulti.Length; i++) {
            soundMulti[i] = Raylib.LoadSoundAlias(sound);
        }
        hasInitialized = true;
    }

}
