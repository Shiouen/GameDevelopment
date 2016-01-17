using UnityEngine;

public class ItemSounds : MonoBehaviour {
	public AudioSource pickupSound;
	public AudioSource winningSound;

    public void Start() {
    }

    public void Update() {
    }

    public void PlayPickupSound() {
        this.pickupSound.Play();
    }

	public void PlayWinningSound() {
		this.winningSound.Play();
	}
}
