namespace DIKUArcade.Timers {
    public class GameTimer {
        // TODO: A better API for this class must be devised!
        private int fps;
        private int ups;

        private double lastTime;
        private double timer;
        private double timeLimit;
        private double deltaTime;
        private double nowTime;

        /// <summary>
        /// Count how many updates we can do per second
        /// </summary>
        public int CapturedFrames { get; private set; }
        /// <summary>
        /// How many times `Update()` gets called per second
        /// </summary>
        public int CapturedUpdates { get; private set; }

        private int frames;
        private int updates;

        public GameTimer(int fps) : this(fps, fps) {}
        public GameTimer() : this(30, 30) {}

        public GameTimer(int fps, int ups) {
            this.fps = fps;
            this.ups = ups;

            timeLimit = 1.0 / (double) fps;
            lastTime = StaticTimer.GetCurrentTimeFrame();
            timer = lastTime;
            deltaTime = 0.0;
            nowTime = 0.0;

            frames = 0;
            updates = 0;
            CapturedFrames = 0;
            CapturedUpdates = 0;
        }

        public void MeasureTime() {
            nowTime = StaticTimer.GetCurrentTimeFrame();
            deltaTime += (nowTime - lastTime) / timeLimit;
            lastTime = nowTime;
            frames++;
        }

        public bool ShouldUpdate() {
            var ret = deltaTime >= 1.0;
            if (ret) {
                updates++;
                deltaTime--;
            }
            return ret;
        }

        // TODO: should this method perform calculations based on desired FPS ?
        public bool ShouldRender() {
            return true;
        }

        public bool ShouldReset() {
            return StaticTimer.GetCurrentTimeFrame() - timer > 1.0;
        }

        public void ResetTime() {
            timer++;
            CapturedUpdates = updates;
            CapturedFrames = frames;
            updates = 0;
            frames = 0;
        }
    }
}