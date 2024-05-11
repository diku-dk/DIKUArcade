﻿namespace DIKUArcadeUnitTests;

using DIKUArcade.Math;
using NUnit.Framework;

[TestFixture]
public class TestOpenGLContext {

    [Test]
    public void TestOpenGLContextTextObject() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        var text = new DIKUArcade.Graphics.Text("test", new Vec2F(), new Vec2F());
        Assert.IsTrue(text.GetShape().GetType() == typeof(DIKUArcade.Entities.StationaryShape));
    }
}
