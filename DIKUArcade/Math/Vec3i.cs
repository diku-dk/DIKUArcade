namespace DIKUArcade.Math
{
    public class Vec3i
    {
        public int X;
        public int Y;
        public int Z;

        public Vec3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3i() : this(0, 0, 0) { }

        public static Vec3i operator+ (Vec3i v1, Vec3i v2)
        {
            return new Vec3i(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vec3i operator- (Vec3i v1, Vec3i v2)
        {
            return new Vec3i(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        // pairwise multiplication
        public static Vec3i operator* (Vec3i v1, Vec3i v2)
        {
            return new Vec3i(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }
        public static int Dot(Vec3i v1, Vec3i v2)
        {
            return (v1.X*v2.X + v1.Y*v2.Y + v1.Z*v2.Z);
        }

        public double Length()
        {
            return System.Math.Sqrt((double) (X*X + Y*Y + Z*Z));
        }

        public override int GetHashCode()
        {
            // Source: http://stackoverflow.com/a/263416/5801152
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }
    }
}