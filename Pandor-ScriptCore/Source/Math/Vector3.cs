using System;

namespace Pandor
{
	public struct Vector3
	{
		public float x, y, z;

		public static Vector3 zero => new Vector3(0.0f);
		public static Vector3 one => new Vector3(1.0f);
		public static Vector3 forward => new Vector3(0.0f, 0.0f, 1.0f);
		public static Vector3 back => new Vector3(0.0f, 0.0f, -1.0f);
		public static Vector3 up => new Vector3(0.0f, 1.0f, 0.0f);
		public static Vector3 down => new Vector3(0.0f, -1.0f, 0.0f);
		public static Vector3 right => new Vector3(1.0f, 0.0f, 0.0f);
		public static Vector3 left => new Vector3(-1.0f, 0.0f, 0.0f);

		public Vector3(float scalar)
		{
			x = scalar;
			y = scalar;
			z = scalar;
		}

		public Vector3(float X, float Y, float Z)
		{
			x = X;
			y = Y;
			z = Z;
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator *(Vector3 vector, float scalar)
		{
			return new Vector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }

        public static Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x / scalar, vector.y / scalar, vector.z / scalar);
        }

        public static bool operator ==(Vector3 vec1, Vector3 vec2)
		{
			return vec1.x == vec2.x && vec1.y == vec2.y && vec1.z == vec2.z;
		}

		public static bool operator !=(Vector3 vec1, Vector3 vec2)
		{
			return !(vec1 == vec2);
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
			return $"({x.ToString("F3")}, {y.ToString("F3")}, {z.ToString("F3")})";
		}

		public float LengthSquared()
		{
			return x * x + y * y + z * z;
		}

		public float Length()
		{
			return (float)Math.Sqrt(LengthSquared());
		}

		public void Normalize()
		{
			float invLenght = 1.0f / Length();
			x *= invLenght;
			y *= invLenght;
			z *= invLenght;
		}

		public Vector3 GetNormalized()
		{
			float invLenght = 1.0f / Length();
			return new Vector3(x *= invLenght, y *= invLenght, z *= invLenght);
		}

		public static float Dot(Vector3 a, Vector3 b)
        {
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public static Vector3 Cross(Vector3 a, Vector3 b)
		{
			return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
		}

		public static float Distance(Vector3 a, Vector3 b)
		{
			return (a - b).Length();
		}
	}
}
