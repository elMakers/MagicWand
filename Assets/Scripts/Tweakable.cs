using MagicKit;
using UnityEngine;

public class Tweakable : MonoBehaviour
{
	public GameObject ChildObject;
	public float ScaleStep = 0.0001f;
	public float RotationStep = 0.1f;
	public float TranslationStep = 0.01f;

	private ControllerInput _input;
	private TweakMode _mode = TweakMode.TRANSLATION;
	private bool _isZ = false;

	private bool _isBumperDown = false;
	private bool _isTouchDown = false;
	private bool _isTriggerDown = false;

	private enum TweakMode
	{
		TRANSLATION,
		SCALE,
		ROTATION
	};
	
	// Use this for initialization
	void Start ()
	{
		_input = GetComponent<ControllerInput>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool bumper = _input.BumperDown;
		bool touch = _input.TouchActive;
		bool trigger = _input.TriggerDown;
		Vector2 touchPosition = _input.TouchPosition;

		if (trigger && !_isTriggerDown)
		{
			_isZ = !_isZ;
			Debug.Log("AXIS: " + (_isZ ? "Z" : "Y"));
		}
		
		switch (_mode)
		{
			case TweakMode.TRANSLATION:
				if (bumper && !_isBumperDown)
				{
					_mode = TweakMode.SCALE;
					Debug.Log("SCALE mode");
				}
				if (touch && !_isTouchDown)
				{
					Vector3 translation = new Vector3(touchPosition.x * TranslationStep,
						_isZ ? 0 : touchPosition.y * TranslationStep, _isZ ? touchPosition.y * TranslationStep : 0);
					ChildObject.transform.position += translation;
					Debug.Log("Translated by " + translation + " to: " + ChildObject.transform.position);
				}
				break;
			case TweakMode.SCALE:
				if (bumper && !_isBumperDown)
				{
					_mode = TweakMode.ROTATION;
					Debug.Log("ROTATION mode");
				}
				if (touch && !_isTouchDown)
				{
					Vector3 rotation = new Vector3(touchPosition.x * RotationStep,
						_isZ ? 0 : touchPosition.y * RotationStep, _isZ ? touchPosition.y * RotationStep : 0);
					ChildObject.transform.rotation *= Quaternion.LookRotation(rotation);
					Debug.Log("Rotated by + " + rotation + " to: " + ChildObject.transform.rotation.eulerAngles);
				}
				break;
			case TweakMode.ROTATION:
				if (bumper && !_isBumperDown)
				{
					_mode = TweakMode.TRANSLATION;
					Debug.Log("TRANSLATION mode");
				}
				
				if (touch && !_isTouchDown)
				{
					Vector3 scale = new Vector3(touchPosition.x * TranslationStep,
						touchPosition.x * ScaleStep, touchPosition.x * TranslationStep);
					ChildObject.transform.localScale += scale;
					Debug.Log("Scaled by " + scale + " to: " + ChildObject.transform.localScale);
				}
				break;
		}

		_isBumperDown = bumper;
		_isTouchDown = touch;
		_isTriggerDown = trigger;
	}
}
