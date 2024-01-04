using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class GameObject
    {
        internal GameObject(ulong uuid)
        {
            ID = uuid;
            transform = new Transform(this);
        }

        public readonly ulong ID;
        public Transform transform;

        public bool HasComponent<T>() where T : BaseComponent, new()
        {
            Type componentType = typeof(T);
            return InternalCalls.Object_HasComponent(ID, componentType);
        }

        public T GetComponent<T>() where T : BaseComponent, new()
        {
            Type componentType = typeof(T);
            ulong componentID = InternalCalls.Object_GetComponent(ID, componentType);
            if (componentID == 0)
                return null;

            T component = new T() { gameObject = this, ID = componentID };
            return component;
        }

        public T As<T>() where T : ScriptComponent, new()
        {
            object instance = InternalCalls.Object_GetScriptInstance(ID, new T());
            return instance as T;
        }

        public GameObject GetParent()
        {
            ulong parentID = InternalCalls.Object_GetParent(ID);
            if (parentID != 0)
                return new GameObject(parentID);
            return null;
        }

        public GameObject GetChild(uint id)
        {
            ulong childID = InternalCalls.Object_GetChild(ID, id);
            if (childID != 0)
                return new GameObject(childID);
            return null;
        }

        public List<GameObject> GetChildren()
        {
            InternalCalls.Object_GetChildren(ID, out ulong[] childrens);
            List<GameObject> gameObjects = new List<GameObject>();
            for (int i = 0; i < childrens.Length; i++)
            {
                gameObjects.Add(new GameObject(childrens[i]));
            }
            return gameObjects;
        }

        public List<BaseComponent> GetComponents()
        {
            InternalCalls.Object_GetComponents(ID, out ulong[] components);
            List<BaseComponent> componentsList = new List<BaseComponent>();
            for (int i = 0; i < components.Length; i++)
            {
                componentsList.Add(new BaseComponent(ID, components[i]));
            }
            return componentsList;
        }

        public T AddComponent<T>() where T : BaseComponent, new()
        {
            Type componentType = typeof(T);
            var id = InternalCalls.Object_AddComponent(ID, componentType);

            if (id == 0)
                return null;

            return new T() { gameObject = this, ID = id };
        }

        public string name
        {
            set
            {
                InternalCalls.Object_SetName(ID, ref value);
            }
            get
            {
                return InternalCalls.Object_GetName(ID);
            }
        }

        public void Destroy()
        {
            InternalCalls.Object_Destroy(ID);
        }

        public void ClearParent()
        {
            InternalCalls.Object_ClearParent(ID);
        }

        public void SetParent(GameObject parent)
        {
            InternalCalls.Object_SetParent(ID, parent.ID);
        }

        public bool active
        {
            set
            {
                InternalCalls.Object_SetActive(ID, value);
            }
            get
            {
                return InternalCalls.Object_GetActive(ID);
            }
        }
    }
}