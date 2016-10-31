using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Camera camera;
	public AudioSource clockSound;
	public AudioClip keyFoundSound;
	public AudioClip keyNotFoundSound;
	public int remainingTime = 30;
	public Text remainingTimeText;

	private float _currentRotation = 0;
	private IEnumerator _gameTimer;
	private static int _level = 0;
	private Vector3 _rotationCenter = Vector3.zero;
	private float _rotationRemaining = 0;
	private float _rotationResolution = 2.0f; // Degrees per frame

	void Start () 
	{
		remainingTime -= 5 * _level; // Every level has 5 less seconds to find the key
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
		HidingSpot[] hidingSpots = FindObjectsOfType(typeof(HidingSpot)) as HidingSpot[];
		foreach (HidingSpot spot in hidingSpots) {
			AudioSource audioSource = spot.GetComponent<AudioSource>();
			audioSource.clip = keyNotFoundSound;
			audioSource.pitch = Random.Range (0.25f, 1.75f);;
		}
		int winnerSpot = Random.Range (0, hidingSpots.Length);
		// Set the winner
		Debug.Log("winner is " + hidingSpots [winnerSpot].name);
		HidingSpot winner = hidingSpots [winnerSpot];
		winner.hidingSpotHasKey = true;
		AudioSource winnerAudio = winner.GetComponent<AudioSource> ();
		winnerAudio.clip = keyFoundSound;
		winnerAudio.pitch = 1.0f;
	}

	public void levelUp() {
		_level++;
	}

	public void rotateView(float rotation) {
//		float direction = Mathf.Sign (rotation);
		float degrees = rotation * 360f;
		_rotationRemaining += degrees;
		_currentRotation = (_currentRotation + degrees + 360f) % 360.0f; // Adding 360 ensures _currentRotation is never < 0
		Debug.Log ("Camera is now rotated " + _currentRotation + " degrees.");
	}

	public void winGame() {
		Debug.Log ("Key found!");
		StopCoroutine (_gameTimer);
		remainingTimeText.color = Color.green;
		// Rotate back to original view
		float rotationsRemaining = _currentRotation / 360f;
		if (rotationsRemaining > 0.25f) {
			rotateView (1f - rotationsRemaining);
		} else {
			rotateView (-1f * rotationsRemaining);
		}
		// Win scene is loaded from KeyBehavior.cs (via animation event)
	}

	private IEnumerator gameTimer () {
		while (remainingTime >= 0) {
			if (remainingTime < 10) {
				remainingTimeText.color = Color.red;
				clockSound.pitch -= 0.1f;
				remainingTimeText.text = "00:0" + remainingTime;
				if (remainingTime == 0) {
					_level = 0; // Reset the level
					SceneManager.LoadScene("lose");
				}
			} else {
				remainingTimeText.text = "00:" + remainingTime;
			}
			clockSound.Play ();
			yield return new WaitForSeconds (1);
			remainingTime--;
		}
	}

}
