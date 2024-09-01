namespace DIKUArcadeUnitTests.GameEventBusTests;

using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.Threading;
using NUnit.Framework;

[TestFixture]
public class TestsTimedEventBus {

    private GameEventBus eventBus;
    private SomeGameEvent SomeGameEvent;
    private int SomeGameEventCount;

    private void SomeEventListener(SomeGameEvent gameEvent) {
        SomeGameEvent = gameEvent;
        SomeGameEventCount++;
    }

    /// <summary>
    /// Reset event bus and event action before calling each test method.
    /// </summary>
    [SetUp]
    public void Setup() {
        eventBus = new GameEventBus();

        SomeGameEvent = new SomeGameEvent(1729);

        SomeGameEventCount = 0;
    }

    [Test]
    public void TestRegisterTimedEvent() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewMilliseconds(500));
        eventBus.ProcessEvents();
        Assert.AreEqual(0, SomeGameEventCount);

        Thread.Sleep(550);
        eventBus.ProcessEvents();
        Assert.AreEqual(1, SomeGameEventCount);
    }

    [Test]
    public void TestResetTimedEvent() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        // event for reset testing
        var idx = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewMilliseconds(1000));

        // event which should stay in event bus for a very long time,
        // and not be disturbed by resetting the other event multiple times.
        var idy = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewSeconds(30.0));

        // time has not passed yet, so event should not have been processed
        Thread.Sleep(100);
        eventBus.ProcessEvents();
        Assert.AreEqual(0, SomeGameEventCount);

        // reset event timer
        bool reset = eventBus.ResetTimedEvent(idx, TimePeriod.NewMilliseconds(100));
        Assert.IsTrue(reset);

        Thread.Sleep(150);
        eventBus.ProcessEvents();
        Assert.AreEqual(1, SomeGameEventCount);

        // try to reset event timer again,
        // should fail because event has been processed
        bool resetFail = eventBus.ResetTimedEvent(idx, TimePeriod.NewMilliseconds(100));
        Assert.IsFalse(resetFail);

        // because event was not contained, we could not reset it.
        // Thus, event should not be contained, and if we call ProcessEvents
        // it should NOT be processed again - verified by the EventCounter.
        Thread.Sleep(150);
        eventBus.ProcessEvents();
        Assert.AreEqual(1, SomeGameEventCount);

        // test that resetting did not disturb the other event
        Assert.IsTrue(eventBus.HasTimedEvent(idy));
    }

    [Test]
    public void TestHasTimedEvent() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        var idx = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewMilliseconds(100));
        var idy = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewMilliseconds(2000));

        // sleep 150ms.
        // This should timeout e1 but not e2
        Thread.Sleep(150);

        eventBus.ProcessEvents();
        Assert.AreEqual(1, SomeGameEventCount);
        Assert.IsFalse(eventBus.HasTimedEvent(idx));
        Assert.IsTrue(eventBus.HasTimedEvent(idy));
    }

    [Test]
    public void TestCancelTimedEvent() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        // events for cancel testing
        var idx = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewMilliseconds(100));
        var idy = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewMilliseconds(2000));

        // event which should stay in event bus for a very long time,
        // and not be disturbed by cancelling the other event multiple times.
        var idz = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewSeconds(30.0));

        // sleep 150ms.
        // This should timeout event 1 but not event 2
        Thread.Sleep(150);

        // cancel event 1 which was timed out and ready for processing
        eventBus.CancelTimedEvent(idx);

        // cancel event 2 which is not timed out
        eventBus.CancelTimedEvent(idy);

        // check that both events were removed
        Assert.IsFalse(eventBus.HasTimedEvent(idx));
        Assert.IsFalse(eventBus.HasTimedEvent(idy));

        // now check that neither event will be processed
        eventBus.ProcessEvents();
        Assert.AreEqual(0, SomeGameEventCount);

        // test that cancelling did not disturb the other event
        Assert.IsTrue(eventBus.HasTimedEvent(idz));
    }

    [Test]
    public void TestAddOrResetTimedEvent() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);

        // event for reset testing
        var idx = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewMilliseconds(100));

        // event which should stay in event bus for a very long time,
        // and not be disturbed by resetting the other event multiple times.
        var idy = eventBus.RegisterTimedEvent(SomeGameEvent, TimePeriod.NewSeconds(30.0));

        // time out the event and process
        Thread.Sleep(150);
        eventBus.ProcessEvents();
        Assert.AreEqual(1, SomeGameEventCount);

        // now, add the event again
        eventBus.AddOrResetTimedEvent(SomeGameEvent, idx, TimePeriod.NewMilliseconds(100));

        // time out the event and process again
        Thread.Sleep(150);
        eventBus.ProcessEvents();
        Assert.AreEqual(2, SomeGameEventCount);

        // add the event one more time,
        // but this time with a longer time period
        eventBus.AddOrResetTimedEvent(SomeGameEvent, idx, TimePeriod.NewMilliseconds(2000));

        // now reset it to a short time period
        eventBus.AddOrResetTimedEvent(SomeGameEvent, idx, TimePeriod.NewMilliseconds(100));

        // time out the event and process again,
        // and test that the reset worked
        Thread.Sleep(150);
        eventBus.ProcessEvents();
        Assert.AreEqual(3, SomeGameEventCount);

        // test that resetting did not disturb the other event
        Assert.IsTrue(eventBus.HasTimedEvent(idy));
    }
}
