using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscores : MonoBehaviour {
	public Text score;
	public InputField playerName;

	// Use this for initialization
	void Start () {
		this.score.text = "Score = "+PlayerPrefs.GetFloat ("CurrentScore").ToString("F2");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void addScore() {
		float oldScore = 0;
		string oldPlayerName = "";
		float newScore = PlayerPrefs.GetFloat ("CurrentScore");
		string newPlayerName = this.playerName.text;
		if (newPlayerName.Length == 0) {
			newPlayerName = "Unknown";
		}

		for (int i = 0; i < 10; i++) {
			if (PlayerPrefs.HasKey ("HS" + i)) {
				if (PlayerPrefs.GetFloat ("HS" + i) < newScore) {
					oldScore = PlayerPrefs.GetFloat ("HS" + i);
					oldPlayerName = PlayerPrefs.GetString ("HSName" + i);
					PlayerPrefs.SetFloat ("HS" + i, newScore);
					PlayerPrefs.SetString ("HSName" + i, newPlayerName);
					newScore = oldScore;
					newPlayerName = oldPlayerName;
				}
			} else {
				PlayerPrefs.SetFloat ("HS" + i, newScore);
				PlayerPrefs.SetString ("HSName" + i, newPlayerName);
				newScore = 0;
				newPlayerName = "";
				break;
			}
		}

		Application.LoadLevel ("Highscores");
	}
}