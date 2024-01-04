using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public struct Quaternion
    {
        public float x, y, z, w;

        public static Quaternion identity => new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

        public Quaternion(float a)
        {
            x = a;
            y = a;
            z = a;
            w = a;
        }

        public Quaternion(float a, float b, float c, float d = 1)
        {
            x = a;
            y = b;
            z = c;
            w = d;
        }

        public Quaternion(Vector3 vec)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
            w = 1;
        }

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Quaternion operator -(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Quaternion operator *(Quaternion quat, float scalar)
        {
            return new Quaternion(quat.x * scalar, quat.y * scalar, quat.z * scalar, quat.w * scalar);
        }
        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            return new Quaternion(
                a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
                a.w * b.y + a.y * b.w + a.z * b.x - a.x * b.z,
                a.w * b.z + a.z * b.w + a.x * b.y - a.y * b.x,
                a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z);
        }

        public static bool operator ==(Quaternion a, Quaternion b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        }

        public static bool operator !=(Quaternion a, Quaternion b)
        {
            return (a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"({x.ToString("F3")}, {y.ToString("F3")}, {z.ToString("F3")}, {w.ToString("F3")})";
        }

        public Quaternion(float angle, Vector3 axis)
        {
            float rad = angle * PMath.deg2rad;
            rad /= 2;
            axis.Normalize();

            w = MathF.Cos(rad);
            x = MathF.Sin(rad) * axis.x;
            y = MathF.Sin(rad) * axis.y;
            z = MathF.Sin(rad) * axis.z;
        }

        public Quaternion(Vector4 vec)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
            w = vec.w;
        }

        public void Set(float a, float b, float c, float d)
        {
            x = a;
            y = b;
            z = c;
            w = d;
        }

        public void Set(Quaternion q)
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }

        public float NormalizeAngle(float angle)
        {
            float modAngle = MathF.IEEERemainder(angle, 360.0f);

            if (modAngle < 0.0f)
                return modAngle + 360.0f;
            else
                return modAngle;
        }

        public Vector3 NormalizeAngles(Vector3 angles)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);
            return angles;
        }

        public Vector3 ToEuler()
        {
            float sqw = w * w;
            float sqx = x * x;
            float sqy = y * y;
            float sqz = z * z;
            float unit = sqx + sqy + sqz + sqw; // if normalized is one, otherwise is correction factor
            float test = x * w - y * z;
            Vector3 v;

            if (test > 0.4995f * unit)
            { // singularity at north pole
                v.x = 2.0f * MathF.Atan2(y, x);
                v.y = -MathF.PI / 2;
                v.z = 0;
                return NormalizeAngles(v * PMath.rad2deg);
            }
            if (test < -0.4995f * unit)
            { // singularity at south pole
                v.x = -2.0f * MathF.Atan2(y, x);
                v.y = MathF.PI / 2.0f;
                v.z = 0;
                return NormalizeAngles(v * PMath.rad2deg);
            }
            Quaternion q = new Quaternion(w, z, x, y);
            v.x = MathF.Atan2(2.0f * q.x * q.w + 2.0f * q.y * q.z, 1 - 2.0f * (q.z * q.z + q.w * q.w));     // Yaw
            v.y = -MathF.Asin(2.0f * (q.x * q.z - q.w * q.y));                             // Pitch
            v.z = MathF.Atan2(2.0f * q.x * q.y + 2.0f * q.z * q.w, 1 - 2.0f * (q.y * q.y + q.z * q.z));      // Roll
            return NormalizeAngles(v * PMath.rad2deg);
        }

        public void Inverse()
        {
            float d = w * w + x * x + y * y + z * z;
            if (d == 0)
                return;

            d = 1 / d;
            Set(-x * d, -y * d, -z * d, w * d);
        }

        public Quaternion GetInverse()
        {
            float d = w * w + x * x + y * y + z * z;
            if (d == 0)
                return identity;

            d = 1 / d;
            return new Quaternion(-x * d, -y * d, -z * d, w * d);
        }

        public static Quaternion LookRotation(Vector3 forward, Vector3 up)
        {
            forward.Normalize();
            Vector3 vector = forward.GetNormalized();
            Vector3 vector2 = Vector3.Cross(up, vector).GetNormalized();
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            float m00 = vector2.x;
            float m01 = vector2.y;
            float m02 = vector2.z;
            float m10 = vector3.x;
            float m11 = vector3.y;
            float m12 = vector3.z;
            float m20 = vector.x;
            float m21 = vector.y;
            float m22 = vector.z;

            float num8 = (m00 + m11) + m22;
            Quaternion quaternion = new Quaternion();
            if (num8 > 0.0f)
            {
                float num = MathF.Sqrt(num8 + 1.0f);
                quaternion.w = num * 0.5f;
                num = 0.5f / num;
                quaternion.x = (m12 - m21) * num;
                quaternion.y = (m20 - m02) * num;
                quaternion.z = (m01 - m10) * num;
                return quaternion;
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                float num7 = MathF.Sqrt(((1.0f + m00) - m11) - m22);
                float num4 = 0.5f / num7;
                quaternion.x = 0.5f * num7;
                quaternion.y = (m01 + m10) * num4;
                quaternion.z = (m02 + m20) * num4;
                quaternion.w = (m12 - m21) * num4;
                return quaternion;
            }
            if (m11 > m22)
            {
                float num6 = MathF.Sqrt(((1.0f + m11) - m00) - m22);
                float num3 = 0.5f / num6;
                quaternion.x = (m10 + m01) * num3;
                quaternion.y = 0.5f * num6;
                quaternion.z = (m21 + m12) * num3;
                quaternion.w = (m20 - m02) * num3;
                return quaternion;
            }
            float num5 = MathF.Sqrt(((1.0f + m22) - m00) - m11);
            float num2 = 0.5f / num5;
            return new Quaternion( (m20 + m02) * num2 , (m21 + m12) * num2, 0.5f * num5,(m01 - m10) * num2 );
        }

        public static Quaternion SLerp(Quaternion a, Quaternion b, float ts)
        {
            if (ts < 0.0f)
                return a;
            else if (ts >= 1.0f)
                return b;
            float d = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
            float s0, s1;
            float sd = (d > 0.0f) ? 1 : ((d < 0.0f) ? -1 : 0);

            d = MathF.Abs(d);

            if (d < 0.9995f)
            {
                float s = MathF.Sqrt(1.0f - d * d);
                float q = MathF.Atan2(s, d);
                float c = MathF.Cos(ts * q);


                s1 = MathF.Sqrt(1.0f - c * c) / s;
                s0 = c - d * s1;
            }
            else
            {
                s0 = 1.0f - ts;
                s1 = ts;
            }

            return a * s0 + b * sd * s1;
        }

        public static Quaternion Euler(float x, float y, float z)
        {
            float angleX = x * PMath.deg2rad * 0.5f;
            float angleY = y * PMath.deg2rad * 0.5f;
            float angleZ = z * PMath.deg2rad * 0.5f;

            float sinX = MathF.Sin(angleX);
            float cosX = MathF.Cos(angleX);
            float sinY = MathF.Sin(angleY);
            float cosY = MathF.Cos(angleY);
            float sinZ = MathF.Sin(angleZ);
            float cosZ = MathF.Cos(angleZ);

            float cosYcosZ = cosY * cosZ;
            float sinYsinZ = sinY * sinZ;
            float cosYsinZ = cosY * sinZ;
            float sinYcosZ = sinY * cosZ;

            Quaternion quaternion;
            quaternion.x = sinX * cosYcosZ - cosX * sinYsinZ;
            quaternion.y = cosX * sinYcosZ + sinX * cosYsinZ;
            quaternion.z = cosX * cosYsinZ - sinX * sinYcosZ;
            quaternion.w = cosX * cosYcosZ + sinX * sinYsinZ;

            return quaternion;
        }

        public static Quaternion Euler(Vector3 eulerAngle)
        {
            float angleX = eulerAngle.x * PMath.deg2rad * 0.5f;
            float angleY = eulerAngle.y * PMath.deg2rad * 0.5f;
            float angleZ = eulerAngle.z * PMath.deg2rad * 0.5f;

            float sinX = MathF.Sin(angleX);
            float cosX = MathF.Cos(angleX);
            float sinY = MathF.Sin(angleY);
            float cosY = MathF.Cos(angleY);
            float sinZ = MathF.Sin(angleZ);
            float cosZ = MathF.Cos(angleZ);

            float cosYcosZ = cosY * cosZ;
            float sinYsinZ = sinY * sinZ;
            float cosYsinZ = cosY * sinZ;
            float sinYcosZ = sinY * cosZ;

            Quaternion quaternion;
            quaternion.x = sinX * cosYcosZ - cosX * sinYsinZ;
            quaternion.y = cosX * sinYcosZ + sinX * cosYsinZ;
            quaternion.z = cosX * cosYsinZ - sinX * sinYcosZ;
            quaternion.w = cosX * cosYcosZ + sinX * sinYsinZ;

            return quaternion;
        }
    }
}
