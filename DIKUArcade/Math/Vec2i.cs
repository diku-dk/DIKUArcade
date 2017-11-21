namespace DIKUArcade.Math
{
    public class Vec2i
    {
        public int X;
        public int Y;

        public Vec2i(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Vec2i() : this(0, 0) {}

        public static Vec2i operator+ (Vec2i v1, Vec2i v2)
        {
            return new Vec2i(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vec2i operator- (Vec2i v1, Vec2i v2)
        {
            return new Vec2i(v1.X - v2.X, v1.Y - v2.Y);
        }
        // pairwise multiplication
        public static Vec2i operator* (Vec2i v1, Vec2i v2)
        {
            return new Vec2i(v1.X * v2.X, v1.Y * v2.Y);
        }
        public static int Dot(Vec2i v1, Vec2i v2)
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