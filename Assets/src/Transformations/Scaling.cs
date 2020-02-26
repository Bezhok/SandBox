using UnityEngine;

namespace src.Transformations
{
    public class Scaling: Transformation
    {
        [SerializeField]
        private Vector3 _scale;
        public override Vector3 Apply(Vector3 point)
        {
            
            return point.MultRes(_scale);
        }
    }
}