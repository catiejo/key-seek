using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button : MonoBehaviour {
	public void LoadStage()  {
		SceneManager.LoadScene("game");
	}
}
