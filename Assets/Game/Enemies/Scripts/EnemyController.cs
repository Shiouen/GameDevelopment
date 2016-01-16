using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
public class EnemyController : MonoBehaviour {
	private float ATTACK_DISTANCE = 1.5f;
	private float RUNNING_DISTANCE = 200;

	private GameObject player;
	private CapsuleCollider collider;
	private Rigidbody rigidbody;
	private EnemyManagement enemyManagement;

	// Use this for initialization
	void Start () {
		this.collider = GetComponent<CapsuleCollider> ();
		this.rigidbody = GetComponent<Rigidbody> ();

		this.player = GameObject.FindGameObjectWithTag("Player");
		this.enemyManagement = GetComponent<EnemyManagement> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float distanceToPlayer = this.distancetoplayer ();
		if (distanceToPlayer < this.RUNNING_DISTANCE && distanceToPlayer < this.ATTACK_DISTANCE) {
			this.enemyManagement.setState ("attacking");
		} else if (distanceToPlayer < this.RUNNING_DISTANCE) {
			this.enemyManagement.setState ("running");
		} else {
			this.enemyManagement.setState ("walking");
		}
	}

	private float distancetoplayer() {
		return (transform.position - this.player.transform.position).sqrMagnitude;
	}
}
