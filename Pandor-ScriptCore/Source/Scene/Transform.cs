using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Transform
    {
        internal Transform(GameObject _gameObject) { gameObject = _gameObject; }

        public GameObject gameObject;

        public override string ToString()
        {
            return $"Position = {position} | Rotation = {rotation} | Scale = {localScale}";
        }
        public Vector3 position
        {
            get
            {
                InternalCalls.Transform_GetPosition(gameObject.ID, out Vector3 position);
                return position;
            }
            set
            {
                InternalCalls.Transform_SetPosition(gameObject.ID, ref value);
            }
        }

        public Quaternion rotation
        {
            get
            {
                InternalCalls.Transform_GetRotation(gameObject.ID, out Quaternion rotation);
                return rotation;
            }
            set
            {
                InternalCalls.Transform_SetRotation(gameObject.ID, ref value);
            }
        }

        public Vector3 eulerAngles
        {
            get
            {
                InternalCalls.Transform_GetEulerAngles(gameObject.ID, out Vector3 eulerAngles);
                return eulerAngles;
            }
            set
            {
                InternalCalls.Transform_SetEulerAngles(gameObject.ID, ref value);
            }
        }

        public Vector3 localPosition
        {
            get
            {
                InternalCalls.Transform_GetLocalPosition(gameObject.ID, out Vector3 localPosition);
                return localPosition;
            }
            set
            {
                InternalCalls.Transform_SetLocalPosition(gameObject.ID, ref value);
            }
        }

        public Quaternion localRotation
        {
            get
            {
                InternalCalls.Transform_GetLocalRotation(gameObject.ID, out Quaternion localRotation);
                return localRotation;
            }
            set
            {
                InternalCalls.Transform_SetLocalRotation(gameObject.ID, ref value);
            }
        }

        public Vector3 localScale
        {
            get
            {
                InternalCalls.Transform_GetLocalScale(gameObject.ID, out Vector3 localScale);
                return localScale;
            }
            set
            {
                InternalCalls.Transform_SetLocalScale(gameObject.ID, ref value);
            }
        }

        public Vector3 localEulerAngles
        {
            get
            {
                InternalCalls.Transform_GetLocalEulerAngles(gameObject.ID, out Vector3 localEulerAngles);
                return localEulerAngles;
            }
            set
            {
                InternalCalls.Transform_SetLocalEulerAngles(gameObject.ID, ref value);
            }
        }

        public Vector3 forward
        {
            get
            {
                InternalCalls.Transform_GetForward(gameObject.ID, out Vector3 forward);
                return forward;
            }
        }

        public Vector3 up
        {
            get
            {
                InternalCalls.Transform_GetUp(gameObject.ID, out Vector3 up);
                return up;
            }
        }

        public Vector3 right
        {
            get
            {
                InternalCalls.Transform_GetRight(gameObject.ID, out Vector3 right);
                return right;
            }
        }

        public void Translate(Vector3 translation)
        {
            gameObject.transform.position += translation;
        }
        public void Rotate(Vector3 eulerAngle)
        {
            gameObject.transform.rotation *= Quaternion.Euler(eulerAngle);
        }
        public void RotateArround(Vector3 target, Vector3 axis, float angle)
        {
            InternalCalls.Transform_RotateArround(gameObject.ID, target, axis, angle);
        }
    }
}
