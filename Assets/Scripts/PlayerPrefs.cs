using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPreferences
{

	public static int HighScore = 0;
	public static int MusicOn = 1;

	public static void SetHighScore(int score)
	{
		PlayerPrefs.SetInt("HighScore", score);
	}

	public static void SetMusicOn(int truthValue)
	{
		PlayerPrefs.SetInt("MusicOn", truthValue);
	}

	public static int GetHighScore()
	{
		if (PlayerPrefs.HasKey("HighScore"))
		{
			return PlayerPrefs.GetInt("HighScore"); 
		}
		else
		{
			PlayerPrefs.SetInt("HighScore", 0);
			return PlayerPrefs.GetInt("HighScore");
		}
	}

	public static int GetMusicOn()
	{
		if (PlayerPrefs.HasKey("MusicOn"))
		{
			return PlayerPrefs.GetInt("MusicOn"); 
		}
		else
		{
			PlayerPrefs.SetInt("MusicOn", 1);
			return PlayerPrefs.GetInt("MusicOn");
		}
	}

}
