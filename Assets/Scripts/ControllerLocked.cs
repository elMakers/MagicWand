using MagicKit;
using UnityEngine;

public class ControllerLocked : MonoBehaviour
{
	private ControllerInput _input;
	private Rigidbody _body;

	// Use this for initialization
	void Start ()
	{
		_input = GetComponent<ControllerInput>();
		_body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// _body.transform.position = Camera.main.transform.position + Camera.main.transform.forward * GrabDistance;

		_body.transform.position = _input.position;
		_body.transform.rotation = _input.orientation;
	}
}
