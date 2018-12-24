using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpeed : MonoBehaviour
{
	// Parameters
	public float MinimumSpeed = 0.01f;
	public float MaximumSpeed = 2.0f;
	public int MaximumCount = 50;
	
	// Instance variables
	private ParticleSystem _particles;
	private Rigidbody _body;
	
	// State
	private Vector3 _lastLocation;

	// Use this for initialization
	void Start ()
	{
		_particles = GetComponent<ParticleSystem>();
		_body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_lastLocation == Vector3.zero)
		{
			_lastLocation = _body.position;
			return;
		}

		Vector3 velocity = _body.position - _lastLocation;
		_lastLocation = _body.position;
		var speed = velocity.magnitude;
		if (speed <= MinimumSpeed)
		{
			_particles.Stop();
		}
		else
		{
			var emission = _particles.emission;
			var particleCount = Mathf.Lerp(0, MaximumCount, speed / MaximumSpeed);
			emission.rateOverTime = particleCount;
			_particles.Play();
		}
	}
}
