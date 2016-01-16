using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManagement : MonoBehaviour {
	private float MAX_TIME = 1000.0f;

	public Text countDown;
	public Text endGame;
	private float startTime;
	private bool isTimerActive;

	public Image FoundClock;
	public Image Clock;
	public Image FoundCompass;
	public Image Compass;
	public Image FoundFlashlight;
	public Image Flashlight;
	public Image FoundGun;
	public Image Gun;
	public Image FoundKey;
	public Image Key;
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

	public Image minimap;
	public Image minimapplayer; 

	private float health;
	private float maxHealth;
	private PlayerManagement playerManagement;

	// Use this for initialization
	void Start () {
		// Countdown
		this.startTime = MAX_TIME;
		this.isTimerActive = false;
		this.countDown.text = "";
		this.activeColor = new Color32(22,255,123,255);

		Color tempEndGame = this.endGame.color;
		tempEndGame.a = 0;
		this.endGame.color = tempEndGame;

		float x = Screen.width-(this.minimap.rectTransform.sizeDelta.x/2);
		float y = this.minimap.rectTransform.sizeDelta.y/2;
		this.minimap.transform.position = new Vector3(x,y,0);
		this.minimapplayer.transform.position = new Vector3 (Screen.width-33,353,0);

		this.health = 100;
		this.maxHealth = 100;

		this.playerManagement = GetComponent<PlayerManagement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isTimerActive && this.startTime >= 0) {
			if (this.startTime < 5) { this.countDown.color = Color.red; }
			this.countDown.text = this.startTime.ToString ("F2");
			this.startTime -= Time.deltaTime;
			this.playerManagement.setScore (MAX_TIME-startTime);
		} else if(this.startTime < 0) {
			this.playerManagement.setState ("death");
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

	public IEnumerator startTimer() {
		GateController.openFences = true;
		this.disableHUD ();
		yield return new WaitForSeconds(4);
		this.enableHUD ();
		this.isTimerActive = true;
	}

	public void disableHUD() {
		this.minimap.enabled = false;
		this.minimapplayer.enabled = false;
		this.FoundClock.enabled = false;
		this.Clock.enabled = false;
		this.FoundGun.enabled = false;
		this.Gun.enabled = false;
		this.FoundKey.enabled = false;
		this.Key.enabled = false;
		this.FoundCompass.enabled = false;
		this.Compass.enabled = false;
		this.FoundFlashlight.enabled = false;
		this.Flashlight.enabled = false;
		this.bloodLevel1.enabled = false;
		this.bloodLevel2.enabled = false;
		this.bloodLevel3.enabled = false;
		this.bloodLevel4.enabled = false;
		this.bloodLevel5.enabled = false;
		this.bloodLevel6.enabled = false;
		this.bloodLevel7.enabled = false;
		this.bloodLevel8.enabled = false;
		this.bloodLevel9.enabled = false;
		this.bloodLevel10.enabled = false;

		// Enemy and player aren't active for a time
		EnemyManagement.isActive = false;
		PlayerController.isActive = false;
	}

	public void enableHUD() {
		this.minimap.enabled = true;
		this.minimapplayer.enabled = true;
		this.FoundClock.enabled = true;
		this.Clock.enabled = true;
		this.FoundGun.enabled = true;
		this.Gun.enabled = true;
		this.FoundKey.enabled = true;
		this.Key.enabled = true;
		this.FoundCompass.enabled = true;
		this.Compass.enabled = true;
		this.FoundFlashlight.enabled = true;
		this.Flashlight.enabled = true;
		this.bloodLevel1.enabled = true;
		this.bloodLevel2.enabled = true;
		this.bloodLevel3.enabled = true;
		this.bloodLevel4.enabled = true;
		this.bloodLevel5.enabled = true;
		this.bloodLevel6.enabled = true;
		this.bloodLevel7.enabled = true;
		this.bloodLevel8.enabled = true;
		this.bloodLevel9.enabled = true;
		this.bloodLevel10.enabled = true;

		// Enemy and player can resume their activities
		EnemyManagement.isActive = true;
		PlayerController.isActive = true;
	}

	public void setHealth(float health) {
		this.health = health;
	}

	private float bloodLevel(float start) {
		if (this.health < start) {
			return 1;
		}
		return 0;
	}

	public void moveMinimapPlayer(Vector3 speed, Vector3 previousPosition) {
		if (previousPosition.x - transform.localPosition.x > 0.01 || transform.localPosition.x - previousPosition.x > 0.01
			|| previousPosition.z - transform.localPosition.z > 0.01 || transform.localPosition.z - previousPosition.z > 0.01) {
			// calculation for minimap
			speed.y = speed.z;
			this.minimapplayer.transform.position = this.minimapplayer.transform.position + speed / 88;
		}
	}

	public void showGameOverHUD() {
		if (this.endGame.color.a < 1) {
			Color tempEndGame = this.endGame.color;
			tempEndGame.a += 0.01f;
			this.endGame.color = tempEndGame;
		}
	}

	public void showWonHUD() {
		this.endGame.text = "YOU WON";
		if (this.endGame.color.a < 1) {
			Color tempEndGame = this.endGame.color;
			tempEndGame.a += 0.01f;
			this.endGame.color = tempEndGame;
		}
	}
}