using UnityEngine;
using System.Collections;

public class EnemySounds : MonoBehaviour {
    private float countdown;
    private int currentSoundIndex;

    private AudioSource[] sounds;
    private GameObject player;
    private Random random;

    [SerializeField]
    private AudioSource moan;
    [SerializeField]
    private AudioSource growl;
    [SerializeField]
    private AudioSource psyched;
    [SerializeField]
    private float hearableDistance;
    [SerializeField]
    private float hearableChaseDistance;

    private float DistanceToPlayer {
        get {
            return (transform.position - this.player.transform.position).sqrMagnitude;
        }
    }

    // Use this for initialization
    public void Start () {
        this.countdown = Random.Range(3, 5);
        this.currentSoundIndex = 0;
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.random = new Random();
        this.sounds = new AudioSource[3] { moan, growl, psyched };

        this.hearableDistance = (this.hearableDistance == 0) ? 200 : this.hearableDistance;
        this.hearableChaseDistance = (this.hearableChaseDistance == 0) ? 100 : this.hearableChaseDistance;

        Debug.Log(this.hearableDistance);
        Debug.Log(this.hearableChaseDistance);
    }
	
	// Update is called once per frame
	public void Update () {
        this.countdown -= Time.deltaTime;

        if (this.countdown <= 0f) {
            this.switchSound();
            this.playSound();
            this.countdown = Random.Range(3, 5);
        }
    }

    private void switchSound() {
        this.sounds[this.currentSoundIndex].mute = true;

        bool chasing = (this.DistanceToPlayer <= this.hearableChaseDistance);
        int index = Random.Range(chasing ? 1 : 0, chasing ? this.sounds.Length : this.sounds.Length - 1);

        this.sounds[index].mute = false;
        this.currentSoundIndex = index;
    }

    private void playSound() {
        if (this.DistanceToPlayer <= this.hearableDistance) {
            Debug.Log("I played");
            this.sounds[this.currentSoundIndex].Play();
        }
    }
}
