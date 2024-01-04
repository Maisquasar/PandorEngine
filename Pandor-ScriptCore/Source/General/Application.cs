using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Application
    {
        static public void QuitRequest()
        {
            InternalCalls.Application_QuitRequest();
        }

        static public float timeScale
        {
            set
            {
                InternalCalls.Application_SetTimeScale(value);
            }
            get
            {
                return InternalCalls.Application_GetTimeScale();
            }
        }
    }
}
