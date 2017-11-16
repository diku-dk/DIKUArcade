namespace DIKUArcade.Math
{
    public class Vec3i
    {
        public float X;
        public float Y;
        
        public Vec3i(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vec3i() : this(0.0f, 0.0f) { }

        public static Vec3i operator+ (Vec3i v1, Vec3i v2)
        {
            return new Vec3i(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vec3i operator- (Vec3i v1, Vec3i v2)
        {
            return new Vec3i(v1.X - v2.X, v1.Y - v2.Y);
        }
        // pairwise multiplication
        public static Vec3i operator *(Vec3i v1, Vec3i v2)
        {
            return new Vec3i(v1.X * v2.X, v1.Y * v2.Y);
        }
        public static float Dot(Vec3i v1, Vec3i v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y);
        }

        public double Length()
        {
            return System.Math.Sqrt((double) (X * X + Y * Y));
        }
    }
}