using DIKUArcade.Math;
using NUnit.Framework;

namespace DIKUArcadeUnitTests {
    [TestFixture]
    public class TestOpenGLContext {

        [Test]
        public void TestOpenGLContextTextObject() {
            DIKUArcade.Window.CreateOpenGLContext();
            var text = new DIKUArcade.Graphics.Text("test", new Vec2F(), new Vec2F());
            Assert.IsTrue(text.GetShape().GetType() == typeof(DIKUArcade.Entities.StationaryShape));
        }
    }
}