using DIKUArcade.EventBus;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.GameEventBus
{
    [TestFixture]
    class TestGameEventFactory
    {
        [Test]
        public void TestCreationOfEventForAllProcessorsInstanceTest()
        {
            var res= GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.ControlEvent, this, "test data",
                "param1", "param2");
            Assert.IsInstanceOf(typeof(GameEvent<object>), res);
        }

        [Test]
        public void TestCreationOfEventForAllProcessorsEventTypeTest()
        {
            var res = GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.ControlEvent, this, "test data",
                "param1", "param2");
            Assert.That(res.EventType == GameEventType.ControlEvent);
        }

        [Test]
        public void TestCreationOfEventForAllProcessorsFromTest()
        {
            var res = GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.ControlEvent, this, "test data",
                "param1", "param2");
            Assert.That(res.From == this);
        }

        [Test]
        public void TestCreationOfEventForAllProcessorsToTest()
        {
            var res = GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.ControlEvent, this, "test data",
                "param1", "param2");
            Assert.That(res.To == null);
        }

        [Test]
        public void TestCreationOfEventSpecificProcessorsToTest()
        {
            var res = GameEventFactory<object>.CreateGameEventForSpecificProcessor(GameEventType.ControlEvent, this, this, "test data",
                "param1", "param2");
            Assert.That(res.To == this);
        }
    }
}
