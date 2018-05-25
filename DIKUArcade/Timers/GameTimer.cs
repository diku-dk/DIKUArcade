using System;
using System.Diagnostics;

namespace DIKUArcade.Timers {
    public class GameTimer {
        private double lastTime;
        private double timer;
        private double updateTimeLimit;
        private double renderTimeLimit;
        private double deltaUpdateTime;
        private double deltaRenderTime;
        private double nowTime;

        /// <summary>
        /// Get the last observed UPS count
        /// </summary>
        public int CapturedUpdates { get; private set; }
        /// <summary>
        /// Get the last observed FPS count
        /// </summary>
        public int CapturedFrames { get; private set; }

        private int updates;
        private int frames;

        private int desiredMaxFPS;

        private Stopwatch stopwatch;

        public GameTimer() : this(30, 30) {}

        public GameTimer(int ups, int fps = 0) {
            if (ups < 0 || fps < 0) {
                throw new ArgumentOutOfRangeException(
                    $"GameTimer must have positive count values: (ups={ups},fps={fps})");
            }
            desiredMaxFPS = fps;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            updateTimeLimit = 1.0 / ups;
            renderTimeLimit = 1.0 / fps;
            lastTime = stopwatch.ElapsedMilliseconds / 1000.0; // elapsed seconds
            deltaUpdateTime = 0.0;
            deltaRenderTime = 0.0;
            nowTime = 0.0;
            timer = lastTime;

            frames = 0;
            updates = 0;
            CapturedFrames = 0;
            CapturedUpdates = 0;
        }

        public void MeasureTime() {
            nowTime = stopwatch.ElapsedMilliseconds / 1000.0;
            deltaUpdateTime += (nowTime - lastTime) / updateTimeLimit;
            deltaRenderTime += (nowTime - lastTime) / renderTimeLimit;
            lastTime = nowTime;
        }

        public bool ShouldUpdate() {
            var ret = deltaUpdateTime >= 1.0;
            if (ret) {
                updates++;
                deltaUpdateTime--;
            }
            return ret;
        }

        public bool ShouldRender() {
            if (desiredMaxFPS < 1) {
                return true;
            }
            var ret = deltaRenderTime >= 1.0;
            if (ret) {
                frames++;
                deltaRenderTime--;
            }
            return ret;
        }

        /// <summary>
        /// The timer will reset if 1 second has passed.
        /// This information can be used to update game logic in any way desireable.
        /// </summary>
        public bool ShouldReset() {
            var ret = (stopwatch.ElapsedMilliseconds / 1000.0) - timer > 1.0;
            if (ret) {
                timer += 1.0;
                CapturedUpdates = updates;
                CapturedFrames = frames;
                updates = 0;
                frames = 0;
            }
            return ret;
        }
    }
}