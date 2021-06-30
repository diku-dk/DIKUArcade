namespace DIKUArcade.Math
{
    public static class Functions
    {
        /// <summary>
        /// Clamps `value` between `min` and `max`. Equivalent to max(min(value, max), min).
        /// </summary>
        public static float Clamp(float min, float max, float value)
        {
            return System.MathF.Max(System.MathF.Min(value, max), min);
        }

        /// <summary>
        /// Performs a linear interpolation between two floating-point values,
        /// that is returns `min` if value is less or equal to 0, `max` if value
        /// is greater than or equal to 1, and otherwise a linear interpolation
        /// between `min` and `max` based on `value`.
        /// </summary>
        public static float Lerp(float min, float max, float value)
        {
            float clampedValue = Clamp(0.0f, 1.0f, value);
            return min + (max - min) * clampedValue;
        }

        /// <summary>
        /// Performs an element-wise linear interpolation between the X-and Y-coordinates
        /// of the input vectors `min` and `max`. Returns `min` if value is less than or
        /// equal to 0, `max` if value is greater than or equal to 1, and otherwise an
        /// element-wise linear interpolation between the X- and Y-coordinates of the
        /// input vectors.
        /// </summary>
        public static Vec2F Lerp(Vec2F min, Vec2F max, float value)
        {
            float clampedValue = Clamp(0.0f, 1.0f, value);
            return new Vec2F(
                min.X + (max.X - min.X) * clampedValue,
                min.Y + (max.Y - min.Y) * clampedValue);
        }
    }
}
