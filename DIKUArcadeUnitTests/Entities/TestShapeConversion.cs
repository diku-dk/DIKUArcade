namespace DIKUArcadeUnitTests.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Numerics;
using NUnit.Framework;

[TestFixture]
public class TestShapeConversion {
    private DynamicShape shape;
    private Image image;

    public TestShapeConversion() {
        var pos = new Vector2(5.0f, 3.0f);
        var ext = new Vector2(10.0f, 6.0f);
        var dir = new Vector2(1.0f, 1.0f);
        shape = new DynamicShape(pos, ext, dir);
        image = null;
    }

    [Test]
    public void TestDynamicFromShape() {
        var entity = new Entity(shape, image);

        entity.Shape.Move();
        Assert.AreEqual(entity.Shape.Position.X, 6.0f);
        Assert.AreEqual(entity.Shape.Position.Y, 4.0f);

        entity.Shape.AsDynamicShape().Velocity.X = -1.0f;
        entity.Shape.AsDynamicShape().Velocity.Y = -1.0f;
        entity.Shape.Move();
        Assert.AreEqual(entity.Shape.Position.X, 5.0f);
        Assert.AreEqual(entity.Shape.Position.Y, 3.0f);
    }
}