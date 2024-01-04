using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Text : BaseComponent
    {
        public string text { 
            set
            {
                InternalCalls.Text_SetText(gameObject.ID, ID, value);
            }
            get
            {
                return InternalCalls.Text_GetText(gameObject.ID, ID);
            }
        }

        public float scale
        {
            set
            {
                InternalCalls.Text_SetScale(gameObject.ID, ID, ref value);
            }
            get
            {
                return InternalCalls.Text_GetScale(gameObject.ID, ID);
            }
        }

        public Vector4 color
        { 
            set
            {
                InternalCalls.Text_SetColor(gameObject.ID, ID, ref value);
            }
            get
            {
                InternalCalls.Text_GetColor(gameObject.ID, ID, out Vector4 value);
                return value;
            }
        }
    }
}
