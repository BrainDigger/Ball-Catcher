using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class GameUIManager : MonoBehaviour
{
	private EventSystem eventSystem;
	private GameObject gameOverScreen;
	private GameObject gameOverMenu;
	private GameObject highScoreMenu;
	private TextMeshProUGUI gameOverText;
	private TextMeshProUGUI scoreText;
	private TextMeshProUGUI highscoreText;
	private TextMeshProUGUI livesText;

	// Start is called before the first frame update
	void Start()
	{
		//Set all private variables
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		gameOverScreen = GameObject.Find("GameOverContainer");
		gameOverMenu = gameOverScreen.transform.Find("GameOverMenuContainer").gameObject;
		highScoreMenu = gameOverScreen.transform.Find("HighScoreMenuContainer").gameObject;
		gameOverText = gameOverScreen.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
		scoreText = GameObject.Find("GameUIContainer/Score Text").GetComponent<TextMeshProUGUI>();
		highscoreText = GameObject.Find("GameUIContainer/High Score Text").GetComponent<TextMeshProUGUI>();
		livesText = GameObject.Find("GameUIContainer/Lives Text").GetComponent<TextMeshProUGUI>();

		// Hide game over screen
		gameOverScreen.gameObject.SetActive(false);
		// Show high score
		SetHighScore(DataManager.Instance.ScoreBoard[0].score);
	}

	public void SetScore(int score)
	{
		scoreText.SetText("SCORE\n" + score);
	}

	public void SetHighScore(int score)
	{
		highscoreText.SetText("HIGH SCORE\n" + score);
	}

	public void SetLives(int lives)
	{
		string stringLives = "";
		for ( int i = 0 ; i < lives ; i++ )
		{
			stringLives += "♥";
		}
		livesText.SetText(stringLives);
	}

	public void ShowGameOver(bool isHighScore = false)
	{
		gameOverScreen.gameObject.SetActive(true);
		highScoreMenu.gameObject.SetActive(isHighScore);
		gameOverMenu.gameObject.SetActive(!isHighScore);
		if (isHighScore)
		{
			gameOverText.SetText("NEW HIGH SCORE");
			eventSystem.SetSelectedGameObject(highScoreMenu.transform.GetChild(0).GetChild(0).gameObject);
		}
		else
		{
			gameOverText.SetText("GAME OVER");
			eventSystem.SetSelectedGameObject(gameOverMenu.transform.GetChild(0).GetChild(0).gameObject);
		}
	}
}
