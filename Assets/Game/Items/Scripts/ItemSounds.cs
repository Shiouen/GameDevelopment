using UnityEngine;

public class ItemSounds : MonoBehaviour {
    private AudioSource pickupSound;

    public void Start() {
        this.pickupSound = this.GetComponent<AudioSource>();
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            this.Play();
        }
    }

    public void OnTriggerEnter(Collider other) {
        this.Play();
    }

    public void Play() {
        this.pickupSound.Play();
    }
}
