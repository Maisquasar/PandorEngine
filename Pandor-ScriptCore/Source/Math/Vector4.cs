using System;

namespace Pandor
{
	public struct Vector4
	{
		public float x, y, z, w;

		public static Vector4 zero => new Vector4(0.0f);
		public static Vector4 one => new Vector4(1.0f);

		public Vector4(float scalar)
		{
			x = scalar;
			y = scalar;
			z = scalar;
			w = scalar;
		}

		public Vector4(float X, float Y, float Z, float W)
		{
			x = X;
			y = Y;
			z = Z;
			w = W;
		}

		public Vector4(Vector3 vec3, float W = 0.0f)
		{
			x = vec3.x;
			y = vec3.y;
			z = vec3.z;
			w = W;
		}

		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		public static Vector4 operator *(Vector4 vector, float scalar)
		{
			return new Vector4(vector.x * scalar, vector.y * scalar, vector.z * scalar, vector.w * scalar);
		}

		public static bool operator ==(Vector4 vec1, Vector4 vec2)
		{
			return vec1.x == vec2.x && vec1.y == vec2.y && vec1.z == vec2.z && vec1.w == vec2.w;
		}

		public static bool operator !=(Vector4 vec1, Vector4 vec2)
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
			return $"({x.ToString("F3")}, {y.ToString("F3")}, {z.ToString("F3")}, {w.ToString("F3")})";
		}

		public float LengthSquared()
		{
			return x * x + y * y + z * z + w * w;
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
			w *= invLenght;
		}

		public Vector4 GetNormalized()
		{
			float invLenght = 1.0f / Length();
			return new Vector4(x *= invLenght, y *= invLenght, z *= invLenght, w *= invLenght);
		}

		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		public static float Distance(Vector4 a, Vector4 b)
		{
			return (a - b).Length();
		}
	}
}
