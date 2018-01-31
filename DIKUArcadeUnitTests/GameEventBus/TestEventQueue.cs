using DIKUArcade.EventBus;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.GameEventBus
{
    [TestFixture]
    class TestEventQueue
    {
        [Test]
        public void CreateEventQueue()
        {
            GameEventQueue<GameEvent<object>> geq= new GameEventQueue<GameEvent<object>>();

            var res1 = GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.ControlEvent, this, "test data",
                "param1", "param2");
            var res2 = GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.ControlEvent, this, "test data2",
                "param1", "param2");

            geq.Enqueue(res1);
            geq.Enqueue(res2);

            var result1 = geq.Dequeue();
            var result2 = geq.Dequeue();

            Assert.That(result1, Is.EqualTo(res1));
            Assert.That(result2, Is.EqualTo(res2));
        }
    }
}
