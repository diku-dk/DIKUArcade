using DIKUArcade.Events;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.GameEventBus
{
    [TestFixture]
    class TestEventQueue
    {
        [Test]
        public void CreateEventQueue()
        {
            GameEventQueue<GameEvent<object>> geq = new GameEventQueue<GameEvent<object>>();

            var res1 = new GameEvent<object> {
                EventType = GameEventType.ControlEvent,
                From = this,
                Message = "test data",
                StringArg1 = "param1",
                StringArg2 = "param2"
            };

            var res2 = new GameEvent<object> {
                EventType = GameEventType.ControlEvent,
                From = this,
                Message = "test data2",
                StringArg1 = "param1",
                StringArg2 = "param2"
            };

            geq.Enqueue(res1);
            geq.Enqueue(res2);

            var result1 = geq.Dequeue();
            var result2 = geq.Dequeue();

            Assert.That(result1, Is.EqualTo(res1));
            Assert.That(result2, Is.EqualTo(res2));
        }
    }
}
