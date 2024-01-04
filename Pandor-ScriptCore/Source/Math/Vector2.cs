using System;

namespace Pandor
{
	public struct Vector2
	{
		public float x, y;

		public static Vector2 zero => new Vector2(0.0f);
		public static Vector2 one => new Vector2(1.0f);
		public static Vector2 up => new Vector2(0.0f, 1.0f);
		public static Vector2 down => new Vector2(0.0f, -1.0f);
		public static Vector2 right => new Vector2(1.0f, 0.0f);
		public static Vector2 left => new Vector2(-1.0f, 0.0f);

		public Vector2(float scalar)
		{
			x = scalar;
			y = scalar;
		}

		public Vector2(float X, float Y)
		{
			x = X;
			y = Y;
		}

		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		public static Vector2 operator *(Vector2 vector, float scalar)
		{
			return new Vector2(vector.x * scalar, vector.y * scalar);
		}

		public static bool operator ==(Vector2 vec1, Vector2 vec2)
		{
			return vec1.x == vec2.x && vec1.y == vec2.y;
		}

		public static bool operator !=(Vector2 vec1, Vector2 vec2)
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
			return $"({x.ToString("F3")}, {y.ToString("F3")})";
		}

		public float LengthSquared()
		{
			return x * x + y * y;
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
		}

		public Vector2 GetNormalized()
		{
			float invLenght = 1.0f / Length();
			return new Vector2(x *= invLenght, y *= invLenght);
		}

		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public static float Distance(Vector2 a, Vector2 b)
		{
			return (a - b).Length();
		}
    }
}