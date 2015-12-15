using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
	private bool flashlightFound;
	private bool gunFound;
	private bool keyFound;
	private bool compassFound;
	private bool clockFound;
	private GameObject FoundClock;
	private GameObject FoundCompass;
	private GameObject FoundFlashlight;
	private GameObject FoundGun;
	private GameObject FoundKey;
	
	private bool ItemsFound {
		get {
			return this.flashlightFound && this.clockFound && this.gunFound && this.keyFound && this.compassFound;
		}
	}
	
	// Use this for initialization
	public void Start () {
		this.flashlightFound = false;
		this.clockFound = false;
		this.gunFound = false;
		this.keyFound = false;
		this.compassFound = false;
		this.FoundClock = GameObject.Find ("FoundClock");
		this.FoundCompass = GameObject.Find ("FoundCompass");
		this.FoundFlashlight = GameObject.Find ("FoundFlashlight");
		this.FoundGun = GameObject.Find ("FoundGun");
		this.FoundKey = GameObject.Find ("FoundKey");
		this.FoundClock.SetActive (false);
		this.FoundCompass.SetActive (false);
		this.FoundFlashlight.SetActive (false);
		this.FoundGun.SetActive (false);
		this.FoundKey.SetActive (false);
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
				this.FoundClock.SetActive(true);
				break;
			case "Compass(Clone)":
				this.compassFound = true;
				this.FoundCompass.SetActive(true);
				break;
			case "Flashlight(Clone)":
				this.flashlightFound = true;
				this.FoundFlashlight.SetActive(true);
				break;
			case "Gun(Clone)":
				this.gunFound = true;
				this.FoundGun.SetActive(true);
				break;
			case "Key(Clone)":
				this.keyFound = true;
				this.FoundKey.SetActive(true);
				break;
			}

			if (this.ItemsFound) {
				// proceed gameee
			}
		}
	}
}

