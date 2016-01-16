using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
	enum State { standing, walking, running, attacked, death, won };

	private bool flashlightFound;
	private bool gunFound;
	private bool keyFound;
	private bool compassFound;
	private bool clockFound;
	private float health;
	private float score;
	private State state;
	private Animator animator;
	private HUDManagement HUD;
	private GameObject gameManagementObj;
	private GameManagement gameManagement;

	private bool ItemsFound {
		get {
			return this.flashlightFound || this.clockFound || this.gunFound || this.keyFound || this.compassFound;
		}
	}
	
	// Use this for initialization
	public void Start () {
		this.HUD = GetComponent<HUDManagement> ();
		this.gameManagementObj = GameObject.Find("GameController");
		this.gameManagement = this.gameManagementObj.GetComponent<GameManagement>();
		this.animator = GetComponent<Animator> ();

		// Attributes initially not found
		this.flashlightFound = false;
		this.clockFound = false;
		this.gunFound = false;
		this.keyFound = false;
		this.compassFound = false;

		// Initial health & score
		this.health = 100;
		this.score = 0;

		// Initial state
		this.state = State.standing;
	}
	
	// Update is called once per frame
	public void Update () {
		this.animator.SetBool ("Standing", false);
		this.animator.SetBool ("Walking", false);
		this.animator.SetBool ("Running", false);
		this.animator.SetBool ("Attacked", false);
		if (this.health == 0) this.state = State.death;

		switch (this.state) {
		    case State.standing:
			    this.animator.SetBool ("Standing", true);
			    break;
		    case State.walking:
			    this.animator.speed = 2.0f;
			    this.animator.SetBool ("Walking", true);
			    break;
		    case State.running:
			    this.animator.speed = 2.0f;
			    this.animator.SetBool ("Running", true);
			    break;
		    case State.attacked:
			    this.animator.SetBool ("Attacked", true);
			    break;
			case State.death:
				this.animator.SetBool ("Death", true);
				this.HUD.showGameOverHUD ();
				this.gameManagement.setState ("gameover");
			    break;
			case State.won:
				this.HUD.showWonHUD ();
				this.HUD.stopTimer ();
				this.gameManagement.setScore (this.score);
				this.gameManagement.setState ("won");
				break;
		}
	}

	// Registers when the player catches an attribute
	public void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Attribute")) {
			other.gameObject.SetActive(false);
			switch(other.gameObject.name) {
			case "Clock(Clone)":
				this.clockFound = true;
				this.HUD.makeActive ("clock");
				break;
			case "Compass(Clone)":
				this.compassFound = true;
				this.HUD.makeActive ("compass");
				break;
			case "Flashlight(Clone)":
				this.flashlightFound = true;
				this.HUD.makeActive ("flashlight");
				break;
			case "Gun(Clone)":
				this.gunFound = true;
				this.HUD.makeActive ("gun");
				break;
			case "Key(Clone)":
				this.keyFound = true;
				this.HUD.makeActive ("key");
				break;
			}

			// When all attributes are found, start HUD timer for next phase of the game
			if (this.ItemsFound) {
				StartCoroutine(this.HUD.startTimer());
			}
		} else if(other.tag.Equals("WinningAttribute")) {
			other.gameObject.SetActive(false);
			this.setState("won");
		}
	}

	public float getHealth() {
		return this.health;
	}

	public void makeDamage() {
		if (this.health <= 0) {
			this.health = 0;
		} else {
			this.health -= 0.5f;
		}
		this.HUD.setHealth (this.health);
	}

	public void repairDamage() {
		if (this.health < 100) {
			this.health += 0.5f;
			this.HUD.setHealth (this.health);
		}
	}

	public void setState(string state) {
		switch (state) {
		case "standing":
			this.state = State.standing;
			break;
		case "walking":
			this.state = State.walking;
			break;
		case "running":
			this.state = State.running;
			break;
		case "attacked":
			this.state = State.attacked;
			break;
		case "death":
			this.state = State.death;
			break;
		case "won":
			this.state = State.won;
			break;
		}
	}

	public void moveMinimapPlayer(Vector3 speed, Vector3 previousPosition) {
		this.HUD.moveMinimapPlayer (speed, previousPosition);
	}

	public void setScore(float score) {
		this.score = score;
	}
}

