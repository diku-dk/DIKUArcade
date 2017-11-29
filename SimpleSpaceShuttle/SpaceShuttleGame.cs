using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Strategies;
using OpenTK.Input;

namespace SimpleSpaceShuttle {
    public class SpaceShuttleGame {
        private readonly Window window;
        private DIKUArcade.Entities.EntityContainer entities;

        private DIKUArcade.Entities.EntityActor player;
        private const float PLAYER_VELOCITY = 0.001f;

        private bool[] keyPressed;

        private void KeyPressHandler(object o, OpenTK.Input.KeyboardKeyEventArgs args) {
            if (args.Key == Key.Escape) {
                window.CloseWindow();
                return;
            }
            keyPressed[(int)args.Key] = true;
        }
        private void KeyReleaseHandler(object o, OpenTK.Input.KeyboardKeyEventArgs args) {
            keyPressed[(int)args.Key] = false;
            Console.WriteLine(player.Entity.Position);
        }

        private void MovePlayer() {
            if (keyPressed[(int)Key.Up]) {
                player.Entity.Position.Y -= SpaceShuttleGame.PLAYER_VELOCITY;
            }
            if (keyPressed[(int)Key.Down]) {
                player.Entity.Position.Y += SpaceShuttleGame.PLAYER_VELOCITY;
            }
            if (keyPressed[(int)Key.Left]) {
                player.Entity.Position.X -= SpaceShuttleGame.PLAYER_VELOCITY;
            }
            if (keyPressed[(int)Key.Right]) {
                player.Entity.Position.X += SpaceShuttleGame.PLAYER_VELOCITY;
            }
        }

        public SpaceShuttleGame(Window win) {
            window = win;
            window.SetClearColor(new Vec3F(1.0f, 1.0f, 0.0f));
            window.AddKeyPressEventHandler(KeyPressHandler);
            window.AddKeyReleaseEventHandler(KeyReleaseHandler);

            entities = new EntityContainer();
            player = new EntityActor(new Entity(), new MovementStrategy(), new Image(new Texture()));
            player.Entity.MoveToPosition(new Vec2F());

            keyPressed = new bool[512];
            for (int i = 0; i < 512; i++) {
                keyPressed[i] = false;
            }
        }

        private void Update() {
            MovePlayer();
        }

        private void Render() {
            //player.RenderEntity();
        }

        public void GameLoop() {
            while (window.IsRunning()) {
                // check for incoming events
                window.PollEvents();

                // if should update
                Update();

                // if should render
                window.Clear();
                Render();

                // double buffering
                window.SwapBuffers();
            }
        }
    }
}