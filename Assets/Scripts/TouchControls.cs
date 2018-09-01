using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	public static bool WALK_RIGHT, WALK_LEFT, IDLE;

	private void Awake()
	{
		WALK_LEFT = false;
		WALK_RIGHT = false;
		IDLE = true;
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (gameObject.name == "Left Button")
		{
			WALK_LEFT = true;
			WALK_RIGHT = false;
			IDLE = false;
		}
		else if (gameObject.name == "Right Button")
		{
			WALK_RIGHT = true;
			WALK_LEFT = false;
			IDLE = false;
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		WALK_RIGHT = false;
		WALK_LEFT = false;
		IDLE = true;
	}

}
