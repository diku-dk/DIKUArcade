namespace DIKUArcade.Math {
    public class Vec4D {
        public double W;
        public double X;
        public double Y;
        public double Z;

        public Vec4D(double x, double y, double z, double w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vec4D() : this(0.0f, 0.0f, 0.0f, 0.0f) { }

        public static Vec4D operator +(Vec4D v1, Vec4D v2) {
            return new Vec4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vec4D operator -(Vec4D v1, Vec4D v2) {
            return new Vec4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        // pairwise multiplication
        public static Vec4D operator *(Vec4D v1, Vec4D v2) {
            return new Vec4D(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
        }

        public static Vec4D operator *(Vec4D v, double s) {
            return new Vec4D(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static Vec4D operator *(double s, Vec4D v) {
            return new Vec4D(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static double Dot(Vec4D v1, Vec4D v2) {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        public Vec4D Copy() {
            return new Vec4D(X, Y, Z, W);
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