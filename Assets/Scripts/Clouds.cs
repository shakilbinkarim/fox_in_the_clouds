using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour 
{

	[SerializeField] private float speed = 1.0f;

	// Update is called once per frame
	void Update ()
	{
		Vector3 temp = gameObject.transform.position;
		temp.y = temp.y + (speed * Time.deltaTime);
		gameObject.transform.position = temp;
	}

}
