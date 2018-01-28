using System.Collections.Generic;
using System.Diagnostics;
using DIKUArcade.EventBus;
using NUnit.Framework;

namespace GameEventBusTestProject
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

        [OneTimeSetUp]
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

    }
}