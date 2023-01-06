using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private Node _from;
    private Node _to;
    private SpringJoint _sj;
    private float _weight;

    public Edge (Node from, Node to,SpringJoint sj, float weight)
    {
        _from = from;
        _to = to;
        _sj = sj;
        _weight = weight;
    }

    public float GetWeight()
    {
        return _weight;
    }
    
    public Node GetFrom()
    {
        return _from;
    }
    
    public Node GetTo()
    {
        return _to;
    }
    
    public SpringJoint GetSpringJoint()
    {
        return _sj;
    }
    
    public void SetWeight(float weight)
    {
        _weight = weight;
    }
}
