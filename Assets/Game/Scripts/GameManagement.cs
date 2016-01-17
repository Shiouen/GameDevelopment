using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {
	enum State { Playing, GameOver, Won };

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
				Debug.Log ("update");
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
		yield return new WaitForSeconds(3);
		this.levelManager.LoadScene ("EndGameOver");
		// preparing for new game if the player wants to play again
	}

	public IEnumerator Won() {
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
