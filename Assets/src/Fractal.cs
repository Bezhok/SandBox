using System;
using System.Collections.Generic;
using src;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Mesh _mesh;
    private int depth = 1;

    private Vector3 from = Vector3.up;

    [Range(0, 6)] [SerializeField] private int maxDepth = 5;

    [Range(0, 1)] [SerializeField] private float scaller = 0.5f;
    
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = _mesh;
        gameObject.AddComponent<MeshRenderer>().material = _material;

        if (depth <= maxDepth)
            foreach (var dir in Directions.d)
                if (!dir.Equals(-from))
                {
                    var child = new GameObject(depth + " depth").AddComponent<Fractal>();
                    child.Init(this, dir);
                }
    }

    private void Init(Fractal parent, Vector3 dir)
    {
        from = dir;
        _mesh = parent._mesh;
        _material = parent._material;
        maxDepth = parent.maxDepth;
        scaller = parent.scaller;
        depth = parent.depth + 1;

        var parentTrans = parent.transform;
        transform.localScale = parentTrans.localScale * scaller;
        var shift = new Vector3(
            parentTrans.localScale.x / 2 + transform.localScale.x / 2,
            parentTrans.localScale.y / 2 + transform.localScale.y / 2,
            parentTrans.localScale.z / 2 + transform.localScale.z / 2
        );

        shift = Mult(shift, dir);
        transform.position = parentTrans.position + shift;
    }

    // Update is called once per frame
    private void Update()
    {
        GetInnput();
    }

    private void GetInnput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
        }
    }

    private Vector3 Mult(Vector3 l, Vector3 r)
    {
        return new Vector3(l.x * r.x, l.y * r.y, l.z * r.z);
    }
}