using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
	public class Input
	{
		public static bool IsKeyDown(Key key)
		{
			return InternalCalls.Input_IsKeyDown(key);
		}
		public static bool IsKeyPressed(Key key)
		{
			return InternalCalls.Input_IsKeyPressed(key);
		}
		public static bool IsKeyReleased(Key key)
		{
			return InternalCalls.Input_IsKeyReleased(key);
		}
		public static Vector2 GetMousePos()
		{
			InternalCalls.Input_GetMousePos(out Vector2 vec);
			return vec;
		}
		public static void EnableCursor()
		{
			InternalCalls.Input_EnableCursor();
		}
		public static void DisableCursor()
		{
			InternalCalls.Input_DisableCursor();
		}
		public static void FixCursor()
		{
			InternalCalls.Input_FixCursor();
		}
	}
}
