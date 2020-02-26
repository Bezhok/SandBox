using UnityEngine;

namespace src.Transformations
{
    public class Translation: Transformation
    {
        [SerializeField]
        private Vector3 _shift;
        public override Vector3 Apply(Vector3 point)
        {
            return point+_shift;
        }
    }
}