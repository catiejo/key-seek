using UnityEngine;
using System.Collections;

public class SmashObject : MonoBehaviour {
	public AudioSource smashSound;
	public bool isMagic;
	bool wasClicked;
	private GameBehavior _controller;
	Color glowColor = Color.red;
	float glowIntensity = 0.3f;
	float intensityIncrement = 0.03f;

	// Use this for initialization
	void Start () 
	{
//		//Find the game controller object in the scene
//		GameObject controllerObject = GameObject.FindWithTag ("GameController");
//		if (controllerObject != null) {
//			_controller = controllerObject.GetComponent<GameBehavior> ();
//			smashSound = gameObject.GetComponent<AudioSource>();
//		} else {
//			Debug.Log ("SmashObject cannot find game controller.");
//		}
	}

	void OnMouseDown () {
//		wasClicked = true;
//		setHighlight (wasClicked, 0.1f);
		StartCoroutine (glowObject ());
		smashSound.Play ();
		Debug.Log ("ouch!");

	}
	
	// Update is called once per frame
	void Update () {
//		if (wasClicked) {
//			Debug.Log (wasClicked);
//			Renderer renderer = gameObject.GetComponent<Renderer>();
//			float intensity = 0.0f;
//			while (intensity < 0.5f) {
//			DynamicGI.SetEmissive (renderer, new Color (1f, 0.1f, 0.5f, 1.0f));
//				intensity += 0.05f;
//			}
//			wasClicked = false;
//			Debug.Log (wasClicked);
//		}
	}

	public void setHighlight(bool highlight, float intensity)
	{
		Material material = gameObject.GetComponent<Renderer>().material;
		if (highlight)
		{
			material.EnableKeyword("_EMISSION");
			material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
			material.SetColor("_EmissionColor", Color.green * intensity);
		}
		else
		{
			material.DisableKeyword("_EMISSION");
			material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
			material.SetColor("_EmissionColor", Color.black);
		}
	}

	private IEnumerator glowObject()
	{
		Material material = gameObject.GetComponent<Renderer>().material;
		float curIntensity = 0.0f;
		if (isMagic) {
			glowColor = Color.green;
		}
		material.EnableKeyword("_EMISSION");
		material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
		while (curIntensity < glowIntensity)
		{
			material.SetColor("_EmissionColor", glowColor * curIntensity);
			curIntensity += intensityIncrement;
			yield return new WaitForSeconds(0.05f);
//			Debug.Log ("current intensity is: " + curIntensity);
		}
		while (curIntensity > 0)
		{
			material.SetColor("_EmissionColor", glowColor * curIntensity);
			curIntensity -= intensityIncrement;
			yield return new WaitForSeconds(0.05f);
//			Debug.Log ("current intensity is: " + curIntensity);
		}
		material.DisableKeyword("_EMISSION");
		material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
		material.SetColor("_EmissionColor", Color.black);
		yield return new WaitForSeconds(0.0f);
	}
}
