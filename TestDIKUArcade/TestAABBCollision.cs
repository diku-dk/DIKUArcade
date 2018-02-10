using System;
using System.Collections.Generic;
using System.ComponentModel;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace TestDIKUArcade {
    public class TestAabbCollision : IGameEventProcessor<object> {
        private Window win;
        private GameEventBus<object> bus;
        private Entity player;
        private Entity wall;

        private float playerVelocity = 0.07f;

        public TestAabbCollision() {
            win = new Window("TestAabbCollision", 500, AspectRatio.R1X1);
            win.SetClearColor(System.Drawing.Color.Black);

            // just listen to input events for now
            bus = new GameEventBus<object>();
            bus.InitializeEventBus(new List<GameEventType>() {GameEventType.InputEvent});
            bus.Subscribe(GameEventType.InputEvent, this);

            win.RegisterEventBus(bus);

            player = new Entity(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
                new Image("Taxi.png"));
            wall = new Entity(new StationaryShape(new Vec2F(0.25f, 0.5f), new Vec2F(0.15f, 0.15f)),
                new Image("wall.jpeg"));
        }

        public void MovePlayer(Vec2F dir) {
            ((DynamicShape) player.Shape).Direction = dir;
            var collide = CollisionDetection.Aabb((DynamicShape) player.Shape, wall.Shape);
            if (collide.Collision) {
                Console.WriteLine($"CollisionDetection occured in direction {collide.CollisionDir}");
                dir *= collide.DirectionFactor;
            }
            player.Shape.Position += dir;
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                win.PollEvents();
                win.Clear();
                bus.ProcessEvents();
                player.RenderEntity();
                wall.RenderEntity();
                win.SwapBuffers();
            }
        }

        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            if (type != GameEventType.InputEvent) {
                throw new InvalidEnumArgumentException("type must be a GameEventType.InputEvent!");
            }
            if (gameEvent.Parameter1 != "KEY_PRESS") {
                return;
            }
            switch (gameEvent.Message) {
            case "KEY_ESCAPE":
                win.CloseWindow();
                break;
            case "KEY_LEFT":
                MovePlayer(new Vec2F(-playerVelocity, 0.0f));
                break;
            case "KEY_RIGHT":
                MovePlayer(new Vec2F(playerVelocity, 0.0f));
                break;
            case "KEY_UP":
                MovePlayer(new Vec2F(0.0f, playerVelocity));
                break;
            case "KEY_DOWN":
                MovePlayer(new Vec2F(0.0f, -playerVelocity));
                break;
            default:
                break;
            }
        }
    }
}