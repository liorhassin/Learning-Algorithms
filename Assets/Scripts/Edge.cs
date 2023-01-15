using Unity.VisualScripting;
using UnityEngine;

public class Edge
{
    private readonly Node _from;
    private readonly Node _to;
    private SpringJoint _sj;
    private float _weight;
    private GameObject _edgeObject;
    private int _colorStatus;

    public Edge (Node from, Node to, float weight, GameObject prefab)
    {
        _from = from;
        _to = to;
        _weight = weight;
        _colorStatus = 0;
        CreateSpringJointConnection(prefab);
    }

    private void CreateSpringJointConnection(GameObject edgePrefab)
    {
        SpringJoint sj = _from.AddComponent<SpringJoint> ();
        sj.anchor = sj.transform.InverseTransformPoint(_from.transform.position);
        sj.autoConfigureConnectedAnchor = false;
        sj.enableCollision = true;
        sj.connectedBody = _to.GetComponent<Rigidbody>();
        sj.damper = 10f;
        sj.spring = 10f;
        sj.minDistance = 0.1f;
        _sj = sj;
        Vector3 toPosition = _to.transform.position;
        _edgeObject = Object.Instantiate(edgePrefab, new Vector3(toPosition.x, toPosition.y, toPosition.z), Quaternion.identity);
        _edgeObject.SetActive(false);
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
    
    public GameObject GetEdgeObject()
    {
        return _edgeObject;
    } 
    
    public void SetWeight(float weight)
    {
        _weight = weight;
    }

    public void SetEdgeMaterialColor(int status)
    {
        Material renderer = _edgeObject.GetComponent<Renderer>().material;
        switch(status)
        {
            case 0:
                renderer.color = Color.white;
                renderer.SetColor("_EmissionColor", Color.white);
                break;
            case 1:
                renderer.color = Color.yellow;
                renderer.SetColor("_EmissionColor", Color.yellow);
                break;
            case 2:
                renderer.color = Color.green;
                renderer.SetColor("_EmissionColor", Color.green);
                break;
            case 3:
                renderer.color = Color.black;
                renderer.SetColor("_EmissionColor", Color.black);
                break;
        }
        _colorStatus = status;
    }

    public int GetColorStatus()
    {
        return _colorStatus;
    }
}
