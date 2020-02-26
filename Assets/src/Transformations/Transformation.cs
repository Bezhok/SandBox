using UnityEngine;

namespace src.Transformations
{
    public abstract class Transformation: MonoBehaviour
    {
        public abstract Vector3 Apply(Vector3 point);
    }
}