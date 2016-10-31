using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SmashObject : MonoBehaviour {
	public AudioSource soundEffect;
	public bool hidingSpotHasKey;

	private GameBehavior _controller;

	void Start () 
	{
		//Find the game controller object in the scene
		GameObject controllerObject = GameObject.FindWithTag ("GameController");
		if (controllerObject != null) {
			_controller = controllerObject.GetComponent<GameBehavior> ();
			soundEffect = gameObject.GetComponent<AudioSource>();
		} else {
			Debug.Log ("This HidingSpot cannot find the game controller.");
		}
	}

	void OnMouseDown () {
		StartCoroutine (checkHidingSpot ());
	}

	private IEnumerator checkHidingSpot()
	{
		float curIntensity = 0.0f;
		Color glowColor = Color.red;
		float glowIntensity = 0.3f;
		float glowIntensityIncrement = 0.03f;
		Material material = gameObject.GetComponent<Renderer>().material;

		if (hidingSpotHasKey) {
			_controller.winGame ();
			glowColor = Color.green;
		}
		soundEffect.Play ();
		// Setup
		material.EnableKeyword("_EMISSION");
		material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
		// Increase intensity (fade in)
		while (curIntensity < glowIntensity)
		{
			material.SetColor("_EmissionColor", glowColor * curIntensity);
			curIntensity += glowIntensityIncrement;
			yield return new WaitForSeconds(0.05f);
		}
		// Peak reached. Reveal the key and decrease intensity (fade out)
		if (hidingSpotHasKey) {
			// Key has an animation that opens the door and loads the win screen
			GameObject key = Instantiate (Resources.Load ("key"), transform.position, Quaternion.identity) as GameObject;
		}
		while (curIntensity > 0)
		{
			material.SetColor("_EmissionColor", glowColor * curIntensity);
			curIntensity -= glowIntensityIncrement;
			yield return new WaitForSeconds(0.05f);
		}
		// Cleanup
		material.DisableKeyword("_EMISSION");
		material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
		material.SetColor("_EmissionColor", Color.black);
	}
}
