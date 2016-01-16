using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public static bool isActive = false;

	[SerializeField]
	private float mouseSensitivity;
	[SerializeField]
	private float movementSpeed;
	[SerializeField]
	private float verticalViewAngle;
	private bool isAttacked = false;

	private float verticalRotation;
	private CharacterController characterController;
	private PlayerManagement playerManagement;

    private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		this.characterController = this.GetComponent<CharacterController>();
		this.verticalRotation = 0.0f;

		this.mouseSensitivity = (this.mouseSensitivity <= 0.0f) ? 5.0f : this.mouseSensitivity;
		this.movementSpeed = (this.movementSpeed <= 0.0f) ? 10.0f : this.movementSpeed;
		this.verticalViewAngle = (this.verticalViewAngle <= 0.0f) ? 120.0f : this.verticalViewAngle;

		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;

		this.playerManagement = GetComponent<PlayerManagement> ();
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (isActive) {
			Vector3 previousPosition = this.transform.localPosition;
			/* Rotation */
			
			// horizontal view change
			float rotateY = Input.GetAxis ("Mouse X") * mouseSensitivity;
			this.transform.Rotate (0.0f, rotateY, 0.0f);
			
			// vertical view change - limited to the chosen view angle
			this.verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			this.verticalRotation = Mathf.Clamp (this.verticalRotation, -(verticalViewAngle / 2.0f), (verticalViewAngle / 2.0f));

			this.mainCamera.transform.localRotation = Quaternion.Euler (this.verticalRotation, 0.0f, 0.0f);

			/* Movement */
			
			// z - move front/backwards
			float z = Input.GetAxis ("Vertical") * movementSpeed;
			// x - move left/right - 80% of it, since this movement seems to be faster than 
			// the front/backwards movement with equal speed value
			float x = Input.GetAxis ("Horizontal") * movementSpeed * 0.80f;

			// Automatic damage repair of the player when he's not under attack
			if (!this.isAttacked) {
				this.repairDamage ();
			}

			// Set state of Player
			if (Input.GetAxis ("Vertical") != 0 && (Input.GetKey (KeyCode.RightShift) || Input.GetKey (KeyCode.LeftShift))) {
				this.playerManagement.setState ("running");

				// need to multiply with rotation, otherwise we keep going 1 way (even if we rotate)
				z = z * 2.0f;
				Vector3 speed = this.transform.rotation * new Vector3 (x, 0.0f, z) * 0.5f;
				this.characterController.SimpleMove (speed);
				this.playerManagement.moveMinimapPlayer (speed, previousPosition);
			} else if (Input.GetAxis ("Vertical") != 0) {
				this.playerManagement.setState ("walking");

				// need to multiply with rotation, otherwise we keep going 1 way (even if we rotate)
				z = z * 0.3f;
				Vector3 speed = this.transform.rotation * new Vector3 (x, 0.0f, z);
				this.characterController.SimpleMove (speed);
				this.playerManagement.moveMinimapPlayer (speed, previousPosition);
			} else if (this.isAttacked) {
				this.playerManagement.setState ("attacked");
			} else {
				this.playerManagement.setState ("standing");
			}
		}
	}

	public void setAttacked(bool attacked) {
		this.isAttacked = attacked;
		if (attacked) {
			this.playerManagement.makeDamage ();
		}
	}

	private void repairDamage() {
		this.playerManagement.repairDamage ();
	}
}