using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceAtCamera : MonoBehaviour
{
    // Update is called once per frame
    private void LateUpdate()
    {
        transform.LookAt(transform.position + UnityEngine.Camera.main.transform.rotation * Vector3.forward, UnityEngine.Camera.main.transform.rotation * Vector3.up);
    }
}
