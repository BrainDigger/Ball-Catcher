using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] spawnPrefabs;
	private float spawnRangeZ = 15.5f;
	private float spawnPosY = 22;
	private float spawnRate = 1.5f;
	private PlayerController playerControllerScript;

	void Start()
	{
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
		StartCoroutine(SpawnRandomObject());
	}

	// While game is active spawn a random object
	IEnumerator SpawnRandomObject()
	{
		while (!playerControllerScript.gameOver)
		{
			yield return new WaitForSeconds(spawnRate);
			// A 20% chance to increase object spawn rate and player movement speed
			if (Random.Range(0.0f, 1.0f) < 0.20)
			{
				float change = Random.Range(0.8f, 1.0f);
				spawnRate *= change;
				playerControllerScript.MultiplySpeed(1.0f/change);
			}
			int objectIndex = Random.Range(0, spawnPrefabs.Length);
			Vector3 spawnPos = new Vector3(0, spawnPosY, Random.Range(-spawnRangeZ, spawnRangeZ));
			Instantiate(spawnPrefabs[objectIndex], spawnPos, spawnPrefabs[objectIndex].transform.rotation);
		}
	}
}
