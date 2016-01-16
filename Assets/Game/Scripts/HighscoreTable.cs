using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreTable : MonoBehaviour {
	public Text hs1;
	public Text hs2;
	public Text hs3;
	public Text hs4;
	public Text hs5;

	// Use this for initialization
	void Start () {
		float highscore = PlayerPrefs.GetFloat ("Highscores");
		hs1.text = "Melissa                      " + highscore.ToString();
	}

	// Update is called once per frame
	void Update () {

	}
}
