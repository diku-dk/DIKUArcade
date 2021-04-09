using System.Collections.Generic;
using DIKUArcade.Events;
using NUnit.Framework;

namespace DIKUArcadeUnitTests
{
    class TestGameEventSender
    {
        public void SendEvent(GameEventBus eventBus, IGameEventProcessor to, int intValue)
        {
            this.Value = intValue;
            var e = new GameEvent {
                EventType = GameEventType.GraphicsEvent,
                From = this,
                To = to,
                IntArg1 = intValue
            };
            eventBus.RegisterEvent(e);
        }

        public int Value { get; private set; }
    }

    class TestGameEventReceiver : IGameEventProcessor
    {
        public int Value { get; private set; }

        public void ProcessEvent(GameEvent gameEvent)
        {
            Value = gameEvent.IntArg1;
        }
    }

    class TestGameEventReceiverGetSender : IGameEventProcessor
    {
        public int Value { get; private set; }

        public void ProcessEvent(GameEvent gameEvent)
        {
            var sender = gameEvent.From as TestGameEventSender;
            Value = sender?.Value ?? -1;
        }
    }

    [TestFixture]
    public class TestGameEvent
    {
        private GameEventBus _eventBus;

        private TestGameEventSender _sender;
        private TestGameEventReceiver _receiver1;
        private TestGameEventReceiver _receiver2;
        private TestGameEventReceiverGetSender _receiverGetSender;

        public TestGameEvent() {
            _sender = new TestGameEventSender();
            _receiver1 = new TestGameEventReceiver();
            _receiver2 = new TestGameEventReceiver();
            _receiverGetSender = new TestGameEventReceiverGetSender();

            _eventBus = new GameEventBus();
            _eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.GraphicsEvent }); // arbitrary event type
            _eventBus.Subscribe(GameEventType.GraphicsEvent, _receiver1);
            _eventBus.Subscribe(GameEventType.GraphicsEvent, _receiver2);
        }

        [Test]
        public void TestSenderReceiver()
        {
            _sender.SendEvent(_eventBus, _receiver1, 1);
            _sender.SendEvent(_eventBus, _receiver2, 2);
            _eventBus.ProcessEvents();

            Assert.AreEqual(1, _receiver1.Value);
            Assert.AreEqual(2, _receiver2.Value);
        }

        [Test]
        public void TestGetSender()
        {
            _sender.SendEvent(_eventBus, _receiverGetSender, 55);
            _eventBus.ProcessEvents();
            Assert.AreEqual(55, _receiverGetSender.Value);
        }
    }
}
