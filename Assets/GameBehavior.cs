using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour {
	public Camera camera;
	public AudioSource clockSound;
	public AudioSource bombSound;
	public AudioClip rightSound;
	public AudioClip wrongSound;
	public int levelTime = 30;
	private static int _level = 0;
	public Text remainingTime;
	private float _rotationAmount = 90f;
	private float _rotationRemaining = 90f;
	private Vector3 _rotationCenter = Vector3.zero;
	private float _rotationResolution;

	// Use this for initialization
	void Start () 
	{
		_rotationResolution = _rotationAmount / 60f; //60 frames per second
		levelTime -= 5 * _level;
		HideKey ();
		StartCoroutine (countdownTimer ());
	}

	// Update is called once per frame
	void Update () {
		if (_rotationRemaining > 0) {
			camera.transform.RotateAround (_rotationCenter, Vector3.up, _rotationResolution);
			_rotationRemaining -= _rotationResolution;
		}
	
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

	public void rotateView() {
		_rotationRemaining += _rotationAmount;
	}
		
}
