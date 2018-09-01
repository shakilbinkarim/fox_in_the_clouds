using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour 
{

	[SerializeField] private float velocity = 0.1f;

	private Vector2 offset = Vector2.zero;
	private Material material;
	
	// Use this for initialization
	private void Start ()
	{
		ScaleBGAccordingToScreenSize();
		InitMaterialAndOffset();
	}

	private void InitMaterialAndOffset()
	{
		material = GetComponent<Renderer>().material;
		offset = material.GetTextureOffset("_MainTex");
	}

	private void ScaleBGAccordingToScreenSize()
	{
		var height = Camera.main.orthographicSize * 2.0f;
		var width = height * Screen.width / Screen.height;
		transform.localScale = new Vector3(width, height, 0);
	}

	private void Update()
	{
		offset.y -= velocity * Time.deltaTime;
		material.SetTextureOffset("_MainTex", offset);
	}

}
