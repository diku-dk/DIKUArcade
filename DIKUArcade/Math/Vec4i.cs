namespace DIKUArcade.Math
{
    public class Vec4i
    {
        public int X;
        public int Y;
        public int Z;
        public int W;

        public Vec4i(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vec4i() : this(0, 0, 0, 0) { }

        public static Vec4i operator+ (Vec4i v1, Vec4i v2)
        {
            return new Vec4i(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }
        public static Vec4i operator- (Vec4i v1, Vec4i v2)
        {
            return new Vec4i(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }
        // pairwise multiplication
        public static Vec4i operator* (Vec4i v1, Vec4i v2)
        {
            return new Vec4i(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
        }
        public static int Dot(Vec4i v1, Vec4i v2)
        {
            return (v1.X*v2.X + v1.Y*v2.Y + v1.Z*v2.Z + v1.W*v2.W);
        }

        public double Length()
        {
            return System.Math.Sqrt((double) (X*X + Y*Y + Z*Z + W*W));
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
                hash = hash * 23 + W.GetHashCode();
                return hash;
            }
        }
    }
}