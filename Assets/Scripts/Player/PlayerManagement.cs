using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
	private bool flashlightFound;
	private bool gunFound;
	private bool keyFound;
	private bool compassFound;
	private bool clockFound;
	private float health;
	private HUDManagement HUD;

	private bool ItemsFound {
		get {
			return this.flashlightFound || this.clockFound || this.gunFound || this.keyFound || this.compassFound;
		}
	}
	
	// Use this for initialization
	public void Start () {
		this.HUD = GetComponent<HUDManagement> ();
		this.flashlightFound = false;
		this.clockFound = false;
		this.gunFound = false;
		this.keyFound = false;
		this.compassFound = false;
		this.health = 100;
	}
	
	// Update is called once per frame
	public void Update () {
		
	}
	
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

			if (this.ItemsFound) {
				this.HUD.startTimer();
			}
		}
	}

	public float getHealth() {
		return this.health;
	}

	public void makeDamage() {
		this.health -= 0.5f;
		this.HUD.setHealth (this.health);
	}

	public void repairDamage() {
		if (this.health < 100) {
			this.health += 0.5f;
			this.HUD.setHealth (this.health);
		}
	}
}

