using System;

namespace DIKUArcade.Timers {
    public class GameTimer {
        private double lastTime;
        private double timer;
        private double timeLimit;
        private double deltaTime;
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

        public GameTimer() : this(30, 30) {}

        public GameTimer(int ups, int fps = 0) {
            if (ups < 0 || fps < 0) {
                throw new ArgumentOutOfRangeException(
                    $"GameTimer must have positive count values: (ups={ups},fps={fps})");
            }
            desiredMaxFPS = fps;

            timeLimit = 1.0 / ups;
            lastTime = StaticTimer.GetElapsedSeconds();
            deltaTime = 0.0;
            nowTime = 0.0;
            timer = lastTime;

            frames = 0;
            updates = 0;
            CapturedFrames = 0;
            CapturedUpdates = 0;
        }

        public void MeasureTime() {
            nowTime = StaticTimer.GetElapsedSeconds();
            deltaTime += (nowTime - lastTime) / timeLimit;
            lastTime = nowTime;
        }

        public bool ShouldUpdate() {
            var ret = deltaTime >= 1.0;
            if (ret) {
                updates++;
                deltaTime--;
            }
            return ret;
        }

        public bool ShouldRender() {
            if (desiredMaxFPS > 0 && frames >= desiredMaxFPS) {
                return false;
            }
            frames++;
            return true;
        }

        /// <summary>
        /// The timer will reset if 1 second has passed.
        /// This information can be used to update game logic in any way desireable.
        /// </summary>
        public bool ShouldReset() {
            var ret = StaticTimer.GetElapsedSeconds() - timer > 1.0;
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