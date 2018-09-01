using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour 
{

	private bool alive;

	[SerializeField] private AudioClip catClip, lightClip, deathClip;
	[SerializeField] private float scoreTime;
	[SerializeField] private ParticleSystem catParticle;
	[SerializeField] private ParticleSystem lanturnParticle;
	
	public static int score;
	public static int catCount;
	public static int lightCount;

	// Use this for initialization
	private void Start ()
	{
		InitVariables();
		StartCoroutine(KeepScore(this.scoreTime));
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Cat")
		{
			catCount++;
			if (PlayerPreferences.GetMusicOn() == 1)
			{
				AudioSource.PlayClipAtPoint(catClip, transform.position); 
			}
			GamePlayController.instance.SetCats(catCount);
			Vector3 pos = collider.transform.position;
			ShowParticles(pos, catParticle);
			Destroy(collider.gameObject);
		}
		if (collider.tag == "Light")
		{
			lightCount++;
			if (PlayerPreferences.GetMusicOn() == 1)
			{
				AudioSource.PlayClipAtPoint(lightClip, transform.position); 
			}
			GamePlayController.instance.SetLight(lightCount);
			Vector3 pos = collider.transform.position;
			ShowParticles(pos, lanturnParticle);
			Destroy(collider.gameObject);
		}
		if (collider.tag == "Deadly")
		{
			Vector3 pos = PerformDeathFormallities(collider);
			ShowParticles(pos, catParticle);
			Destroy(collider.gameObject);
			// TODO: play lost life animation
		}
		if (collider.tag == "DeadlyImmortal")
		{
			catCount = 0;
			Vector3 pos = PerformDeathFormallities(collider);
		}
	}

	private Vector3 PerformDeathFormallities(Collider2D collider)
	{
		if (PlayerPreferences.GetMusicOn() == 1)
		{
			AudioSource.PlayClipAtPoint(deathClip, transform.position); 
		}
		catCount--;
		GamePlayController.instance.SetCats(catCount);
		GamePlayController.instance.ResetFox();
		if (catCount <= 0)
		{
			alive = false;
			GamePlayController.instance.SetFinalScore(score);
			GamePlayController.instance.GameOver();
		}
		Vector3 pos = collider.transform.position;
		return pos;
	}

	private void InitVariables()
	{
		alive = true;
		score = 0;
		catCount = 2;
		lightCount = 3;
		GamePlayController.instance.SetScore(score);
		GamePlayController.instance.SetLight(lightCount);
		GamePlayController.instance.SetCats(catCount);
	}

	void ShowParticles(Vector3 position, ParticleSystem particle)
	{
		if (!particle)
		{
			return;
		}
		ParticleSystem ps = (ParticleSystem)Instantiate(particle);
		ps.transform.position = position;
		ps.Play();
		Destroy(ps.gameObject, ps.duration);
	}

	IEnumerator KeepScore(float scoreTime)
	{
		// Repeat until keepSpawning == false or this GameObject is disabled/destroyed.
		while (alive)
		{
			yield return new WaitForSeconds(scoreTime);
			score++;
			GamePlayController.instance.SetScore(score);
		}
	}

}
