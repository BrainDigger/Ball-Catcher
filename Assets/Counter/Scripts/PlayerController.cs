using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
	public GameObject confettiPrefab;

	private GameManager gameManager;
	private float horizontalInput;
	private float speed = 20.0f;
	private float zRange = 16;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
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
		if (gameManager.isGameActive)
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
		if (other.gameObject.CompareTag("CatchObject") && gameManager.isGameActive)
		{
			gameManager.AddScore();
			Instantiate(confettiPrefab, other.transform.position, confettiPrefab.transform.rotation);
			Destroy(other.gameObject);
		}
	}

	public void MultiplySpeed(float amount)
	{
		speed *= amount;
	}

}
