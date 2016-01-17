using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {
	enum State { Playing, GameOver, Won };
	public AudioSource backgroundSound;
	public AudioSource gameOverSound;

	private float score;
	private State state;
	private GameObject levelManagerObj;
	private LevelManager levelManager;
	private GameObject HUDObj;
	private HUDManagement HUD;

	// Use this for initialization
	void Start () {
		this.levelManager = this.GetComponent<LevelManager>();
		this.HUDObj = GameObject.Find("Player");
		this.HUD = this.HUDObj.GetComponent<HUDManagement>();

		this.state = State.Playing;
		this.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (this.state) {
			case State.Playing:
				break;
			case State.GameOver:
				EnemyManagement.isActive = false;
				PlayerController.isActive = false;
				StartCoroutine(this.EndGameOver());
				break;
			case State.Won:
				EnemyManagement.isActive = false;
				PlayerController.isActive = false;
				StartCoroutine (this.Won());
				break;
		}
	}

	public IEnumerator EndGameOver() {
		this.backgroundSound.Stop ();
		if(!this.gameOverSound.isPlaying) {
			this.gameOverSound.Play();
		}
        yield return new WaitForSeconds(2);
		this.levelManager.LoadScene ("EndGameOver");
	}

	public IEnumerator Won() {
		this.backgroundSound.Stop ();
		PlayerPrefs.SetFloat ("CurrentScore",this.score);
		PlayerPrefs.Save ();
		yield return new WaitForSeconds(3);
		this.levelManager.LoadScene ("EndGameWon");
	}
		
	public void setState(string state) {
		switch (state) {
		case "playing":
			this.state = State.Playing;
			break;
		case "gameover":
			this.state = State.GameOver;
			break;
		case "won":
			this.state = State.Won;
			break;
		}
	}

	public void setScore(float score) {
		this.score = score;
	}

	public void startGame() {
		this.HUD.hideStartInfo();
		PlayerController.isActive = true;
		EnemyManagement.isActive = true;
	}
}
