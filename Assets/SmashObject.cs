using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SmashObject : MonoBehaviour {
	public AudioSource smashSound;
	public bool isMagic;
	private GameBehavior _controller;
	Color glowColor = Color.red;
	float glowIntensity = 0.3f;
	float intensityIncrement = 0.03f;

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
		StartCoroutine (glowObject ());
		smashSound.Play ();
//		Debug.Log ("ouch!");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator glowObject()
	{
		Material material = gameObject.GetComponent<Renderer>().material;
		float curIntensity = 0.0f;
		if (isMagic) {
			_controller.winGame ();
			glowColor = Color.green;
		}
		material.EnableKeyword("_EMISSION");
		material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
		// Increase intensity (fade in)
		while (curIntensity < glowIntensity)
		{
			material.SetColor("_EmissionColor", glowColor * curIntensity);
			curIntensity += intensityIncrement;
			yield return new WaitForSeconds(0.05f);
//			Debug.Log ("current intensity is: " + curIntensity);
		}
		if (isMagic) {
//			GameObject bomb = Instantiate (Resources.Load ("bomb"), transform.position, Quaternion.identity) as GameObject;
			GameObject key = Instantiate (Resources.Load ("key"), transform.position, Quaternion.identity) as GameObject;
		}
		// Peak reached. Now decrease intensity (fade out)
		while (curIntensity > 0)
		{
			material.SetColor("_EmissionColor", glowColor * curIntensity);
			curIntensity -= intensityIncrement;
			yield return new WaitForSeconds(0.05f);
//			Debug.Log ("current intensity is: " + curIntensity);
		}
		// Cleanup
		material.DisableKeyword("_EMISSION");
		material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
		material.SetColor("_EmissionColor", Color.black);
	}
}
