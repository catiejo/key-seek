using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameButton : MonoBehaviour {
	public void LoadGame()  {
		SceneManager.LoadScene("game");
	}
	public void LoadMenu()  {
		SceneManager.LoadScene("start");
	}	
	public void LoadCredits()  {
		SceneManager.LoadScene("credits");
	}
}
