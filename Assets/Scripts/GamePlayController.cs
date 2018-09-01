using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GamePlayController : MonoBehaviour 
{
	[HideInInspector] public static GamePlayController instance;

	[SerializeField] private ParticleSystem teleportParticle;
	[SerializeField] private GameObject fox;
	[SerializeField] private Text scoreText, lightText, catText, finalScoreText;
	[SerializeField] private GameObject pausePanel, gameOverPanel;
	[SerializeField] private GameObject pauseButton, teleportButton;
	[SerializeField] private AudioClip teleClip;

	private Vector3 foxPos;
	private bool isPaused; // Don't like how I am using this

	private void Awake()
	{
		MakeInstance();
		isPaused = false;
	}
	
	private void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	
	/// <summary>
	/// Teleport the fox
	/// </summary>
	public void Teleport()
	{
		if (PlayerScore.lightCount > 0)
		{
			ShowParticles(fox.transform.position, teleportParticle);
			foxPos = fox.transform.position;
			AudioSource.PlayClipAtPoint(teleClip, foxPos);
			fox.transform.position = new Vector3(-100, -100, 0);
			Invoke("SpawnFox", 0.5f);
			SetLight(--PlayerScore.lightCount); // I really don't like the way I'm handling Light Counts here
		}
	}

	// Pausing the Game ///////////////////
	public void PauseGame()
	{
		Time.timeScale = 0.0f;
		pausePanel.SetActive(true);
	}

	public void ResumeGame()
	{
		Time.timeScale = 1.0f;
		pausePanel.SetActive(false);
	}

	public void QuitGame()
	{
		Time.timeScale = 1.0f;
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}

	public void GameOver()
	{
		Destroy(fox);
		pauseButton.SetActive(false);
		teleportButton.SetActive(false);
		ShowAdd();
		gameOverPanel.SetActive(true);
	}

	private static void ShowAdd()
	{
		float addDecider = Random.Range(1, 99);
		if (addDecider > 50)
		{
			if (Advertisement.IsReady())
			{
				Advertisement.Show();
			}
		}
	}

	// Pausing the Game ///////////////////

	// Update UI with Sccores /////////////
	public void SetScore(int score)
	{
		scoreText.text = "x " + score;
	}

	public void SetCats(int cats)
	{
		catText.text = "x " + cats;
	}

	public void SetLight(int lights)
	{
		lightText.text = "x " + lights;
	}

	public void SetFinalScore(int score)
	{
		if (score < PlayerPreferences.GetHighScore())
		{
			finalScoreText.text = "Score: " + score;
		}
		else
		{
			finalScoreText.text = "High Score: " + score;
			PlayerPreferences.SetHighScore(score);
		}
	}

	// Update UI with Sccores /////////////

	// Helper Methods /////////////////////
	void SpawnFox()
	{
		GameObject [] clouds = GameObject.FindGameObjectsWithTag("Cloud");
		float minY = 100;
		GameObject cloud = (clouds.Length > 0) ? clouds[0] : GameObject.FindGameObjectWithTag("Cloud") as GameObject;
		for (int i = 0; i < clouds.Length; i++)
		{
			Vector3 currentPos = clouds[i].transform.position;
			if (i == 0)
			{
				minY = currentPos.y;
				cloud = clouds[i];
			}
			else
			{
				if (minY > currentPos.y)
				{
					minY = currentPos.y;
					cloud = clouds[i];
				}
			}
		}
		if (cloud)
		{
			Vector3 foxCloudPos = cloud.transform.position;
			Vector3 newPos = new Vector3(foxCloudPos.x, foxCloudPos.y + 0.7f, foxCloudPos.z);
			ShowParticles(newPos, teleportParticle);
			fox.transform.position = newPos; 
		}
		else
		{
			Vector3 newPos = new Vector3(foxPos.x, foxPos.y - 2f, foxPos.z);
			ShowParticles(newPos, teleportParticle);
			fox.transform.position = newPos;
		}
	}

	void ShowParticles(Vector3 position, ParticleSystem particle)
	{
		if (!particle)
		{
			return;
		}
		ParticleSystem ps = (ParticleSystem) Instantiate(particle);
		ps.transform.position = position;
		ps.Play();
		Destroy(ps.gameObject, ps.duration);
	}

	public void ResetFox()
	{
		fox.transform.position = new Vector3(-0.62f, 7.0f, 0);
	}

	public void RestartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}

	// Helper Methods /////////////////////

}
