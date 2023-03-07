using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public RectTransform test;
    public GameObject objectToRotate;
    public float rotationSpeed;
    void OnMouseDrag()
    {
		if (objectToRotate!=null)
		{
            float YaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            if (objectToRotate) objectToRotate.transform.Rotate(Vector3.up, -YaxisRotation * rotationSpeed * Time.deltaTime);
        }
    }
	public void Update()
	{
        Debug.Log(test.rect.width);
        Debug.Log(test.rect.size);
    }
}
