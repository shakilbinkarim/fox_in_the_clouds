using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void PlayGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void HandleSound()
	{
		int music = PlayerPreferences.GetMusicOn();
		if (music == 1)
		{
			music = 0;
			PlayerPreferences.SetMusicOn(0);
		}
		else
		{
			music = 1;
			PlayerPreferences.SetMusicOn(1);
		}
		Text soundText = GameObject.Find("Sound Button").GetComponent<Text>();
		soundText.text = (music == 1) ? "Sound On" : "Sound Off";
		GameManager.instance.HandleGameBGMusic();
	}

}
