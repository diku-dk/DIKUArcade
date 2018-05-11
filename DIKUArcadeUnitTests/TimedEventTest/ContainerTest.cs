using System.Collections.Generic;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.TimedEventTest {
    [TestFixture]
    public class ContainerTest {
        /// <summary>
        /// Only use this class for mockup event processor
        /// </summary>
        internal class MockupEventProcessor : IGameEventProcessor<object> {
            public int ObservedEvents { get; private set; }
            public MockupEventProcessor() { }

            public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
                if (eventType == GameEventType.TimedEvent) {
                    ObservedEvents++;
                }
            }

            public void ResetEventCount() {
                ObservedEvents = 0;
            }
        }

        private GameEventBus<object> bus;
        private TimedEventContainer container;
        private MockupEventProcessor processor;

        public ContainerTest() {
            bus = new GameEventBus<object>();
            bus.InitializeEventBus(new List<GameEventType>() {GameEventType.TimedEvent});
            container = new TimedEventContainer(5);
            container.AttachEventBus(bus);
            processor = new MockupEventProcessor();
            bus.Subscribe(GameEventType.TimedEvent, processor);
        }

        [SetUp()]
        public void Initialize() {
            processor.ResetEventCount();
            container.ResetContainer();
        }

        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(200)]
        [TestCase(500)]
        [TestCase(1000)]
        public void TestSingleEvent(int timeSpan) {
            container.AddTimedEvent(TimeSpanType.Milliseconds, timeSpan, "msg", "par1", "par2");

            // elapse timeSpan for events to expire
            var startTime = StaticTimer.GetElapsedMilliseconds();
            var nowTime = 0.0;
            // save some space, because system timers are never 100% precise
            while((nowTime = StaticTimer.GetElapsedMilliseconds()) - startTime < timeSpan+10) {}

            container.ProcessTimedEvents();
            bus.ProcessEventsSequentially(); // events must be processed on the main thread!
            Assert.AreEqual(1, processor.ObservedEvents);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void TestMultipleEvents(int numEvents) {
            for (int i = 0; i < numEvents; i++) {
                container.AddTimedEvent(TimeSpanType.Milliseconds, 100, "msg", "par1", "par2");
            }

            // elapse timeSpan for events to expire
            var startTime = StaticTimer.GetElapsedMilliseconds();
            var nowTime = 0.0;
            // save some space, because system timers are never 100% precise
            while((nowTime = StaticTimer.GetElapsedMilliseconds()) - startTime < 110) {}

            container.ProcessTimedEvents();
            bus.ProcessEventsSequentially(); // events must be processed on the main thread!
            Assert.AreEqual(numEvents, processor.ObservedEvents);
        }
    }
}