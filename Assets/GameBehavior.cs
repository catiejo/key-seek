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
		int winnerSpot = Random.Range (0, hidingSpots.Length);
		// Set the winner
		Debug.Log("winner is " + hidingSpots [winnerSpot].name);
		SmashObject winner = hidingSpots [winnerSpot];
		winner.isMagic = true;
		AudioSource winnerAudio = winner.GetComponent<AudioSource> ();
		winnerAudio.clip = rightSound;
		winnerAudio.pitch = 1.0f;
	}

	void StartTimer () {
		
	}
		
}
