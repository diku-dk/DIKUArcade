using DIKUArcade.Timers;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.GameTimerTests {
    [TestFixture]
    public class TestingGameTimer {

        [Repeat(3)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(30)]
        [TestCase(60)]
        public void TestCapturedUpdates(int ups) {
            var timer = new GameTimer(ups);
            var updates = 0;

            while (!timer.ShouldReset()) {
                timer.MeasureTime();
                if (timer.ShouldUpdate()) {
                    updates++;
                }
            }
            // can we count the number of updates
            Assert.AreEqual(updates, timer.CapturedUpdates);

            // if e.g. target ups is 60, then sometimes reaching 59 is okay!
            Assert.LessOrEqual(timer.CapturedUpdates, ups);
            Assert.GreaterOrEqual(timer.CapturedUpdates, ups - 1);
        }

        [Repeat(3)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(30)]
        [TestCase(60)]
        public void TestCapturedFrames(int fps) {
            var timer = new GameTimer(30, fps);
            var frames = 0;

            while (!timer.ShouldReset()) {
                timer.MeasureTime();
                if (timer.ShouldRender() && fps > 0) { // fps 0 will try to render unlimited!
                    frames++;
                }
            }
            // can we count the number of updates
            Assert.AreEqual(frames, timer.CapturedFrames);

            // if e.g. target fps is 60, then sometimes reaching 59 is okay!
            Assert.LessOrEqual(timer.CapturedFrames, fps);
            Assert.GreaterOrEqual(timer.CapturedFrames, fps - 1);
        }
    }
}