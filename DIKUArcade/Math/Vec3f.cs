namespace DIKUArcade.Math
{
    public class Vec3f
    {
        public float X;
        public float Y;
        public float Z;
        
        public Vec3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3f() : this(0.0f, 0.0f, 0.0f) { }

        public static Vec3f operator+ (Vec3f v1, Vec3f v2)
        {
            return new Vec3f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vec3f operator- (Vec3f v1, Vec3f v2)
        {
            return new Vec3f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        // pairwise multiplication
        public static Vec3f operator* (Vec3f v1, Vec3f v2)
        {
            return new Vec3f(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }
        public static float Dot(Vec3f v1, Vec3f v2)
        {
            return (v1.X*v2.X + v1.Y*v2.Y + v1.Z*v2.Z);
        }

        public double Length()
        {
            return System.Math.Sqrt((double) (X*X + Y*Y + Z*Z));
        }
    }
}