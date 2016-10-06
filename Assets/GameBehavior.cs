using UnityEngine;
using System.Collections;

public class GameBehavior : MonoBehaviour {
	public AudioClip wrongSound;
	public AudioClip rightSound;

	// Use this for initialization
	void Start () 
	{
		HideKey ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void HideKey () {
		SmashObject[] hidingSpots = FindObjectsOfType(typeof(SmashObject)) as SmashObject[];
		foreach (SmashObject spot in hidingSpots) {
			AudioSource audioSource = spot.GetComponent<AudioSource>();
			audioSource.clip = wrongSound;
			audioSource.pitch = Random.Range (0.25f, 1.75f);;
		}
		int hidingSpot = Random.Range (0, hidingSpots.Length);
		Debug.Log("spot is " + hidingSpots [hidingSpot].name);
		AudioSource winner = hidingSpots [hidingSpot].GetComponent<AudioSource> ();
		hidingSpots [hidingSpot].isMagic = true;
		Debug.Log(hidingSpots [hidingSpot].name + " is magic? " + hidingSpots [hidingSpot].isMagic);
		winner.clip = rightSound;
		winner.pitch = 1.0f;
	}

	void StartTimer () {
		
	}
		
}
