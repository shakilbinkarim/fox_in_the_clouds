using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;
	
	private Text highScore, soundText;
	private AudioSource audioSource;

	// Called Before Start
	private void Awake ()
	{
		MakeSingleton();
		audioSource = GetComponent<AudioSource>();
		HandleGameBGMusic();
	}
	
	private void MakeSingleton()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += Loadedscene;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= Loadedscene;
	}

	private void Loadedscene(Scene scene, LoadSceneMode mode)
	{
		scene = SceneManager.GetActiveScene();
		if (scene.buildIndex == 0)
		{
			// スコア
			int score = PlayerPreferences.GetHighScore(); // 悪い
			highScore = GameObject.Find("High Score Text").GetComponent<Text>();
			highScore.text = "High Score: " + score;
			// 音楽
			int music = PlayerPreferences.GetMusicOn(); // 悪い
			soundText = GameObject.Find("Sound Button").GetComponent<Text>();
			soundText.text = (music == 1) ? "Sound On" : "Sound Off";
		}
	}

	public void HandleGameBGMusic()
	{
		if (PlayerPreferences.GetMusicOn() == 1)
		{
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		else
		{
			if (audioSource.isPlaying)
			{
				audioSource.Stop();
			}
		}
	}

}
