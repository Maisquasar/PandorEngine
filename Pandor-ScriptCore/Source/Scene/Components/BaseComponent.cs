using System;
using System.Runtime.CompilerServices;

namespace Pandor
{
    public class BaseComponent
    {
        protected BaseComponent() { }
        internal BaseComponent(ulong objectID, ulong componentID)
        {
            gameObject = new GameObject(objectID);
            ID = componentID;
        }

        public bool enable
        {
            get
            {
                return InternalCalls.Component_GetEnable(gameObject.ID, ID);
            }
            set
            {
                InternalCalls.Component_SetEnable(gameObject.ID, ID, ref value);
            }
        }

        public Transform transform
        {
            get
            {
                return gameObject.transform;
            }
        }

        public GameObject gameObject;
        internal ulong ID;

        public void Destroy()
        {
            InternalCalls.Component_Destroy(gameObject.ID, ID);
        }
    }
}
