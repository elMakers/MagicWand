using MagicKit;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour {
	public GameObject Projectile;
	public GameObject SpawnLocation;
	public float Speed = 10.0f;
	
	private ControllerInput _input;

	// Use this for initialization
	void Start ()
	{
		_input = GetComponent<ControllerInput>();
		_input.OnTriggerDown += OnTriggerDown;
	}

	void OnTriggerDown()
	{
		GameObject spawned = Instantiate(Projectile, _input.position, Quaternion.identity);
		spawned.GetComponent<Rigidbody>().AddForce(SpawnLocation.transform.forward * Speed);
	}
}
