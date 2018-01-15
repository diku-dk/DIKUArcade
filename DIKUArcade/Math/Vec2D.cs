namespace DIKUArcade.Math {
    public class Vec2D {
        public double X;
        public double Y;

        public Vec2D(double x, double y) {
            X = x;
            Y = y;
        }

        public Vec2D() : this(0.0f, 0.0f) { }

        public static Vec2D operator +(Vec2D v1, Vec2D v2) {
            return new Vec2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vec2D operator -(Vec2D v1, Vec2D v2) {
            return new Vec2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        // pairwise multiplication
        public static Vec2D operator *(Vec2D v1, Vec2D v2) {
            return new Vec2D(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vec2D operator *(Vec2D v, double s) {
            return new Vec2D(v.X * s, v.Y * s);
        }

        public static Vec2D operator *(double s, Vec2D v) {
            return new Vec2D(v.X * s, v.Y * s);
        }

        public static double Dot(Vec2D v1, Vec2D v2) {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y);
        }

        public Vec2D Copy() {
            return new Vec2D(X, Y);
        }

        public override int GetHashCode() {
            // Source: http://stackoverflow.com/a/263416/5801152
            unchecked // Overflow is fine, just wrap
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString() {
            return $"Vec2D({X},{Y})";
        }
    }
}