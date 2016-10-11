using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameButton : MonoBehaviour {
	public void LoadStage()  {
		SceneManager.LoadScene("game");
	}
}
