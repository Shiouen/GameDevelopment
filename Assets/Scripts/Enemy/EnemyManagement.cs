using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]
public class EnemyManagement : MonoBehaviour {
	enum State { walking, running, attacking };

	private State state;
	private float animatorSpeed = 1.0f;
	private bool isAttacking = false;

	private GameObject player;
	private PlayerController playerController;
	private Animator animator;
	private NavMeshAgent zombie;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator> ();
		this.animator.speed = this.animatorSpeed;

		this.player = GameObject.FindGameObjectWithTag("Player");
		this.playerController = this.player.GetComponent<PlayerController>();
		this.zombie = GetComponent<NavMeshAgent>();

		// Initial state
		this.state = State.walking;
	}

	// Update is called once per frame
	void Update () {
		this.animator.SetBool ("Walking", false);
		this.animator.SetBool ("Running", false);
		this.animator.SetBool ("Attacking", false);

		// update location of the player, 'the target' for the zombie
		this.zombie.SetDestination (this.player.transform.position);

		switch (this.state) {
		    case State.walking:
			    this.animator.speed = 1.9f;
			    this.animator.SetBool ("Walking", true);
			    this.zombie.Resume ();
			    this.zombie.speed = 3.0f;

			    // Let player know that he's not under attack if he was
			    if (this.isAttacking) {
				    this.isAttacking = false;
				    this.playerController.setAttacked (false);
			    }
			    break;
		    case State.running:
			    this.animator.speed = 3.0f;
			    this.animator.SetBool ("Running", true);
			    this.zombie.Resume ();
			    this.zombie.speed = 6.0f;

			    // Let player know that he's not under attack if he was
			    if (this.isAttacking) {
				    this.isAttacking = false;
				    this.playerController.setAttacked (false);
			    }
			    break;
		    case State.attacking:
			    this.animator.speed = 1.5f;
			    this.animator.SetBool ("Attacking", true);
			    this.zombie.Stop ();

			    this.isAttacking = true;
			    this.playerController.setAttacked (true);
			    break;
		}
	}

	public void setState(string state) {
		switch (state) {
		    case "walking":
			    this.state = State.walking;
			    break;
		    case "running":
			    this.state = State.running;
			    break;
		    case "attacking":
			    this.state = State.attacking;
			    break;
		    }
	}
}

