using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleOnEnded : MonoBehaviour
{
	private ParticleSystem particles;

	// Start is called before the first frame update
	void Start()
	{
		particles = GetComponent<ParticleSystem>();
		float totalDuration = particles.main.duration + particles.main.startLifetime.Evaluate(1);
		Destroy(gameObject, totalDuration);
	}

	// Update is called once per frame
	void Update()
	{
	}
}
