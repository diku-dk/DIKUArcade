namespace DIKUArcade.Math {
    public class Vec3F {
        public float X;
        public float Y;
        public float Z;

        public Vec3F(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3F() : this(0.0f, 0.0f, 0.0f) { }

        public static Vec3F operator +(Vec3F v1, Vec3F v2) {
            return new Vec3F(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vec3F operator -(Vec3F v1, Vec3F v2) {
            return new Vec3F(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        // pairwise multiplication
        public static Vec3F operator *(Vec3F v1, Vec3F v2) {
            return new Vec3F(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public static Vec3F operator *(Vec3F v, float s) {
            return new Vec3F(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vec3F operator *(float s, Vec3F v) {
            return new Vec3F(v.X * s, v.Y * s, v.Z * s);
        }

        public static float Dot(Vec3F v1, Vec3F v2) {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vec3F Copy() {
            return new Vec3F(X, Y, Z);
        }

        public override int GetHashCode() {
            // Source: http://stackoverflow.com/a/263416/5801152
            unchecked // Overflow is fine, just wrap
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }
    }
}