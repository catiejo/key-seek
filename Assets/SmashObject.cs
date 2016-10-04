using UnityEngine;
using System.Collections;

public class SmashObject : MonoBehaviour {
	public AudioSource smashSound;
	bool isMagic;
	private GameBehavior _controller;

	// Use this for initialization
	void Start () 
	{
		//Find the game controller object in the scene
		GameObject controllerObject = GameObject.FindWithTag ("GameController");
		if (controllerObject != null) {
			_controller = controllerObject.GetComponent<GameBehavior> ();
			smashSound = gameObject.GetComponent<AudioSource>();
		} else {
			Debug.Log ("SmashObject cannot find game controller.");
		}
	}

	void OnMouseDown () {
		smashSound.Play ();
		Debug.Log ("ouch!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
