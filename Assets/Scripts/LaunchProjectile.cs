using System;
using MagicKit;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour {
	public GameObject Projectile;
	public GameObject SpawnLocation;
	public float SpeedMultiplier = 5.0f;
	public float MinimumSpeed = 0.2f;
	public float StopSpeed = 0.01f;
	public float MaximumSpeed = 3.0f;
	public Boolean UseTrigger;
	public int MaximumCount = 50;
	public GameObject ParticleSystem;
	
	// Components
	private ControllerInput _input;
	private Rigidbody _body;
	private ParticleSystem _particles;
	
	// State
	private Vector3 _lastLocation;
	private float _peakSpeed;

	// Use this for initialization
	void Start ()
	{
		_input = GetComponent<ControllerInput>();
		_input.OnTriggerDown += OnTriggerDown;
		_body = GetComponent<Rigidbody>();
		_particles = ParticleSystem.GetComponent<ParticleSystem>();
	}

	void OnTriggerDown()
	{
		if (!UseTrigger) return;
		Launch(SpeedMultiplier * MaximumSpeed);
	}

	private void Launch(float speed)
	{
		Debug.Log("LAUNCH!");
		GameObject spawned = Instantiate(Projectile, _input.position, Quaternion.identity);
		spawned.GetComponent<Rigidbody>().AddForce(SpawnLocation.transform.forward * speed);
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
		Debug.Log("SPEED: " + speed);
		if (speed <= MinimumSpeed)
		{
			_particles.Stop();
		}
		if (speed <= StopSpeed)
		{
			if (_peakSpeed >= MinimumSpeed)
			{
				Launch(_peakSpeed * SpeedMultiplier);
			}
			_peakSpeed = 0;
		}
		else
		{
			_peakSpeed = Math.Max(_peakSpeed, speed);
			
			var emission = _particles.emission;
			var particleCount = Mathf.Lerp(0, MaximumCount, speed / MaximumSpeed);
			emission.rateOverTime = particleCount;
			_particles.Play();
		}
	}
}
