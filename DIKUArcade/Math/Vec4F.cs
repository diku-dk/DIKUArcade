namespace DIKUArcade.Math {
    public class Vec4F {
        public float W;
        public float X;
        public float Y;
        public float Z;

        public Vec4F(float x, float y, float z, float w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vec4F() : this(0.0f, 0.0f, 0.0f, 0.0f) { }

        public static Vec4F operator +(Vec4F v1, Vec4F v2) {
            return new Vec4F(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vec4F operator -(Vec4F v1, Vec4F v2) {
            return new Vec4F(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        // pairwise multiplication
        public static Vec4F operator *(Vec4F v1, Vec4F v2) {
            return new Vec4F(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
        }

        public static Vec4F operator *(Vec4F v, float s) {
            return new Vec4F(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static Vec4F operator *(float s, Vec4F v) {
            return new Vec4F(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static float Dot(Vec4F v1, Vec4F v2) {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        public Vec4F Copy() {
            return new Vec4F(X, Y, Z, W);
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