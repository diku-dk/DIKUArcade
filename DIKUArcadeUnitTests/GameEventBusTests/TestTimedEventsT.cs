using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Events.Generic;
using DIKUArcade.Timers;
using System.Threading;

namespace DIKUArcadeUnitTests.GameEventBusTests
{
    public enum TestTimedEventT {
        TimedEvent,
        StatusEvent,
        MovementEvent
    };

    [TestFixture]
    public class TestTimedEventsT
    {
        private class Helper : IGameEventProcessor<TestTimedEventT>
        {
            public void ProcessEvent(GameEvent<TestTimedEventT> gameEvent)
            {
                EventCounter++;
            }

            public int EventCounter { get; private set; } = 0;

            public void ResetEventCounter() => EventCounter = 0;
        }


        private GameEventBus<TestTimedEventT> _eventBus;
        private Helper _helper;

        public TestTimedEventsT()
        {
            _helper = new Helper();

            _eventBus = new GameEventBus<TestTimedEventT>();
            _eventBus.InitializeEventBus(new List<TestTimedEventT> {
                TestTimedEventT.TimedEvent,
                TestTimedEventT.StatusEvent,
                TestTimedEventT.MovementEvent
            });
            _eventBus.Subscribe(TestTimedEventT.TimedEvent, _helper);
        }

        [SetUp]
        public void Setup()
        {
            _helper.ResetEventCounter();
            _eventBus.Flush();
        }

        [Test]
        public void TestRegisterTimedEvent()
        {
            var e = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.TimedEvent,
                From = this,
                To = _helper
            };
            _eventBus.RegisterTimedEvent(e, TimePeriod.NewMilliseconds(500));

            _eventBus.ProcessEvents();
            Assert.AreEqual(0, _helper.EventCounter);

            Thread.Sleep(550);
            _eventBus.ProcessEvents();
            Assert.AreEqual(1, _helper.EventCounter);
        }

        
        [Test]
        public void TestResetTimedEvent()
        {
            // event for reset testing
            var e = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.TimedEvent,
                From = this,
                To = _helper,
                Id = 587
            };
            _eventBus.RegisterTimedEvent(e, TimePeriod.NewMilliseconds(1000));

            // event which should stay in event bus for a very long time,
            // and not be disturbed by resetting the other event multiple times.
            var longWaitEvent = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.TimedEvent,
                From = this,
                To = _helper,
                Id = 315
            };
            _eventBus.RegisterTimedEvent(longWaitEvent, TimePeriod.NewSeconds(30.0));

            // time has not passed yet, so event should not have been processed
            Thread.Sleep(100);
            _eventBus.ProcessEvents();
            Assert.AreEqual(0, _helper.EventCounter);

            // reset event timer
            bool reset = _eventBus.ResetTimedEvent(587, TimePeriod.NewMilliseconds(100));
            Assert.IsTrue(reset);

            Thread.Sleep(150);
            _eventBus.ProcessEvents();
            Assert.AreEqual(1, _helper.EventCounter);

            // try to reset event timer again,
            // should fail because event has been processed
            bool resetFail = _eventBus.ResetTimedEvent(587, TimePeriod.NewMilliseconds(100));
            Assert.IsFalse(resetFail);

            // because event was not contained, we could not reset it.
            // Thus, event should not be contained, and if we call ProcessEvents
            // it should NOT be processed again - verified by the EventCounter.
            Thread.Sleep(150);
            _eventBus.ProcessEvents();
            Assert.AreEqual(1, _helper.EventCounter);

            // test that resetting did not disturb the other event
            Assert.IsTrue(_eventBus.HasTimedEvent(longWaitEvent.Id));
        }

        
        [Test]
        public void TestHasTimedEvent()
        {
            var e1 = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.StatusEvent,
                From = this,
                To = _helper,
                Id = 42
            };
            var e2 = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.MovementEvent,
                From = this,
                To = _helper,
                Id = 633
            };

            _eventBus.RegisterTimedEvent(e1, TimePeriod.NewMilliseconds(100));
            _eventBus.RegisterTimedEvent(e2, TimePeriod.NewMilliseconds(2000));

            // sleep 150ms.
            // This should timeout e1 but not e2
            Thread.Sleep(150);

            _eventBus.ProcessEvents();
            Assert.AreEqual(1, _helper.EventCounter);
            Assert.IsFalse(_eventBus.HasTimedEvent(e1.Id));
            Assert.IsTrue(_eventBus.HasTimedEvent(e2.Id));
        }


        [Test]
        public void TestCancelTimedEvent()
        {
            // events for cancel testing
            var e1 = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.StatusEvent,
                From = this,
                To = _helper,
                Id = 42
            };
            var e2 = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.MovementEvent,
                From = this,
                To = _helper,
                Id = 633
            };
            _eventBus.RegisterTimedEvent(e1, TimePeriod.NewMilliseconds(100));
            _eventBus.RegisterTimedEvent(e2, TimePeriod.NewMilliseconds(2000));

            // event which should stay in event bus for a very long time,
            // and not be disturbed by cancelling the other event multiple times.
            var longWaitEvent = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.TimedEvent,
                From = this,
                To = _helper,
                Id = 316
            };
            _eventBus.RegisterTimedEvent(longWaitEvent, TimePeriod.NewSeconds(30.0));

            // sleep 150ms.
            // This should timeout e1 but not e2
            Thread.Sleep(150);

            // cancel e1 which was timed out and ready for processing
            _eventBus.CancelTimedEvent(e1.Id);

            // cancel e2 which is not timed out
            _eventBus.CancelTimedEvent(e2.Id);

            // check that both events were removed
            Assert.IsFalse(_eventBus.HasTimedEvent(e1.Id));
            Assert.IsFalse(_eventBus.HasTimedEvent(e2.Id));

            // now check that neither event will be processed
            _eventBus.ProcessEvents();
            Assert.AreEqual(0, _helper.EventCounter);

            // test that cancelling did not disturb the other event
            Assert.IsTrue(_eventBus.HasTimedEvent(longWaitEvent.Id));
        }


        [Test]
        public void TestAddOrResetTimedEvent()
        {
            // event for reset testing
            var e = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.TimedEvent,
                From = this,
                To = _helper,
                Id = 587
            };
            _eventBus.RegisterTimedEvent(e, TimePeriod.NewMilliseconds(100));

            // event which should stay in event bus for a very long time,
            // and not be disturbed by resetting the other event multiple times.
            var longWaitEvent = new GameEvent<TestTimedEventT> {
                EventType = TestTimedEventT.TimedEvent,
                From = this,
                To = _helper,
                Id = 317
            };
            _eventBus.RegisterTimedEvent(longWaitEvent, TimePeriod.NewSeconds(30.0));

            // time out the event and process
            Thread.Sleep(150);
            _eventBus.ProcessEvents();
            Assert.AreEqual(1, _helper.EventCounter);

            // now, add the event again
            _eventBus.AddOrResetTimedEvent(e, TimePeriod.NewMilliseconds(100));

            // time out the event and process again
            Thread.Sleep(150);
            _eventBus.ProcessEvents();
            Assert.AreEqual(2, _helper.EventCounter);

            // add the event one more time,
            // but this time with a longer time period
            _eventBus.AddOrResetTimedEvent(e, TimePeriod.NewMilliseconds(2000));

            // now reset it to a short time period
            _eventBus.AddOrResetTimedEvent(e, TimePeriod.NewMilliseconds(100));

            // time out the event and process again,
            // and test that the reset worked
            Thread.Sleep(150);
            _eventBus.ProcessEvents();
            Assert.AreEqual(3, _helper.EventCounter);

            // test that resetting did not disturb the other event
            Assert.IsTrue(_eventBus.HasTimedEvent(longWaitEvent.Id));
        }
    }
}
