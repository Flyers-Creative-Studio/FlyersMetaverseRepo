using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndRotate : MonoBehaviour
{
	public Camera cam;
	public float distanceToTarget = 10;

	private Vector3 previousPosition;
	public float perspectiveZoomSpeed = 0.5f;
	public float orthoZoomSpeed = 0.5f;
	public GameObject objectToRotate;
	public Bounds bounds;
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
			Vector3 direction = previousPosition - newPosition;

			float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
			float rotationAroundXAxis = direction.y * 180; // camera moves vertically

			//cam.transform.position = objectToRotate.transform.position;
			cam.transform.position = bounds.center;

			cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
			cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

			cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

			previousPosition = newPosition;
		}
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			// If the camera is orthographic...
			if (cam.orthographic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

				// Make sure the orthographic size never drops below zero.
				cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
			}
			else
			{
				// Otherwise change the field of view based on the change in distance between the touches.
				if (cam.fieldOfView>=8f && cam.fieldOfView<=100)
				{
					cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				}

				// Clamp the field of view to make sure it's between 0 and 180.
				cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 8f, 100);
			}

		}
	}
}


