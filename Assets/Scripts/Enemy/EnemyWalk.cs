using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
[RequireComponent (typeof(NavMeshAgent))]
public class EnemyWalk : MonoBehaviour {
	private float animatorSpeed = 2.0f;
	private float agentSpeed = 2.0f;
	private float agentRunSpeed = 5.0f;
	private float ATTACK_DISTANCE = 4;
	private float RUNNING_DISTANCE = 200;
	private GameObject player;
	private NavMeshAgent agent;
	private Animator animator;
	private CapsuleCollider collider;
	private Rigidbody rigidbody;
	private PlayerController playerController;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator> ();
		this.collider = GetComponent<CapsuleCollider> ();
		this.rigidbody = GetComponent<Rigidbody> ();
		this.agent = GetComponent<NavMeshAgent>();
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.playerController = this.player.GetComponent<PlayerController>();
		this.animator.speed = this.animatorSpeed;
		this.agent.speed = this.agentSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// update location of the player, 'the target' of the enemy
		this.agent.SetDestination (this.player.transform.position);
		float distanceToPlayer = this.distancetoplayer ();
		if (distanceToPlayer < this.RUNNING_DISTANCE) {
			if (distanceToPlayer < this.ATTACK_DISTANCE) {
				this.animator.SetBool ("isRunning", false);
				this.animator.SetBool ("isStopped", true);
				this.agent.speed = this.agentSpeed;
				this.playerController.setPunched (true);
			} else {
				this.animator.SetBool ("isRunning", true);
				this.animator.SetBool ("isStopped", false);
				this.agent.speed = this.agentRunSpeed;
				this.playerController.setPunched (false);
			}
		} else {
			this.animator.SetBool ("isRunning", false);
			this.animator.SetBool ("isStopped", false);
			this.agent.speed = this.agentSpeed;
		}
	}

	private float distancetoplayer() {
		return (transform.position - this.player.transform.position).sqrMagnitude;
	}
}
