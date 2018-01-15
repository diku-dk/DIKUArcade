namespace DIKUArcade.Math {
    public class Vec3I {
        public int X;
        public int Y;
        public int Z;

        public Vec3I(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3I() : this(0, 0, 0) { }

        public static Vec3I operator +(Vec3I v1, Vec3I v2) {
            return new Vec3I(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vec3I operator -(Vec3I v1, Vec3I v2) {
            return new Vec3I(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        // pairwise multiplication
        public static Vec3I operator *(Vec3I v1, Vec3I v2) {
            return new Vec3I(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public static Vec3I operator *(Vec3I v, int s) {
            return new Vec3I(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vec3I operator *(int s, Vec3I v) {
            return new Vec3I(v.X * s, v.Y * s, v.Z * s);
        }

        public static int Dot(Vec3I v1, Vec3I v2) {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vec3I Copy() {
            return new Vec3I(X, Y, Z);
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