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
	public int levelIncrement = 5;
	public Text remainingTime;

	private static int _level = 0;
	private Vector3 _rotationCenter = Vector3.zero;
	private float _currentRotation = 0;
	private float _rotationAmount = 90f;
	private float _rotationRemaining = 0;
	private float _rotationResolution;
	private bool _gameWon = false;
	private IEnumerator _gameTimer;

	void Start () 
	{
		_rotationResolution = _rotationAmount / 60f; // Divide by frame rate (so rotation takes 1 second)
		levelTime -= levelIncrement * _level;
		HideKey ();
		_gameTimer = countdownTimer ();
		StartCoroutine (_gameTimer);
	}

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
		while (levelTime >= 0 && !_gameWon) {
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
		_currentRotation = (_currentRotation + _rotationAmount) % 360.0f;
		Debug.Log ("Camera is now rotated " + _currentRotation + " degrees.");
	}

	public void winGame() {
		Debug.Log ("game is won!");
		StopCoroutine (_gameTimer);
		remainingTime.color = Color.green;
		int rotationsRemaining = (4 - (int) (_currentRotation / _rotationAmount)) % 4;
		while (rotationsRemaining != 0) {
			rotateView ();
			rotationsRemaining--;
		}
		GameObject door = GameObject.FindWithTag ("Door");
		door.GetComponent<Animation>().Play ();
//		Invoke("loadWin", 3f);
	}
		
	private void loadWin() {
		levelUp ();
		SceneManager.LoadScene ("win");
	}
}
