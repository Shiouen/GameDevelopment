using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreTable : MonoBehaviour {
	public Text hs1;
	public Text hs2;
	public Text hs3;
	public Text hs4;
	public Text hs5;

	private Text[] highscoreTexts = new Text[5];

	// Use this for initialization
	void Start () {
		highscoreTexts [0] = hs1;
		highscoreTexts [1] = hs2;
		highscoreTexts [2] = hs3;
		highscoreTexts [3] = hs4;
		highscoreTexts [4] = hs5;

		for(int i = 0; i < 5; i++) {
			if (PlayerPrefs.HasKey ("HS" + i)) {
				highscoreTexts[i].text = string.Format("{0,-15} {1,15}", PlayerPrefs.GetString ("HSName" + i).ToString(), PlayerPrefs.GetFloat("HS" + i).ToString("F2"));
			} else {
				highscoreTexts[i].text = "";
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
