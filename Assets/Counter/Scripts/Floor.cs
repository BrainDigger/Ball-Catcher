using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	public GameObject explosionPrefab;

	private GameManager gameManager;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("CatchObject"))
		{
			Instantiate(explosionPrefab, other.transform.position, explosionPrefab.transform.rotation);
			Destroy(other.gameObject);
			if (gameManager.isGameActive)
			{
				gameManager.LoseLife();
			}
		}
	}

}
