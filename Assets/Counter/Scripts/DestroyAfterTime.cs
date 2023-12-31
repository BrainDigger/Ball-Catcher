using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	public float lifetime = 1.0f;
	// Start is called before the first frame update
	void Start()
	{
		Destroy(gameObject, lifetime);
	}

}
