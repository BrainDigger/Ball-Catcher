using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	public GameObject explosionPrefab;

	private PlayerController playerControllerScript;

	// Start is called before the first frame update
	void Start()
	{
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("CatchObject"))
		{
			Instantiate(explosionPrefab, other.transform.position, explosionPrefab.transform.rotation);
			Destroy(other.gameObject);
			if (!playerControllerScript.gameOver)
			{
				playerControllerScript.ReduceLife();
			}
		}
	}

}
