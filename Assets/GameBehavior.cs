using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour {
	public Camera camera;
	public AudioSource clockSound;
	public AudioClip rightSound;
	public AudioClip wrongSound;
	public int levelTime = 30;
	public int levelIncrement = 5;
	public Text remainingTime;

	private static int _level = 0;
	private Vector3 _rotationCenter = Vector3.zero;
	private float _currentRotation = 0;
	private float _rotationRemaining = 0;
	private float _rotationResolution = 2.0f; // Degrees per frame
	private bool _gameWon = false;
	private IEnumerator _gameTimer;

	void Start () 
	{
		levelTime -= levelIncrement * _level;
		HideKey ();
		_gameTimer = gameTimer ();
		StartCoroutine (_gameTimer);
	}

	void Update () {
		if (Mathf.Abs(_rotationRemaining) > Mathf.Epsilon) {
			float rotationAmount = Mathf.Sign (_rotationRemaining) * _rotationResolution; // direction * amount
			camera.transform.RotateAround (_rotationCenter, Vector3.up, rotationAmount);
			_rotationRemaining -= rotationAmount;
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

	private IEnumerator gameTimer () {
		while (levelTime >= 0) {
			if (levelTime < 10) {
				remainingTime.color = Color.red;
				clockSound.pitch -= 0.1f;
				remainingTime.text = "00:0" + levelTime;
				if (levelTime == 0) {
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

	public void rotateView(float rotation) {
//		float direction = Mathf.Sign (rotation);
		float degrees = rotation * 360f;
		_rotationRemaining += degrees;
		_currentRotation = (_currentRotation + degrees + 360f) % 360.0f;
		Debug.Log ("Camera is now rotated " + _currentRotation + " degrees.");
	}

	public void winGame() {
		Debug.Log ("Key found!");
		StopCoroutine (_gameTimer);
		remainingTime.color = Color.green;
		float rotationsRemaining = _currentRotation / 360f;
		if (rotationsRemaining > 0.25f) {
			rotateView (1f - rotationsRemaining);
		} else {
			rotateView (-1f * rotationsRemaining);
		}
		// Win scene is loaded from KeyBehavior.cs (via animation event)
	}
}
