namespace DIKUArcade.Audio;

using System.IO;
using Raylib_cs;

/// <summary>
/// Static class to open and close the audiodevice. Contains methods for opening and closing
/// the audiodevice.
/// </summary>
public static class AudioDevice {
    /// <summary>
    /// Gets whether the audiodevice is initlialized.
    /// </summary>
    public static bool IsInitialized {
        get; private set;
    }

    /// <summary>
    /// Opens the audiodevice if it hasn't been initlialized. Also creates a directory
    /// for the temporary files.
    /// </summary>
    public static void OpenAudioDevice() {
        if (!IsInitialized) {
            Raylib.InitAudioDevice();
            Raylib.SetAudioStreamBufferSizeDefault(4096);
            Directory.CreateDirectory("./tmp");
            IsInitialized = true;
        }
    }

    /// <summary>
    /// Closes the audiodevice if it has been initlialized. Also deletes the directory
    /// for the temporary files.
    /// </summary>
    public static void CloseAudioDevice() {
        if (IsInitialized) {
            Raylib.CloseAudioDevice();
            Directory.Delete("./tmp");
            IsInitialized = false;
        }
    }

}
