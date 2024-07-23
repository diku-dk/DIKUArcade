namespace DIKUArcadeUnitTests.GameEventBusTests;

using System;
using System.Collections.Generic;
using DIKUArcade.Events;
using NUnit.Framework;


[TestFixture]
public class TestsEventBus {

    private GameEventBus eventBus;
    private SomeGameEvent SomeGameEvent;
    private OtherGameEvent OtherGameEvent;
    private int SomeGameEventCount;
    private int OtherGameEventCount;

    private void SomeEventListener(SomeGameEvent gameEvent) {
        SomeGameEvent = gameEvent;
        SomeGameEventCount++;
    }

    private void OtherEventListener(OtherGameEvent gameEvent) {
        OtherGameEvent = gameEvent;
        OtherGameEventCount++;
    }

    /// <summary>
    /// Reset event bus and event action before calling each test method.
    /// </summary>
    [SetUp]
    public void Setup() {
        eventBus = new GameEventBus();

        SomeGameEvent = new SomeGameEvent(1729);
        OtherGameEvent = new OtherGameEvent(5040);

        SomeGameEventCount = 0;
        OtherGameEventCount = 0;
    }

    /// <summary>
    /// Generate five events and process them in parallel. Afterwards check the counts of events.
    /// </summary>
    public void TestEventBusSimpleCount5Test() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        eventBus.Subscribe<OtherGameEvent>(OtherEventListener);

        eventBus.RegisterEvent(SomeGameEvent);
        eventBus.RegisterEvent(OtherGameEvent);
        eventBus.RegisterEvent(SomeGameEvent);
        eventBus.RegisterEvent(SomeGameEvent);
        eventBus.RegisterEvent(OtherGameEvent);

        eventBus.ProcessEvents();

        Assert.That(SomeGameEventCount == 3);
        Assert.That(OtherGameEventCount == 2);
    }

    /// <summary>
    /// Generate three events and process them in parallel. Afterwards check the counts of events.
    /// </summary>
    public void TestEventBusSimpleCount3Test() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        eventBus.Subscribe<OtherGameEvent>(OtherEventListener);

        eventBus.RegisterEvent(SomeGameEvent);
        eventBus.RegisterEvent(OtherGameEvent);
        eventBus.RegisterEvent(SomeGameEvent);
        
        eventBus.ProcessEvents();

        Assert.That(SomeGameEventCount == 2);
        Assert.That(OtherGameEventCount == 1);
    }

    /// <summary>
    /// Generate numEventGroups groups of three events and process them in parallel. Afterwards 
    /// check the counts of events.
    /// </summary>
    /// <param name="numEventGroups">Number of event groups used for the test case.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(8)]
    [TestCase(16)]
    [TestCase(32)]
    [TestCase(47)]
    [TestCase(64)]
    [TestCase(128)]
    [TestCase(199)]
    [TestCase(1024)]
    [TestCase(2048)]
    public void TestEventBusSimpleCountParametricTest(int numEventGroups) {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        eventBus.Subscribe<OtherGameEvent>(OtherEventListener);

        for (int iter = 0; iter < numEventGroups; iter++) {
            eventBus.RegisterEvent(SomeGameEvent);
            eventBus.RegisterEvent(OtherGameEvent);
            eventBus.RegisterEvent(SomeGameEvent);
        }

        eventBus.ProcessEvents();

        Assert.That(SomeGameEventCount == 2*numEventGroups);
        Assert.That(OtherGameEventCount == 1*numEventGroups);
    }

    /// <summary>
    /// Generate a fixed number of listeners subscribing to a game event bus and process three 
    /// events sequentially. Check afterwards that all event have been processed.
    /// </summary>
    /// <param name="numListeners">Number of listeners.</param>
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(6)]
    [TestCase(8)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(256)]
    public void TestConcurrentListenersSequentially(int numListeners) {

        eventBus.RegisterEvent(SomeGameEvent);
        eventBus.RegisterEvent(OtherGameEvent);
        eventBus.RegisterEvent(SomeGameEvent);

        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        eventBus.Subscribe<OtherGameEvent>(OtherEventListener);

        var listeners = new List<Listener>();
        for (int iter = 0; iter < numListeners; iter++) {
            var listener = new Listener();
            listeners.Add(listener);
            eventBus.Subscribe<SomeGameEvent>(listener.SomeListener);
            eventBus.Subscribe<OtherGameEvent>(listener.OtherListener);
        }

        eventBus.ProcessEvents();

        Assert.That(SomeGameEventCount == 2);
        Assert.That(OtherGameEventCount == 1);

        foreach (var listener in listeners) {
            Assert.That(listener.SomeCount == 2);
            Assert.That(listener.OtherCount == 1);
        }
    }

    /// <summary>
    /// Checks that exceptions are raised if the action reference is null. Absence of undesired 
    /// behavior.
    /// </summary>
    [Test]
    public void TestSubscribeActionArgumentNotNullException() {
        Assert.Throws<ArgumentNullException>(() => {
            eventBus.Subscribe(default(Action<int>));
        });
    }

    /// <summary>
    /// Checks that exceptions are raised if the action reference is null. Absence of undesired 
    /// behavior.
    /// </summary>
    [Test]
    public void TestUnsubscribeActionArgumentNotNullException() {
        Assert.Throws<ArgumentNullException>(() => {
            eventBus.Unsubscribe(default(Action<int>));
        });
    }

    /// <summary>
    /// Checks that exceptions are raised if the action is not subscribed.
    /// </summary>
    [Test]
    public void TestUnsubscribeNonExistentAction() {

        Action<SomeGameEvent> dummyListener = _ => { };

        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);

        Assert.Throws<ArgumentException>(() => {
            eventBus.Unsubscribe<SomeGameEvent>(dummyListener);
        });
        
        eventBus.RegisterEvent(SomeGameEvent);

        eventBus.ProcessEvents();

        Assert.AreEqual(1, SomeGameEventCount);
    }

    /// <summary>
    /// Checks that exceptions are raised if the key does not exist.
    /// </summary>
    [Test]
    public void TestUnsubscribeNonExistentKey() {

        Assert.Throws<ArgumentException>(() => {
            eventBus.Unsubscribe<SomeGameEvent>(SomeEventListener);
        });
    }

    /// <summary>
    /// Checks that unsubscribg does not effect existing subscribers.
    /// </summary>
    [Test]
    public void TestUnsubscribe() {

        var dummyCount = 0;

        Action<SomeGameEvent> dummyListener = _ => dummyCount++;

        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        eventBus.Subscribe<SomeGameEvent>(dummyListener);
        eventBus.Unsubscribe<SomeGameEvent>(dummyListener);
        
        eventBus.RegisterEvent(SomeGameEvent);

        eventBus.ProcessEvents();

        Assert.AreEqual(1, SomeGameEventCount);
        Assert.AreEqual(0, dummyCount);
    }

    /// <summary>
    /// Checks if the EventBus can be flused.
    /// </summary>
    [Test]
    public void TestFlush() {
        eventBus.Subscribe<SomeGameEvent>(SomeEventListener);
        eventBus.RegisterEvent(SomeGameEvent);
        eventBus.Flush();
        eventBus.ProcessEvents();
        Assert.AreEqual(0, SomeGameEventCount);
    }
}
