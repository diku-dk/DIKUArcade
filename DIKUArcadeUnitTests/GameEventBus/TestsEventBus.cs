using System;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using NUnit.Framework;

namespace GameEventBusTestProject.GameEventBus
{
    [TestFixture]
    public class TestsEventBus
    {
        private readonly List<GameEventType> _registeredEvents= new List<GameEventType>() { GameEventType.ControlEvent, GameEventType.SoundEvent, GameEventType.StatusEvent};
        private GameEventBus<object> _eb;
        private SimpleEventProcessor _simpleEventProcessor;
        private GameEvent<object> _eventControl;
        private GameEvent<object> _eventSound;
        
        public class SimpleEventProcessor : IGameEventProcessor<object>
        {
            public int EventCounterControl;
            public int EventCounterSound;
            public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
            {
                if(eventType==GameEventType.ControlEvent)
                    EventCounterControl++;
                if (eventType == GameEventType.SoundEvent)
                    EventCounterSound++;
            }
        }

        [SetUp]
        public void SetupEventBusForTests()
        {
            _eb=new GameEventBus<object>();
            _eb.InitializeEventBus(_registeredEvents);

            _simpleEventProcessor = new SimpleEventProcessor();

            _eb.Subscribe(GameEventType.ControlEvent, _simpleEventProcessor);
            _eb.Subscribe(GameEventType.SoundEvent, _simpleEventProcessor);

            _eventControl = GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.ControlEvent, this, "Test1", "test",
                "test");
            _eventSound = GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.SoundEvent, this, "Test2", "test",
                "test");
        }

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

        [Test]
        public void TestSubscribeGameEventProcessorArgumentNotNullException()
        {
            Assert.Throws<ArgumentNullException>(delegate
                {
                    _eb.Subscribe(GameEventType.ControlEvent, default(IGameEventProcessor<object>));
                });
        }
        
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