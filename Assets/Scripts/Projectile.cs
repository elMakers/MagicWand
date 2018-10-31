using MagicKit;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public GameObject ParticleSystem;
	public GameObject Explosion;
	public float Lifespan = 10.0f;

	private ControllerInput _input;

	// Use this for initialization
	void Awake()
	{
		Destroy(gameObject, Lifespan);
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log("Collided!");
		// Spawn an explosion
		if (Explosion)
		{
			Instantiate(Explosion, transform.position, Quaternion.identity);
		}
		
		// Stop the particle trail
		var particles = ParticleSystem.GetComponent<ParticleSystem>();
		particles.Stop();

		// Unparent the particles, give them a chance to finish
		particles.transform.parent = null;
		Destroy(particles.gameObject, particles.main.duration);
		Destroy(gameObject);
	}
}
