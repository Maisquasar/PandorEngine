using System;
using System.Collections.Generic;
using System.Text;

namespace Pandor
{
    public class Physic
    {
        public static bool RayCast(Vector3 origin, Vector3 direction, float distanceMax, out RaycastHit hit)
        {
            bool value = InternalCalls.Physic_Raycast(origin, direction, distanceMax, 
                out bool _hit, out Vector3 position, out Vector3 normal, out Collider collider, out float distance, out uint faceIndex);

            hit = new RaycastHit(_hit, position, normal, collider, distance, faceIndex);

            if (hit != null)
            {
                Console.WriteLine($"NOT NULL {hit}");
            }
            return value;
        }
    }
}
