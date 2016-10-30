using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyBehavior : MonoBehaviour {
	private GameBehavior _controller;

	// Use this for initialization
	void Start () {
		//Find the game controller object in the scene
		GameObject controllerObject = GameObject.FindWithTag ("GameController");
		if (controllerObject != null) {
			_controller = controllerObject.GetComponent<GameBehavior> ();
		} else {
			Debug.Log ("Key cannot find game controller.");
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadNextLevel() {
		_controller.levelUp ();
		Invoke("loadWinScene", 3f);

	}

	private void loadWinScene() {
		SceneManager.LoadScene ("win");
	}
		
}
