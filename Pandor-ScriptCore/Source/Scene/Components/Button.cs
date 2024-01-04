using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Button : BaseComponent
    {
        public Vector4 defaultColor
        {
            set
            {
                InternalCalls.Button_SetDefaultColor(gameObject.ID, ID, ref value);
            }
            get
            {
                InternalCalls.Button_GetDefaultColor(gameObject.ID, ID, out Vector4 value);
                return value;
            }
        }

        public Vector4 highlightedColor
        {
            set
            {
                InternalCalls.Button_SetHighlightedColor(gameObject.ID, ID, ref value);
            }
            get
            {
                InternalCalls.Button_GetHighlightedColor(gameObject.ID, ID, out Vector4 value);
                return value;
            }
        }

        public Vector4 pressedColor
        {
            set
            {
                InternalCalls.Button_SetPressedColor(gameObject.ID, ID, ref value);
            }
            get
            {
                InternalCalls.Button_GetPressedColor(gameObject.ID, ID, out Vector4 value);
                return value;
            }
        }
    }
}
