namespace DIKUArcade.Math
{
    public class Vec2f
    {
        public float X;
        public float Y;
        
        public Vec2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Move(Vec2f vec)
        {
            this.X += vec.X;
            this.Y += vec.Y;
        }
    }
}
