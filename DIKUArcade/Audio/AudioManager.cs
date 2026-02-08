namespace DIKUArcade.Audio;

using System;
using System.Collections.Generic;

/// <summary>
/// Static class to manage the game's <see cref="Audio"/> instances for later clean up.
/// Contains methods for adding audios, removing audios and cleaning up audios.
/// </summary>
public static class AudioManager {
    private static List<Audio> audioList = new List<Audio>();

    /// <summary>
    /// Adds an <see cref="Audio"/> instance to the <c>AudioManager</c>.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown if audio instance is already in the <c>AudioManager</c>.
    /// </exception>
    public static void AddAudio(Audio audio) {
        if (!audioList.Contains(audio)) {
            audioList.Add(audio);
        } else {
            throw new ArgumentException($"The audio instance {audio} already exists in the "
                    + "AudioManager.");
        }
    }

    /// <summary>
    /// Removes an <see cref="Audio"/> instance from the <c>AudioManager</c>.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown if audio instance isn't in the <c>AudioManager</c>.
    /// </exception>
    public static void RemoveAudio(Audio audio) {
        if (audioList.Contains(audio)) {
            audioList.Remove(audio);
        } else {
            throw new ArgumentException($"The audio instance {audio} doesn't exists in the "
                    + "AudioManager.");
        }
    }

    /// <summary>
    /// Cleans up the audio resources for all the audio instances in the <c>AudioManager</c>
    /// and closes the <see cref="AudioDevice"/>.
    /// </summary>
    /// <exception cref="Exception">
    /// Thrown if there was an issue cleaning up audio resources.
    /// </exception>
    public static void CleanUp() {
        try {
            while (audioList.Count > 0) {
                int index = audioList.Count - 1;
                audioList[index].Dispose();
            }
            AudioDevice.CloseAudioDevice();
        } catch (Exception e) {
            throw new Exception("Problem occured while trying to clean up audio resources. "
                    + $"Remember to clean up audio resources: {e.Message}");
        }
    }

}
