using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour 
{
	[SerializeField] private GameObject[] clouds;
	[SerializeField] private float offset = 0.5f;
	[SerializeField] private float timeBetweenClouds = 3.0f;
	[SerializeField] private GameObject cat;
	[SerializeField] private GameObject lanturn;
	
	private float minX, maxX;
	private bool keepSpawning = true;
	private int previousIndex = 0;

	private void Awake()
	{
		SetBounds();
	}

	/// <summary>
	/// Sets the values of minX and maxX according to player screen
	/// </summary>
	private void SetBounds()
	{
		Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
		maxX = bounds.x - offset;
		minX = -bounds.x + offset;
	}

	// Use this for initialization
	private void Start ()
	{
		UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
		StartCoroutine(SpawnCloudAtIntervals(timeBetweenClouds));
	}


	IEnumerator SpawnCloudAtIntervals(float secondsBetweenSpawns)
	{
		// Repeat until keepSpawning == false or this GameObject is disabled/destroyed.
		while (keepSpawning)
		{
			// Put this coroutine to sleep until the next spawn time.
			yield return new WaitForSeconds(secondsBetweenSpawns);
			// Now it's time to spawn again.
			Spawn();
		}
	}

	private void Spawn()
	{
		bool collectible = false;
		int randomIndex = (int)Mathf.Floor(UnityEngine.Random.Range(0, clouds.Length));
		while (randomIndex == previousIndex)
		{
			randomIndex = (int)Mathf.Floor(UnityEngine.Random.Range(0, clouds.Length));
			collectible = true;
		}
		previousIndex = randomIndex;
		float xPos = UnityEngine.Random.Range(minX, maxX);
		GameObject cloud = Instantiate(clouds[randomIndex], new Vector3(xPos, transform.position.y, 0), Quaternion.identity) as GameObject;
		TurnCloudIfCan(cloud);
		SpawnCollectibleIfNeeded(collectible, cloud);
	}

	private static void TurnCloudIfCan(GameObject cloud)
	{
		float chance = (int)Mathf.Floor(UnityEngine.Random.Range(0, 8));
		if (chance > 3)
		{
			Vector3 temp = cloud.transform.localScale;
			cloud.transform.localScale = new Vector3(-temp.x, temp.y, temp.z);
		}
	}

	private void SpawnCollectibleIfNeeded(bool collectible, GameObject cloud)
	{
		if (collectible && cloud.tag != "Deadly")
		{
			float chance = (int)Mathf.Floor(UnityEngine.Random.Range(0, 8));
			Vector3 pos = cloud.transform.position;
			Vector3 temp = new Vector3(pos.x, pos.y + 0.5f, pos.z);
			if (chance > 4)
			{
				GameObject neko = Instantiate(cat, temp, Quaternion.identity) as GameObject;
				neko.transform.SetParent(cloud.transform);
			}
			else
			{
				GameObject shiro = Instantiate(lanturn, temp, Quaternion.identity) as GameObject;
				shiro.transform.SetParent(cloud.transform);
			}
		}
	}
}
