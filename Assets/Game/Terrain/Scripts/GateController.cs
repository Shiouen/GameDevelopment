using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameObject))]
public class GateController : MonoBehaviour {
    public bool IsOpen = false;

    [SerializeField]
    private GameObject GatePrefab;

    private GameObject smallGate;
    private GameObject bigGate;
    private GameObject labyrinth;

    private GameObject mainCamera;
    private GameObject gateCamera;

    private static readonly Vector3 smallGatePosition = new Vector3(208.56f, -4.92003f, 245.73f);
    private static readonly Vector3 smallGateRotation = new Vector3(0.0f, 0.0f, 0.0f);
    private static readonly Vector3 smallGateScale = new Vector3(1.93f, 2.0f, 2.0f);

    private static readonly Vector3 bigGatePosition = new Vector3(182.6f, -4.92003f, 297.8f);
    private static readonly Vector3 bigGateRotation = new Vector3(0.0f, 90.0f, 0.0f);
    private static readonly Vector3 bigGateScale = new Vector3(2.57f, 2.2f, 2.57f);

    // Use this for initialization
    public void Start() {
        this.IsOpen = false;

        this.labyrinth = GameObject.FindGameObjectWithTag("Labyrinth");
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        this.gateCamera = GameObject.FindGameObjectWithTag("GateCamera");

        this.gateCamera.GetComponent<Camera>().enabled = false;
        this.mainCamera.GetComponent<Camera>().enabled = true;

        this.smallGate = this.CreateGate("Small Gate", smallGatePosition, smallGateRotation, smallGateScale);
        this.bigGate = this.CreateGate("Big Gate", bigGatePosition, bigGateRotation, bigGateScale);

        this.SetRotationHinge(this.FindHinge(this.smallGate, "right_gate", "outer_upper_hinge"), new Vector3(0.0f, -180.0f, 0.0f));
    }

    public void Update() {
		if (this.IsOpen) {
            StartCoroutine(this.ToggleGates());
            this.IsOpen = !this.IsOpen;
        }
    }

    public IEnumerator ToggleGates() {
        this.mainCamera.GetComponent<Camera>().enabled = false;
        this.gateCamera.GetComponent<Camera>().enabled = true;

        yield return new WaitForSeconds(2);

        this.ToggleHinge(this.FindHinge(this.smallGate, "left_gate", "outer_upper_hinge"), this.IsOpen);
        this.ToggleHinge(this.FindHinge(this.smallGate, "right_gate", "outer_upper_hinge"), !this.IsOpen);
        this.ToggleHinge(this.FindHinge(this.bigGate, "left_gate", "outer_upper_hinge"), this.IsOpen);
        this.ToggleHinge(this.FindHinge(this.bigGate, "right_gate", "outer_upper_hinge"), !this.IsOpen);

        yield return new WaitForSeconds(2);

        this.gateCamera.GetComponent<Camera>().enabled = false;
        this.mainCamera.GetComponent<Camera>().enabled = true;
    }

    private GameObject CreateGate(string name, Vector3 position, Vector3 rotation, Vector3 scale) {
        GameObject gate = (GameObject) Instantiate(this.GatePrefab, Vector3.zero, Quaternion.identity);

        gate.transform.position = position;
        gate.transform.rotation = Quaternion.Euler(rotation);
        gate.transform.localScale = scale;

        gate.name = name;
        gate.transform.parent = this.labyrinth.transform;

        return gate;
    }

    private GameObject FindHinge(GameObject gate, string gatePart, string hinge) {
        return gate.transform.FindChild(gatePart).FindChild(hinge).gameObject;
    }

    private void RotateHinge(GameObject hinge, Vector3 rotation) {
        hinge.transform.Rotate(rotation);
    }

    private void SetRotationHinge(GameObject hinge, Vector3 rotation) {
        hinge.transform.rotation = Quaternion.Euler(rotation);
    }

    private void ToggleHinge(GameObject hinge, bool clockwise) {
        this.RotateHinge(hinge, new Vector3(0.0f, (clockwise) ? 90.0f : -90.0f, 0.0f));
    }
}
