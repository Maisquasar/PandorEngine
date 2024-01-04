using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Rigidbody : BaseComponent 
    {
        public float mass
        {
            get
            {
                return InternalCalls.Rigidbody_GetMass(gameObject.ID, ID);
            }
            set
            {
                InternalCalls.Rigidbody_SetMass(gameObject.ID, ID, ref value);
            }
        }

        public Vector3 velocity
        {
            get
            {
                InternalCalls.Rigidbody_GetVelocity(gameObject.ID, ID, out Vector3 velocity);
                return velocity;
            }
            set
            {
                InternalCalls.Rigidbody_SetVelocity(gameObject.ID, ID, ref value);
            }
        }

        public Vector3 angularVelocity
        {
            get
            {
                InternalCalls.Rigidbody_GetAngularVelocity(gameObject.ID, ID, out Vector3 angularVelocity);
                return angularVelocity;
            }
            set
            {
                InternalCalls.Rigidbody_SetAngularVelocity(gameObject.ID, ID, ref value);
            }
        }

        public bool useGravity
        {
            get
            {
                return InternalCalls.Rigidbody_GetGravity(gameObject.ID, ID);
            }
            set
            {
                InternalCalls.Rigidbody_SetGravity(gameObject.ID, ID, ref value);
            }
        }

        public bool isKinematic
        {
            get
            {
                return InternalCalls.Rigidbody_GetKinematic(gameObject.ID, ID);
            }
            set
            {
                InternalCalls.Rigidbody_SetKinematic(gameObject.ID, ID, ref value);
            }
        }

        public bool fixedRotationX
        {
            get
            {
                return InternalCalls.Rigidbody_GetRotationX(gameObject.ID, ID);
            }
            set
            {
                InternalCalls.Rigidbody_SetRotationY(gameObject.ID, ID, ref value);
            }
        }

        public bool fixedRotationY
        {
            get
            {
                return InternalCalls.Rigidbody_GetRotationX(gameObject.ID, ID);
            }
            set
            {
                InternalCalls.Rigidbody_SetRotationZ(gameObject.ID, ID, ref value);
            }
        }

        public bool fixedRotationZ
        {
            get
            {
                return InternalCalls.Rigidbody_GetRotationX(gameObject.ID, ID);
            }
            set
            {
                InternalCalls.Rigidbody_SetRotationZ(gameObject.ID, ID, ref value);
            }
        }

        public void AddForce(Vector3 force)
        {
            InternalCalls.Rigidbody_AddForce(gameObject.ID, ID, force);
        }

        public void AddTorque(Vector3 torque)
        {
            InternalCalls.Rigidbody_AddTorque(gameObject.ID, ID, torque);
        }
    }
}
