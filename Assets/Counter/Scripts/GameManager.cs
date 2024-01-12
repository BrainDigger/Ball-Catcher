using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public List<GameObject> spawnPrefabs;
	public bool isGameActive;
	public AudioClip catchSound;
	public AudioClip missSound;

	private float spawnRangeZ = 15.5f;
	private float spawnPosY = 22;
	private float spawnRate = 1.5f;
	private AudioSource gameAudio;
	private PlayerController playerControllerScript;
	private GameUIManager gameUI;
	private int score;
	private int lives;

	void Start()
	{
		if (!DataManager.Instance)
		{
			MainMenu();
		}
		isGameActive = true;
		score = 0;
		lives = 3;
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
		gameUI = GameObject.Find("Canvas").GetComponent<GameUIManager>();
		gameAudio = GetComponent<AudioSource>();
		StartCoroutine(SpawnRandomObject());
	}

	// While game is active spawn a random object
	IEnumerator SpawnRandomObject()
	{
		while (isGameActive)
		{
			yield return new WaitForSeconds(spawnRate);
			// A 20% chance to increase object spawn rate and player movement speed
			if (Random.Range(0.0f, 1.0f) < 0.20)
			{
				float change = Random.Range(0.8f, 1.0f);
				spawnRate *= change;
				playerControllerScript.MultiplySpeed(1.0f/change);
			}
			int objectIndex = Random.Range(0, spawnPrefabs.Count);
			Vector3 spawnPos = new Vector3(0, spawnPosY, Random.Range(-spawnRangeZ, spawnRangeZ));
			Instantiate(spawnPrefabs[objectIndex], spawnPos, spawnPrefabs[objectIndex].transform.rotation);
		}
	}

	// Add a point to the score
	public void AddScore(int pointsToAdd = 1)
	{
		score += pointsToAdd;
		gameUI.SetScore(score);
		gameAudio.PlayOneShot(catchSound, 0.3f);
	}

	// Reduce player life and end the game if it reaches 0
	public void LoseLife()
	{
		lives--;
		gameUI.SetLives(lives);
		gameAudio.PlayOneShot(missSound, 0.3f);
		if (lives <= 0)
		{
			isGameActive = false;
			gameUI.ShowGameOver(score > DataManager.Instance.ScoreBoard[DataManager.Instance.ScoreBoard.Length - 1].score);
		}
	}

	// Saves player name and score to the scoreboard
	public void SaveHighScore(string playerName)
	{
		DataManager.Instance.AddScoreToBoard(playerName, score);
		DataManager.Instance.SaveScoreBoard();
		gameUI.ShowGameOver(false);
	}

	// Restart game by reloading the scene
	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Exit to main menu
	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}

}
