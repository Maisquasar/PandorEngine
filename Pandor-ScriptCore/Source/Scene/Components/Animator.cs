using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Animator : BaseComponent
    {
        void SetBoolean(string name, bool value)
        {
            InternalCalls.Animator_SetBoolean(gameObject.ID, ID, name, value);
        }
    }
}
