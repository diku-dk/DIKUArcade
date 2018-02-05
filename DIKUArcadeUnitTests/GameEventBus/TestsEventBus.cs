using System;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.GameEventBus
{
    [TestFixture]
    public class TestsEventBus
    {
        private readonly List<GameEventType> _registeredEvents= new List<GameEventType>() {
            GameEventType.ControlEvent, GameEventType.SoundEvent, GameEventType.StatusEvent
        };
        private GameEventBus<object> _eb;
        private SimpleEventProcessor _simpleEventProcessor;
        private GameEvent<object> _eventControl;
        private GameEvent<object> _eventSound;

        /// <summary>
        /// SimpleGameProcessor is a mock-up processor for testing purposes.
        /// </summary>
        public class SimpleEventProcessor : IGameEventProcessor<object>
        {
            /// <summary>
            /// Counter for number of processes control events.
            /// </summary>
            public int EventCounterControl;
            /// <summary>
            /// Counter for number of processes sound events.
            /// </summary>
            public int EventCounterSound;

            public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
            {
                // Count events using integer fields
                if(eventType==GameEventType.ControlEvent)
                    EventCounterControl++;
                if (eventType == GameEventType.SoundEvent)
                    EventCounterSound++;
            }
        }

        /// <summary>
        /// Setup event processor mock-up and events.
        /// </summary>
        [SetUp]
        public void SetupEventBusForTests()
        {
            _eb=new GameEventBus<object>();
            _eb.InitializeEventBus(_registeredEvents);

            _simpleEventProcessor = new SimpleEventProcessor();

            _eb.Subscribe(GameEventType.ControlEvent, _simpleEventProcessor);
            _eb.Subscribe(GameEventType.SoundEvent, _simpleEventProcessor);

            _eventControl = GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.ControlEvent, this, "Test1", "test", "test");
            _eventSound = GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.SoundEvent, this, "Test2", "test", "test");
        }

        /// <summary>
        /// Generate five events and process them in parallel. Afterwards check the counts of events.
        /// </summary>
        [Test]
        public void TestEventBusSimpleCount5Test()
        {
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);

            _eb.ProcessEvents();

            Assert.That(_simpleEventProcessor.EventCounterControl == 3);
            Assert.That(_simpleEventProcessor.EventCounterSound == 2);
        }

        /// <summary>
        /// Generate three events and process them in parallel. Afterwards check the counts of events.
        /// </summary>
        [Test]
        public void TestEventBusSimpleCount3Test()
        {
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);
            _eb.RegisterEvent(_eventControl);

            _eb.ProcessEvents();

            Assert.That(_simpleEventProcessor.EventCounterControl == 2);
            Assert.That(_simpleEventProcessor.EventCounterSound == 1);
        }

        /// <summary>
        /// Generate five events and process them sequentially. Afterwards check the counts of events.
        /// </summary>
        [Test]
        public void TestEventBusSimpleCount5TestSeq()
        {
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);

            _eb.ProcessEventsSequentially();

            Assert.That(_simpleEventProcessor.EventCounterControl == 3);
            Assert.That(_simpleEventProcessor.EventCounterSound == 2);
        }

        /// <summary>
        /// Generate three events and process them sequentially. Afterwards check the counts of events.
        /// </summary>
        [Test]
        public void TestEventBusSimpleCount3TestSeq()
        {
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);
            _eb.RegisterEvent(_eventControl);

            _eb.ProcessEventsSequentially();

            Assert.That(_simpleEventProcessor.EventCounterControl == 2);
            Assert.That(_simpleEventProcessor.EventCounterSound == 1);
        }

        /// <summary>
        /// Generate numEventGroups groups of three events and process them in parallel. Afterwards check the counts of events.
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
        public void TestEventBusSimpleCountParametricTest(int numEventGroups)
        {
            for (int iter=0; iter < numEventGroups; iter++)
            {
                _eb.RegisterEvent(_eventControl);
                _eb.RegisterEvent(_eventSound);
                _eb.RegisterEvent(_eventControl);
            }

            _eb.ProcessEvents();

            Assert.That(_simpleEventProcessor.EventCounterControl == 2*numEventGroups);
            Assert.That(_simpleEventProcessor.EventCounterSound == 1*numEventGroups);
        }

        /// <summary>
        /// Generate numEventGroups groups of three events and process them sequentially. Afterwards check the counts of events.
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
        public void TestEventBusSimpleCountParametricTestSequentially(int numEventGroups)
        {
            for (int iter=0; iter < numEventGroups; iter++)
            {
                _eb.RegisterEvent(_eventControl);
                _eb.RegisterEvent(_eventSound);
                _eb.RegisterEvent(_eventControl);
            }

            _eb.ProcessEventsSequentially();

            Assert.That(_simpleEventProcessor.EventCounterControl == 2*numEventGroups);
            Assert.That(_simpleEventProcessor.EventCounterSound == 1*numEventGroups);
        }

        /// <summary>
        /// Generate a fixed number of listeners subscribing to a game event bus and process three events in parallel. Check afterwards that all event have been processed.
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
        public void TestConcurrentListeners(int numListeners)
        {
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);
            _eb.RegisterEvent(_eventControl);

            List<SimpleEventProcessor> listOfProcessors= new List<SimpleEventProcessor>();
            for (int iter = 0; iter < numListeners; iter++)
            {
                var processor= new SimpleEventProcessor();
                listOfProcessors.Add(processor);
                _eb.Subscribe(GameEventType.ControlEvent, processor);
                _eb.Subscribe(GameEventType.SoundEvent, processor);
            }

            _eb.ProcessEvents();

            Assert.That(_simpleEventProcessor.EventCounterControl == 2);
            Assert.That(_simpleEventProcessor.EventCounterSound == 1);
            foreach (var processor in listOfProcessors)
            {
                Assert.That(processor.EventCounterControl == 2);
                Assert.That(processor.EventCounterSound == 1);
            }
        }

        /// <summary>
        /// Generate a fixed number of listeners subscribing to a game event bus and process three events sequentially. Check afterwards that all event have been processed.
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
        public void TestConcurrentListenersSequentially(int numListeners)
        {
            _eb.RegisterEvent(_eventControl);
            _eb.RegisterEvent(_eventSound);
            _eb.RegisterEvent(_eventControl);

            List<SimpleEventProcessor> listOfProcessors= new List<SimpleEventProcessor>();
            for (int iter = 0; iter < numListeners; iter++)
            {
                var processor= new SimpleEventProcessor();
                listOfProcessors.Add(processor);
                _eb.Subscribe(GameEventType.ControlEvent, processor);
                _eb.Subscribe(GameEventType.SoundEvent, processor);
            }

            _eb.ProcessEventsSequentially();

            Assert.That(_simpleEventProcessor.EventCounterControl == 2);
            Assert.That(_simpleEventProcessor.EventCounterSound == 1);
            foreach (var processor in listOfProcessors)
            {
                Assert.That(processor.EventCounterControl == 2);
                Assert.That(processor.EventCounterSound == 1);
            }
        }

        /// <summary>
        /// Checks that exceptions are raised if the processor reference is null. Absence of undesired behavior.
        /// </summary>
        [Test]
        public void TestSubscribeGameEventProcessorArgumentNotNullException()
        {
            Assert.Throws<ArgumentNullException>(delegate
                {
                    _eb.Subscribe(GameEventType.ControlEvent, default(IGameEventProcessor<object>));
                });
        }

        /// <summary>
        /// Checks that exceptions are raised if the processor reference is null. Absence of undesired behavior.
        /// </summary>
        [Test]
        public void TestUnsubscribeGameEventProcessorArgumentNotNullException()
        {
            Assert.Throws<ArgumentNullException>(delegate
            {
                _eb.Subscribe(GameEventType.ControlEvent, default(IGameEventProcessor<object>));
            });
        }
    }
}