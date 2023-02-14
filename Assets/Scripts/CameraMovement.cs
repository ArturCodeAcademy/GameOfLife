using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    [Header("Params")]
    [SerializeField, Min(0)] private float _movementSpeed;
    [SerializeField, Min(0)] private float _zoomSpeed;

	private Camera _camera;
	private float _maxSize;

	private const float MIN_SIZE = 1;

	private void Awake()
	{
		_camera= GetComponent<Camera>();	
	}

	private void Start()
	{
		ResetCamera();
	}

	private void LateUpdate()
	{
		// Zoom
		float scrollDelta = -Input.mouseScrollDelta.y;
		if (scrollDelta != 0)
		{
			float zoom = _camera.orthographicSize;
			zoom += scrollDelta * _zoomSpeed * _camera.orthographicSize;
			_camera.orthographicSize = Mathf.Clamp(zoom, MIN_SIZE, _maxSize);
		}		

		// Movement
		Vector3 transition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		transition.Normalize();
		transition *= _movementSpeed * Time.deltaTime * _camera.orthographicSize;

		float width = _camera.aspect * _camera.orthographicSize;
		float height = _camera.orthographicSize;

		Vector3 newPos = transform.position + transition;
		if (_grid.LeftBorder + width <= _grid.RightBorder - width)
			newPos.x = Mathf.Clamp(newPos.x, _grid.LeftBorder + width, _grid.RightBorder - width);
		else
			newPos.x = 0;
		if (_grid.DownBorder + height <= _grid.UpBorder - height)
			newPos.y = Mathf.Clamp(newPos.y, _grid.DownBorder + height, _grid.UpBorder - height);
		else
			newPos.y = 0;
		transform.position = newPos;
	}

	public void ResetCamera()
	{
		transform.position = Vector3.back * 10;
		if (_camera.aspect > _grid.RightBorder / _grid.UpBorder)
			_maxSize = _grid.UpBorder;
		else
			_maxSize = _grid.RightBorder / _camera.aspect;
		_camera.orthographicSize = _maxSize;
	}
}
