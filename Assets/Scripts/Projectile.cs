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
		var particles = ParticleSystem.GetComponents<ParticleSystem>();
		foreach (var particle in particles)
		{
			particle.Stop();

			// Unparent the particles, give them a chance to finish
			particle.transform.parent = null;
			Destroy(particle.gameObject, particle.main.duration);
		}
		Destroy(gameObject);
	}
}
