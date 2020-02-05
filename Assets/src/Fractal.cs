using System.Collections.Generic;
using UnityEngine;

namespace src
{
    public class Fractal : MonoBehaviour
    {
        private static readonly int maxDepth = 5;
        private static int currMaxDepth = maxDepth;
        
        private static List<GameObject>[] branches;
        private static Material[] materials;
        
        [SerializeField] private Material _material;
        [SerializeField] private Mesh _mesh;
        private int depth;

        private Vector3 from = Vector3.up;

        public static Transform rootTransform;
        [Range(0, 1)] [SerializeField] private float scaller = 0.5f;
        
        private void Start()
        {
            if (branches == null)
            {
                branches = new List<GameObject>[maxDepth + 1];
                materials = new Material[maxDepth + 1];

                for (var i = 0; i < branches.Length; i++)
                {
                    branches[i] = new List<GameObject>();
                    materials[i] = new Material(_material);
                    materials[i].color = Color.Lerp(Color.green, Color.blue, i / (float)maxDepth);
                    materials[i].color = Color.Lerp(Color.gray, Color.black, i / (float)maxDepth);
                }

                branches[0].Add(gameObject);
                rootTransform = gameObject.transform;
            }

            gameObject.AddComponent<MeshFilter>().mesh = _mesh;
            gameObject.AddComponent<MeshRenderer>().material = materials[depth];
//            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.blue, depth / (float)maxDepth);

            if (depth < maxDepth)
                foreach (var dir in Directions.d)
                    if (!dir.Equals(-from))
                    {
                        var child = new GameObject(depth + 1 + " depth").AddComponent<Fractal>();
                        child.Init(this, dir);

                        branches[depth + 1].Add(child.gameObject);
                    }
        }

        private void Init(Fractal parent, Vector3 dir)
        {
            from = dir;
            _mesh = parent._mesh;
            scaller = parent.scaller;
            depth = parent.depth + 1;

            var parentTrans = parent.transform;
            transform.localScale = parentTrans.localScale * scaller;
//            transform.localScale = Vector3.one * scaller;
            transform.parent = rootTransform;//parentTrans;
            var shift = new Vector3(
                parentTrans.localScale.x / 2 + transform.localScale.x / 2,
                parentTrans.localScale.y / 2 + transform.localScale.y / 2,
                parentTrans.localScale.z / 2 + transform.localScale.z / 2
            );
            
            shift = shift.Mult(dir);
            transform.position = parentTrans.position + shift;
        }

        public static void Decrease()
        {
            if (currMaxDepth >= 1)
            {
                foreach (var o in branches[currMaxDepth]) o.SetActive(false);
                currMaxDepth--;
            }
        }

        public static void Increase()
        {
            if (currMaxDepth < maxDepth)
            {
                currMaxDepth++;
                foreach (var o in branches[currMaxDepth]) o.SetActive(true);
            }
        }
    }
}