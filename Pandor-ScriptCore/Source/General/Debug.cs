using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Debug
    {
		public static void Print(string log)
		{
			InternalCalls.Debug_Print(log);
		}
		public static void PrintWarning(string log)
		{
			InternalCalls.Debug_PrintWarning(log);
		}
		public static void PrintError(string log)
		{
			InternalCalls.Debug_PrintError(log);
		}
	}
}
