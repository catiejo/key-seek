using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyBehavior : MonoBehaviour {
	private GameBehavior _controller;

	void Start () {
		//Find the game controller object in the scene
		GameObject controllerObject = GameObject.FindWithTag ("GameController");
		if (controllerObject != null) {
			_controller = controllerObject.GetComponent<GameBehavior> ();
		} else {
			Debug.Log ("Key cannot find game controller.");
		}	
	}

	public void loadNextLevel() {
		_controller.levelUp ();
		Invoke("_loadWinScene", 3f); // Invoke can't be passed with arguments, so I made a wrapper function loadWinScene
	}

	private void _loadWinScene() {
		SceneManager.LoadScene ("win");
	}

	private void openDoor() {
		GameObject door = GameObject.FindWithTag ("Door");
		door.GetComponent<Animation> ().Play ();
	}
		
}
