using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public Vector2 TouchDist;
	public Vector2 PointerOld;
	protected int PointerId;
	public bool Pressed;
	public void OnPointerDown(PointerEventData eventData)
	{
		Pressed = true;
		PointerId = eventData.pointerId;
		PointerOld = eventData.position;
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		Pressed = false;
	}

	void Update()
	{
		if (Pressed)
		{
			if (PointerId >= 0 && PointerId < Input.touches.Length)
			{
				TouchDist = Input.touches[PointerId].position - PointerOld;
				PointerOld = Input.touches[PointerId].position;
				Debug.Log(TouchDist);
			}
			else
			{
				TouchDist = new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y) - PointerOld;
				PointerOld = Input.mousePosition;
			}
		}
		else
		{
			TouchDist = new Vector2();
		}
	}

	
}
