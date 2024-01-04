using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class RaycastHit
    {
        public RaycastHit(bool _hit, Vector3 _position, Vector3 _normal, Collider _collider, float _distance, uint _faceIndex)
        {
            hit = _hit;
            position = _position;
            normal = _normal;
            collider = _collider;
            distance = _distance;
            faceIndex = _faceIndex;
        }
        public bool hit;
        public Vector3 position;
        public Vector3 normal;
        public Collider collider;
        public float distance;
        public uint faceIndex;
    }
}
