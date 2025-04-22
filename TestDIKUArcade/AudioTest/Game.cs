namespace TestDIKUArcade.AudioTest;

using System;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Audio;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;

public class Game : DIKUGame {
    MusicAudio music;
    SoundEffectAudio sound;
    Text[] texts = new Text[] {
        new Text("Click 'I' to play music", new Vector2(0.05f, 0.9f), 0.4f),
        new Text("Click 'O' to pause/resume music", new Vector2(0.05f, 0.85f), 0.4f),
        new Text("Click 'P' to stop music", new Vector2(0.05f, 0.8f), 0.4f),
        new Text("Click 'Up'/'Down' to increase/decrease volume", new Vector2(0.05f, 0.75f), 0.4f),
        new Text("Click 'W'/'S' to increase or decrease pitch", new Vector2(0.05f, 0.7f), 0.4f),
        new Text("Click 'E' to increase Pan (LEFT)", new Vector2(0.05f, 0.65f), 0.4f),
        new Text("Click 'D' to decrease Pan (RIGHT)", new Vector2(0.05f, 0.60f), 0.4f),
        new Text("Hold 'Space' to play multisound", new Vector2(0.05f, 0.55f), 0.4f),
    };

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        music = new MusicAudio("TestDIKUArcade.Assets.music.ogg");
        sound = new SoundEffectAudio("TestDIKUArcade.Assets.shot.wav");
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            switch (key) {
                case KeyboardKey.I:
                    music.Play();
                    break;
                case KeyboardKey.O:
                    if (music.Status == AudioStatus.Playing) {
                        music.Pause();
                    } else if (music.Status == AudioStatus.Paused) {
                        music.Resume();
                    }
                    break;
                case KeyboardKey.P:
                    music.Stop();
                    break;
                case KeyboardKey.Up:
                    music.Volume += 0.1f;
                    break;
                case KeyboardKey.Down:
                    music.Volume -= 0.1f;
                    break;
                case KeyboardKey.W:
                    music.Pitch += 0.01f;
                    break;
                case KeyboardKey.S:
                    music.Pitch -= 0.01f;
                    break;
                case KeyboardKey.E:
                    music.Pan += 0.1f;
                    break;
                case KeyboardKey.D:
                    music.Pan -= 0.1f;
                    break;
                case KeyboardKey.Space:
                    sound.PlaySoundMulti();
                    break;
            }
        }
    }

    public override void Render(WindowContext context) {
        foreach (Text text in texts) {
            text.Render(context);
        }

        string info = $"Status: {music.Status} "
            + $"| Vol: {MathF.Round(music.Volume * 100)}% "
            + $"| Pitch: {MathF.Round(music.Pitch * 100)}% "
            + $"| Pan: {MathF.Round(music.Pan * 100)}%";
        Text infoBar = new Text(info, new Vector2(0.05f, 0.1f), 0.37f);
        infoBar.SetColor(70, 70, 70);
        infoBar.Render(context);
    }

    public override void Update() {
        music.Update();
    }

}
