using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class PMath
    {
        public static readonly float deg2rad = MathF.PI / 180.0f;
        public static readonly float rad2deg =  180.0f / MathF.PI;

        public static float Lerp(float start, float end, float t)
        {
            return start + (end - start) * t;
        }

        public static float SmoothLerp(float start, float end, float t)
        {
            return Lerp(start, end, t * t * (3f - 2f * t));
        }

        public static float EaseInLerp(float start, float end, float t)
        {
            return start + (end - start) * t * t;
        }

        public static float EaseOutLerp(float start, float end, float t)
        {
            return start + (end - start) * (1f - (1f - t) * (1f - t));
        }

        public static float PingPong(float t, float length)
        {
            t = Repeat(t, length * 2.0f);
            return length - Math.Abs(t - length);
        }

        public static float Repeat(float t, float length)
        {
            return t - (float)Math.Floor((t / length) * length);
        }

        public static Vector3 Lerp(Vector3 start, Vector3 end, float t)
        {
            return start + (end - start) * t;
        }

        public static Vector3 SmoothLerp(Vector3 start, Vector3 end, float t)
        {
            return Lerp(start, end, t * t * (3f - 2f * t));
        }
    }
}
