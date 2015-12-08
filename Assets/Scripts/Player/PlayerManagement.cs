using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
    private bool flashlightFound;
    private bool glassesFound;
    private bool gunFound;
    private bool keyFound;
    private bool trackerFound;

    private bool ItemsFound {
        get {
            return this.flashlightFound && this.glassesFound && this.gunFound && this.keyFound && this.trackerFound;
        }
    }

	// Use this for initialization
	public void Start () {
        this.flashlightFound = false;
        this.glassesFound = false;
        this.gunFound = false;
        this.keyFound = false;
        this.trackerFound = false;
}
	
	// Update is called once per frame
	public void Update () {
	
	}

    public void OnTriggerEnter(Collider other) {
        print("hey");
        if (other.tag.Equals("Attribute")) {
            other.gameObject.SetActive(false);

            if (this.ItemsFound) {
                // proceed gameee
            }
        }
    }
}
