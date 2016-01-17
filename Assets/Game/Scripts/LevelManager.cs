using UnityEngine;

public class LevelManager : MonoBehaviour {
	public void LoadScene(string name) {
		Application.LoadLevel (name);
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
