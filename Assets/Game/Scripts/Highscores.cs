using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Highscores : MonoBehaviour {
	public Text score;
	public InputField playerName;

	// Use this for initialization
	void Start () {
		this.score.text = "Score = "+PlayerPrefs.GetFloat ("CurrentScore").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addScore() {
		PlayerPrefs.SetFloat (this.playerName.text, PlayerPrefs.GetFloat ("CurrentScore"));
		Application.LoadLevel ("Highscores");
	}
}