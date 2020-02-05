using UnityEngine;

namespace src
{
    public static class Vector3Extension
    {
        public static Vector3 Mult(this Vector3 l, Vector3 r)
        {
            return new Vector3(l.x * r.x, l.y * r.y, l.z * r.z);
        }
    }
}