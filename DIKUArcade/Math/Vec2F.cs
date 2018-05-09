namespace DIKUArcade.Math {
    public class Vec2F {
        public float X;
        public float Y;

        public Vec2F(float x, float y) {
            X = x;
            Y = y;
        }

        public Vec2F() : this(0.0f, 0.0f) { }

        public static Vec2F operator +(Vec2F v1, Vec2F v2) {
            return new Vec2F(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vec2F operator -(Vec2F v1, Vec2F v2) {
            return new Vec2F(v1.X - v2.X, v1.Y - v2.Y);
        }

        // pairwise multiplication
        public static Vec2F operator *(Vec2F v1, Vec2F v2) {
            return new Vec2F(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vec2F operator *(Vec2F v, float s) {
            return new Vec2F(v.X * s, v.Y * s);
        }

        public static Vec2F operator *(float s, Vec2F v) {
            return new Vec2F(v.X * s, v.Y * s);
        }

        public static float Dot(Vec2F v1, Vec2F v2) {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public double Length() {
            return System.Math.Sqrt(X * X + Y * Y);
        }

        public static Vec2F Normalize(Vec2F v) {
            return v.Copy() * (1.0f / (float)v.Length());
        }

        public Vec2F Copy() {
            return new Vec2F(X, Y);
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
            return $"Vec2F({X},{Y})";
        }
    }
}