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

        public Vec2f() : this(0.0f, 0.0f) { }

        public static Vec2f operator+ (Vec2f v1, Vec2f v2)
        {
            return new Vec2f(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vec2f operator- (Vec2f v1, Vec2f v2)
        {
            return new Vec2f(v1.X - v2.X, v1.Y - v2.Y);
        }
        // pairwise multiplication
        public static Vec2f operator* (Vec2f v1, Vec2f v2)
        {
            return new Vec2f(v1.X * v2.X, v1.Y * v2.Y);
        }
        public static float Dot(Vec2f v1, Vec2f v2)
        {
            return (v1.X*v2.X + v1.Y*v2.Y);
        }

        public double Length()
        {
            return System.Math.Sqrt((double) (X*X + Y*Y));
        }

        public override int GetHashCode()
        {
            // Source: http://stackoverflow.com/a/263416/5801152
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }
    }
}