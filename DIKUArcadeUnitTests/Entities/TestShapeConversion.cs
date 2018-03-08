using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using NUnit.Framework;

namespace DIKUArcadeUnitTests.Entities {
    [TestFixture]
    public class TestShapeConversion {
        private DynamicShape shape;
        private Image image;

        public TestShapeConversion() {
            var pos = new Vec2F(5.0f, 3.0f);
            var ext = new Vec2F(10.0f, 6.0f);
            var dir = new Vec2F(1.0f, 1.0f);
            shape = new DynamicShape(pos, ext, dir);
            image = null;
        }

        [Test]
        public void TestDynamicFromShape() {
            var entity = new Entity(shape, image);

            entity.Shape.Move();
            Assert.AreEqual(entity.Shape.Position.X, 6.0f);
            Assert.AreEqual(entity.Shape.Position.Y, 4.0f);

            entity.Shape.AsDynamicShape().Direction.X = -1.0f;
            entity.Shape.AsDynamicShape().Direction.Y = -1.0f;
            entity.Shape.Move();
            Assert.AreEqual(entity.Shape.Position.X, 5.0f);
            Assert.AreEqual(entity.Shape.Position.Y, 3.0f);
        }
    }
}