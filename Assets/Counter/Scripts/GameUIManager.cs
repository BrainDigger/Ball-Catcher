using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class GameUIManager : MonoBehaviour
{
	private GameObject gameOverScreen;
	private GameObject gameOverMenu;
	private GameObject highScoreMenu;
	private TextMeshProUGUI scoreText;
	private TextMeshProUGUI livesText;
	// Start is called before the first frame update
	void Start()
	{
		gameOverScreen = GameObject.Find("GameOverContainer");
		gameOverMenu = GameObject.Find("GameOverMenuContainer");
		highScoreMenu = GameObject.Find("HighScoreMenuContainer");
		scoreText = GameObject.Find("GameUIContainer/Score Text").GetComponent<TextMeshProUGUI>();
		livesText = GameObject.Find("GameUIContainer/Lives Text").GetComponent<TextMeshProUGUI>();
		// Hide game over screen
		gameOverScreen.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void SetScore(int score)
	{
		scoreText.SetText("SCORE\n" + score);
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
		gameOverScreen.transform.Find("HighScoreMenuContainer").gameObject.SetActive(isHighScore);
		gameOverScreen.transform.Find("GameOverMenuContainer").gameObject.SetActive(!isHighScore);
	}
}
