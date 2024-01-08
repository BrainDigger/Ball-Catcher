using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public class HighScore
	{
		public string name;
		public int score;

		public HighScore(string nameInit, int scoreInit)
		{
			name = nameInit;
			score = scoreInit;
		}
	}

	public static DataManager Instance;

	public HighScore[] ScoreBoard = {new HighScore("Mario", 4), new HighScore("Luigi", 3), new HighScore("Peach", 2), new HighScore("Daizy", 1), new HighScore("Toad", 0)};

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
		//LoadScoreBoard();
	}

	[System.Serializable]
	class SaveData
	{
		public HighScore[] ScoreBoard = new HighScore[5];
	}

	public void SaveScoreBoard()
	{
		SaveData data = new SaveData();
		data.ScoreBoard = ScoreBoard;

		string json = JsonUtility.ToJson(data);

		Debug.Log("Saving JSON:\n" + json);
		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadScoreBoard()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			ScoreBoard = data.ScoreBoard;
		}
	}

	public void AddScoreToBoard(string playerName, int playerScore)
	{
		for (int i = 0 ; i < ScoreBoard.Length ; i++)
		{
			if (playerScore > ScoreBoard[i].score)
			{
				for (int j = ScoreBoard.Length - 1 ; j > i ; j--)
				{
					ScoreBoard[j].name = ScoreBoard[j-1].name;
					ScoreBoard[j].score = ScoreBoard[j-1].score;
				}
				ScoreBoard[i].name = playerName;
				ScoreBoard[i].score = playerScore;
				return;
			}
		}
	}
}
