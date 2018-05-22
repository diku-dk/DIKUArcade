using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using DIKUArcade.Timers;

namespace DIKUArcadeUnitTests.StaticTimerTests {
    [TestFixture]
    public class StaticTimerTest {

        /// <summary>
        /// Wait for the specified amount of milliseconds
        /// </summary>
        private void Wait(Stopwatch timer, int milliseconds) {
            timer.Restart();
            var startTime = timer.ElapsedMilliseconds;
            while (timer.ElapsedMilliseconds - startTime <= milliseconds) {}
            timer.Stop();
        }

        [Test]
        public void TestPauseTimer() {
            var testTimer = new Stopwatch();

            StaticTimer.RestartTimer();
            Wait(testTimer, 1000);
            StaticTimer.PauseTimer();
            Assert.GreaterOrEqual(StaticTimer.GetElapsedMilliseconds(), 1000, "test 1");

            Wait(testTimer, 1000);
            StaticTimer.ResumeTimer();
            Wait(testTimer, 1000);
            //Assert.AreEqual(2000, StaticTimer.GetElapsedMilliseconds(), "test 2 første");
            Assert.GreaterOrEqual(StaticTimer.GetElapsedMilliseconds(), 2000, "test 2");
        }

        [Test]
        public void RepeatedTestPauseTimer() {
            var testTimer = new Stopwatch();
            StaticTimer.RestartTimer();

            for (int i = 0; i < 10; i++) {
                StaticTimer.ResumeTimer();
                Assert.GreaterOrEqual(StaticTimer.GetElapsedMilliseconds(), 1000*i, $"test {i} (1)");
                Assert.Less(StaticTimer.GetElapsedMilliseconds(), 50+1000*i, $"test {i} (2)");
                Wait(testTimer, 1000);
                StaticTimer.PauseTimer();
            }
        }
    }
}