namespace DIKUArcade.Math {
    public class Vec4I {
        public int W;
        public int X;
        public int Y;
        public int Z;

        public Vec4I(int x, int y, int z, int w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vec4I() : this(0, 0, 0, 0) { }

        public static Vec4I operator +(Vec4I v1, Vec4I v2) {
            return new Vec4I(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vec4I operator -(Vec4I v1, Vec4I v2) {
            return new Vec4I(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        // pairwise multiplication
        public static Vec4I operator *(Vec4I v1, Vec4I v2) {
            return new Vec4I(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
        }

        public static Vec4I operator *(Vec4I v, int s) {
            return new Vec4I(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static Vec4I operator *(int s, Vec4I v) {
            return new Vec4I(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static int Dot(Vec4I v1, Vec4I v2) {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        public Vec4I Copy() {
            return new Vec4I(X, Y, Z, W);
        }

        public override int GetHashCode() {
            // Source: http://stackoverflow.com/a/263416/5801152
            unchecked // Overflow is fine, just wrap
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                hash = hash * 23 + W.GetHashCode();
                return hash;
            }
        }
    }
}