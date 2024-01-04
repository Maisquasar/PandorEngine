using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class ScriptComponent : BaseComponent
    {
        public GameObject FindObjectByName(string name)
        {
            ulong objectID = InternalCalls.Script_FindObjectByName(name);
            if (objectID == 0)
                return null;

            return new GameObject(objectID);
        }

        public GameObject Instantiate(GameObject gameObject, Vector3 position, Quaternion rotation, GameObject parent)
        {
            ulong objectID = InternalCalls.Script_Instantiate(gameObject.ID, position, rotation, parent.ID);
            if (objectID == 0)
                return null;

            return new GameObject(objectID);
        }

        public virtual void OnCollisionEnter(Collider collider) {}
        public virtual void OnCollisionStay(Collider collider) {}
        public virtual void OnCollisionExit(Collider collider) {}
        public virtual void OnTriggerEnter(Collider collider) {}
        public virtual void OnTriggerStay(Collider collider) {}
        public virtual void OnTriggerExit(Collider collider) {}
    }
}
