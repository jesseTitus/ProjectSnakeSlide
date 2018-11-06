using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For tracking the path all parts follow
/// </summary>
public class TransformationBuffer : MonoBehaviour
{

    public static TransformationBuffer _instance;

    public List<Transformation> _transformations = new List<Transformation>();
    int framesTracked = 300;

    // Adding newest at end of buffer
    // deleting oldest if buffer full
    public void AddTransformation(Transformation t)
    {
        _transformations.Add(t);
        if (_transformations.Count >= framesTracked)
        {
            _transformations.RemoveAt(0);
        }
    }

    public Transformation GetTransformation(int index)
    {
        //Debug.Log(string.Format("Count: {0}, index: {1}", _transformations.Count, index));
        return _transformations[_transformations.Count - 1 - (index)];
    }

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        } else if (_instance != this)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        //Debug.Log(_transformations.Count);
        //debugBuffer = wayPointsBuffer.Select(v => v.position).ToList();
        for (var i = 1; i < _transformations.Count; i+=2)
        {
            Debug.DrawLine(_transformations[i-1].position, _transformations[i].position, Color.red);
        }
    }
}
