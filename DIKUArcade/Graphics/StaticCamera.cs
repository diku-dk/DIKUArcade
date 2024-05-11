namespace DIKUArcade.Graphics;

using DIKUArcade.Math;

public class StaticCamera : Camera {
    public StaticCamera() { 
        Offset = new Vec2F(0f, 0f);
        Scale = 1f;
    }
}
