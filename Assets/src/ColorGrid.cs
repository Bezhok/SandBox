using System;
using System.Collections.Generic;
using src.Transformations;
using UnityEngine;
using UnityEngine.Assertions;

namespace src
{

    public class ColorGrid: MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int resolution = 50;
        private GameObject[] _points;
        private float _size = 5f;
        private bool isFixedSize = false;

        private List<Transformation> _transformations;
        private void Awake()
        {
            _transformations = new List<Transformation>();
            GenerateGrid();
        }

        private void Update()
        {
            GetComponents(_transformations);
            
            int idx = 0;
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    for (int k = 0; k < resolution; k++, idx++)
                    {
                        var pos = _points[idx].transform.localPosition;
                        for (int l = 0; l < _transformations.Count; l++)
                        {
                            pos = _transformations[l].Apply(pos);
                        }
                        
                        _points[idx].transform.localPosition = pos;
                    }
                }
            }
        }

        private void GenerateGrid()
        {
            if (_prefab == null) throw new NullReferenceException();
            
            _points = new GameObject[resolution*resolution*resolution];

            int idx = 0;
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    for (int k = 0; k < resolution; k++, idx++)
                    {
                        _points[idx] = GeneratePoint(i, j, k);
                    }
                }
            }
        }

        private GameObject GeneratePoint(int i, int j, int k)
        {

            var obj = Instantiate(_prefab, this.transform, true);// new GameObject($"{i} {j} {k}");

            if (isFixedSize)
            {
                float scale = 0.5f / resolution;
                obj.transform.localScale = new Vector3(scale, scale, scale);

                obj.GetComponent<MeshRenderer>().material.color = new Color((float) i / resolution,
                    (float) j / resolution, (float) k / resolution);

                float shift = 0.5f; //resolution * 0.5f;
                obj.transform.localPosition = new Vector3((float) i / resolution - shift,
                    (float) j / resolution - shift, (float) k / resolution - shift);
            }
            else
            {
                float scale = 0.5f;
                obj.transform.localScale = new Vector3(scale, scale, scale);

                obj.GetComponent<MeshRenderer>().material.color = new Color((float) i / resolution,
                    (float) j / resolution, (float) k / resolution);

                float shift = resolution * 0.5f;
                obj.transform.localPosition = new Vector3((float) i *scale*2  - shift,
                    (float) j *scale*2- shift, (float) k *scale*2  - shift);
            }

            return obj;
        }
    }
}