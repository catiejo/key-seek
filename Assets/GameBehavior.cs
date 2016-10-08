using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour {
	public AudioSource clockSound;
	public AudioSource bombSound;
	public AudioClip rightSound;
	public AudioClip wrongSound;
	public int levelTime = 30;
	private static int _level = 0;
	public Text remainingTime;

	// Use this for initialization
	void Start () 
	{
		levelTime -= 3 * _level;
		HideKey ();
		StartCoroutine (countdownTimer ());
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

	private IEnumerator countdownTimer () {
		while (levelTime >= 0) {
			if (levelTime < 10) {
				remainingTime.color = Color.red;
				remainingTime.text = "00:0" + levelTime;
				clockSound.pitch -= 0.1f;
				if (levelTime == 0) {
					bombSound.Play ();
					SceneManager.LoadScene("lose");
				}
			} else {
				remainingTime.text = "00:" + levelTime;
			}
			clockSound.Play ();
			yield return new WaitForSeconds (1);
			levelTime--;
		}
	}

	public void levelUp() {
		_level++;
	}

	public void levelReset() {
		_level = 0;
	}
		
}
