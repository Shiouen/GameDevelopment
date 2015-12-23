using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManagement : MonoBehaviour {
	public Text countDown;
	private float startTime;
	private bool isTimerActive;

	public Image FoundClock;
	public Image FoundCompass;
	public Image FoundFlashlight;
	public Image FoundGun;
	public Image FoundKey;
	private Color32 activeColor;

	public Image bloodLevel1;
	public Image bloodLevel2;
	public Image bloodLevel3;
	public Image bloodLevel4;
	public Image bloodLevel5;
	public Image bloodLevel6;
	public Image bloodLevel7;
	public Image bloodLevel8;
	public Image bloodLevel9;
	public Image bloodLevel10;

	private float health;
	private float maxHealth;

	// Use this for initialization
	void Start () {
		// Countdown
		this.startTime = 10.0f;
		this.isTimerActive = false;
		this.countDown.text = "";
		this.activeColor = new Color32(22,255,123,255);

		/*Rect screen = new Rect (-Screen.width/2, -Screen.height/2, Screen.width, Screen.height);
		bloodLevel1. = screen;
		bloodLevel2.pixelsPerUnit = screen;
		bloodLevel3.pixelsPerUnit = screen;*/
		this.health = 100;
		this.maxHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isTimerActive && this.startTime >= 0) {
			if(this.startTime < 5) this.countDown.color = Color.red;
			this.countDown.text = this.startTime.ToString("F2");
			this.startTime -= Time.deltaTime;
		}

		Color tempLevel1 = bloodLevel1.color;
		Color tempLevel2 = bloodLevel2.color;
		Color tempLevel3 = bloodLevel3.color;
		Color tempLevel4 = bloodLevel4.color;
		Color tempLevel5 = bloodLevel5.color;
		Color tempLevel6 = bloodLevel6.color;
		Color tempLevel7 = bloodLevel7.color;
		Color tempLevel8 = bloodLevel8.color;
		Color tempLevel9 = bloodLevel9.color;
		Color tempLevel10 = bloodLevel10.color;
		tempLevel1.a = bloodLevel(100);
		tempLevel2.a = bloodLevel(90);
		tempLevel3.a = bloodLevel(80);
		tempLevel4.a = bloodLevel(70);
		tempLevel5.a = bloodLevel(60);
		tempLevel6.a = bloodLevel(50);
		tempLevel7.a = bloodLevel(40);
		tempLevel8.a = bloodLevel(30);
		tempLevel9.a = bloodLevel(20);
		tempLevel10.a = bloodLevel(10);
		bloodLevel1.color = tempLevel1;
		bloodLevel2.color = tempLevel2;
		bloodLevel3.color = tempLevel3;
		bloodLevel4.color = tempLevel4;
		bloodLevel5.color = tempLevel5;
		bloodLevel6.color = tempLevel6;
		bloodLevel7.color = tempLevel7;
		bloodLevel8.color = tempLevel8;
		bloodLevel9.color = tempLevel9;
		bloodLevel10.color = tempLevel10;
	}

	public void makeActive(string attribute) {
		switch (attribute) {
		case "clock":
			this.FoundClock.color = this.activeColor;
			break;
		case "key":
			this.FoundKey.color = this.activeColor;
			break;
		case "gun":
			this.FoundGun.color = this.activeColor;
			break;
		case "compass":
			this.FoundCompass.color = this.activeColor;
			break;
		case "flashlight":
			this.FoundFlashlight.color = this.activeColor;
			break;
		}
	}

	public void startTimer() {
		this.isTimerActive = true;
	}

	public void setHealth(float health) {
		this.health = health;
	}

	private float bloodLevel(float start) {
		if (this.health < start) {
			return 1;
		}
		//float alpha = 1 - (100 * this.health) / (start * this.maxHealth);
		//Debug.Log (alpha);
		return 0;
	}
}