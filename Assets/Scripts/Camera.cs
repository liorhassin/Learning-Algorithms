using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

  public GameObject graph;

  private float _rotationSpeed = 2.5f; // The speed at which the camera should rotate around the object
  private float _zoomSpeed = 2.5f; // The speed at which the camera should zoom in and out
  private float _minZoom = 10f; // The minimum distance the camera should be from the target
  private float _maxZoom = 1000f; // The maximum distance the camera should be from the target

  private float _currentZoom = 10f; // The current distance the camera is from the target
  private Vector3 _offset; // The distance between the target and the camera
  
  void Start()
  {
	  _offset = transform.position - graph.transform.position;
	  _currentZoom = _offset.magnitude;
  }

    void Update()
    {
	    transform.LookAt(graph.transform);
		float horizontal = Input.GetAxis("Horizontal");
		float vertical_zoom = Input.GetAxis("Vertical_Zoom");

		_offset = Quaternion.AngleAxis(horizontal * _rotationSpeed, Vector3.up) * _offset;

		_currentZoom -= vertical_zoom * _zoomSpeed;
		_currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);
		_offset = _offset.normalized * _currentZoom;
		
		transform.position = graph.transform.position + _offset; // Update camera movement.
		
		//Support mouse right click movement.
		if (Input.GetMouseButton(1)) {
			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = Input.GetAxis("Mouse Y");
			_offset = Quaternion.AngleAxis(mouseX * _rotationSpeed, Vector3.up) * _offset;
			_offset = Quaternion.AngleAxis(mouseY * _rotationSpeed, Vector3.left) * _offset;
		}
    }
}
