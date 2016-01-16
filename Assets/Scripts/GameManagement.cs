using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {
	enum State { Playing, GameOver, Won };

	private State state;
	private GameObject levelManagerObj;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		this.levelManagerObj = GameObject.Find("LevelManager");
		this.levelManager = this.levelManagerObj.GetComponent<LevelManager>();
		this.state = State.Playing;
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
		this.initializeGame();
	}

	public IEnumerator Won() {
		yield return new WaitForSeconds(3);
		this.levelManager.LoadScene ("EndGameWon");

		// preparing for new game if the player wants to play again
		this.initializeGame();
	}

	private void initializeGame() {
		this.state = State.Playing;
		PlayerController.isActive = true;
		EnemyManagement.isActive = true;
		GateController.openFences = false;
	}

	public void setState(string state) {
		switch (state) {
		case "playing":
			this.state = State.Playing;
			break;
		case "gameover":
			Debug.Log ("setstate");

			this.state = State.GameOver;
			break;
		case "won":
			this.state = State.Won;
			break;
		}
	}

}
