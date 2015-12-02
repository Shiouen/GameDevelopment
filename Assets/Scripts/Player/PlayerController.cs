using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private float mouseSensitivity;
	[SerializeField]
	private float movementSpeed;
	[SerializeField]
	private float verticalViewAngle;

	private float verticalRotation;
	private CharacterController characterController;

	// Use this for initialization
	void Start () {
		this.characterController = this.GetComponent<CharacterController>();
		this.verticalRotation = 0.0f;

		this.mouseSensitivity = (this.mouseSensitivity <= 0.0f) ? 5.0f : this.mouseSensitivity;
		this.movementSpeed = (this.movementSpeed <= 0.0f) ? 10.0f : this.movementSpeed;
		this.verticalViewAngle = (this.verticalViewAngle <= 0.0f) ? 120.0f : this.verticalViewAngle;

		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		/* Rotation */
		
		// horizontal view change
		float rotateY = Input.GetAxis("Mouse X") * mouseSensitivity;
		this.transform.Rotate(0.0f, rotateY, 0.0f);
		
		// vertical view change - limited to the chosen view angle
		this.verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		this.verticalRotation = Mathf.Clamp(this.verticalRotation, -(verticalViewAngle / 2.0f), (verticalViewAngle / 2.0f));
		
		Camera.main.transform.localRotation = Quaternion.Euler(this.verticalRotation, 0.0f, 0.0f);

		/* Movement */
		
		// z - move front/backwards
		float z = Input.GetAxis("Vertical") * movementSpeed;
		// x - move left/right - 80% of it, since this movement seems to be faster than 
		// the front/backwards movement with equal speed value
		float x = Input.GetAxis("Horizontal") * movementSpeed * 0.80f;
		
		// need to multiply with rotation, otherwise we keep going 1 way (even if we rotate)
		Vector3 speed = this.transform.rotation * new Vector3(x, 0.0f, z);
		
		this.characterController.SimpleMove(speed);
	}
}