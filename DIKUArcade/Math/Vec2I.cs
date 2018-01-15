namespace DIKUArcade.Math {
    public class Vec2I {
        public int X;
        public int Y;

        public Vec2I(int x, int y) {
            X = x;
            Y = y;
        }

        public Vec2I() : this(0, 0) { }

        public static Vec2I operator +(Vec2I v1, Vec2I v2) {
            return new Vec2I(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vec2I operator -(Vec2I v1, Vec2I v2) {
            return new Vec2I(v1.X - v2.X, v1.Y - v2.Y);
        }

        // pairwise multiplication
        public static Vec2I operator *(Vec2I v1, Vec2I v2) {
            return new Vec2I(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vec2I operator *(Vec2I v, int s) {
            return new Vec2I(v.X * s, v.Y * s);
        }

        public static Vec2I operator *(int s, Vec2I v) {
            return new Vec2I(v.X * s, v.Y * s);
        }

        public static int Dot(Vec2I v1, Vec2I v2) {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y);
        }

        public Vec2I Copy() {
            return new Vec2I(X, Y);
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
    }
}