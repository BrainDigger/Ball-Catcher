using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
	public bool gameOver;
	public int lives;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI livesText;
	public GameObject gameOverScreen;
	public AudioClip catchSound;
	public AudioClip missSound;
	public GameObject confettiPrefab;

	private int score = 0;
	private float horizontalInput;
	private float speed = 20.0f;
	private float zRange = 16;
	private AudioSource playerAudio;

	// Start is called before the first frame update
	void Start()
	{
		playerAudio = GetComponent<AudioSource>();
		// Reset score
		score = 0;
		// Hide game over screen
		gameOverScreen.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		// Show the correct number of lives left
		string stringLives = "";
		for ( int i = 0 ; i < lives ; i++ )
		{
			stringLives += "♥";
		}
		livesText.SetText(stringLives);

		// Check for left and right bounds
		if (transform.position.z < -zRange)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
		}

		if (transform.position.z > zRange)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
		}

		// Allow player movement left to right while game isn't over
		if (!gameOver)
		{
			// Get player input
			horizontalInput = Input.GetAxis("Horizontal");

			// Move the box
			transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalInput);

			// Rotate the wheels
			Transform wheelsR = transform.Find("right wheels");
			Transform wheelsL = transform.Find("left wheels");
			wheelsR.Rotate(Vector3.right, speed * horizontalInput * Time.deltaTime * 50.0f);
			wheelsL.Rotate(Vector3.right, speed * horizontalInput * Time.deltaTime * 50.0f);
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("CatchObject") && !gameOver)
		{
			score += 1;
			scoreText.SetText("SCORE\n" + score);
			playerAudio.PlayOneShot(catchSound, 0.3f);
			Instantiate(confettiPrefab, other.transform.position, confettiPrefab.transform.rotation);
			Destroy(other.gameObject);
		}
	}

	public void ReduceLife()
	{
		lives--;
		playerAudio.PlayOneShot(missSound, 0.3f);
		if (lives <= 0)
		{
			gameOver = true;
			gameOverScreen.gameObject.SetActive(true);
		}
	}

	public void MultiplySpeed(float amount)
	{
		speed *= amount;
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
