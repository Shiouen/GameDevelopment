using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
	enum State { standing, walking, running, death };

	private bool flashlightFound;
	private bool gunFound;
	private bool keyFound;
	private bool compassFound;
	private bool clockFound;
	private float health;
	private State state;
	private Animator animator;
	private HUDManagement HUD;

	private bool ItemsFound {
		get {
			return this.flashlightFound || this.clockFound || this.gunFound || this.keyFound || this.compassFound;
		}
	}
	
	// Use this for initialization
	public void Start () {
		this.HUD = GetComponent<HUDManagement> ();
		this.animator = GetComponent<Animator> ();

		// Attributes initially not found
		this.flashlightFound = false;
		this.clockFound = false;
		this.gunFound = false;
		this.keyFound = false;
		this.compassFound = false;

		// Initial health
		this.health = 100;

		// Initial state
		this.state = State.standing;
	}
	
	// Update is called once per frame
	public void Update () {
		this.animator.SetBool ("Standing", false);
		this.animator.SetBool ("Walk", false);
		this.animator.SetBool ("Running", false);
		if (this.health == 0) this.state = State.death;

		switch (this.state) {
		case State.standing:
			this.animator.SetBool ("Standing", true);
			break;
		case State.walking:
			this.animator.speed = 2.0f;
			this.animator.SetBool ("Walk", true);
			break;
		case State.running:
			this.animator.speed = 3.0f;
			this.animator.SetBool ("Running", true);
			break;
		case State.death:
			this.animator.SetBool ("Death", true);
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
				this.HUD.startTimer();
			}
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
		case "death":
			this.state = State.death;
			break;
		}
	}
}

