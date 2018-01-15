namespace DIKUArcade.Math {
    public class Vec3D {
        public double X;
        public double Y;
        public double Z;

        public Vec3D(double x, double y, double z) {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3D() : this(0.0f, 0.0f, 0.0f) { }

        public static Vec3D operator +(Vec3D v1, Vec3D v2) {
            return new Vec3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vec3D operator -(Vec3D v1, Vec3D v2) {
            return new Vec3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        // pairwise multiplication
        public static Vec3D operator *(Vec3D v1, Vec3D v2) {
            return new Vec3D(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public static Vec3D operator *(Vec3D v, double s) {
            return new Vec3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vec3D operator *(double s, Vec3D v) {
            return new Vec3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static double Dot(Vec3D v1, Vec3D v2) {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vec3D Copy() {
            return new Vec3D(X, Y, Z);
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