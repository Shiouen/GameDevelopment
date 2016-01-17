using UnityEngine;
using System.Collections;

public class GenerateAttributes : MonoBehaviour {
	private GameObject[] attributes;
	public GameObject clockPrefab;
	public GameObject compassPrefab;
	public GameObject flashlightPrefab;
	public GameObject gunPrefab;
	public GameObject keyPrefab;
	public GameObject winningAttributePrefab;
	private GameObject labyrinth;
	private GameObject winningAttribute;
	private int numberOfAttributes = 5;
	private GameObject[] prefabs;
	
	// Use this for initialization
	void Start () {
		this.labyrinth = GameObject.FindGameObjectWithTag ("Labyrinth");
		float xSize = this.labyrinth.GetComponent<Renderer> ().bounds.size.x;
		float maxX = xSize - (xSize / 2);
		float zSize = this.labyrinth.GetComponent<Renderer> ().bounds.size.z;
		float maxZ = zSize - (zSize / 2);

		float minX = this.labyrinth.transform.position.x - maxX;
		float minZ = this.labyrinth.transform.position.z - maxZ;
	
		float randomX;
		float randomZ;
		float y;

		prefabs = new GameObject[numberOfAttributes];
		prefabs[0] = keyPrefab;
		prefabs[1] = gunPrefab;
		prefabs[2] = flashlightPrefab;
		prefabs[3] = compassPrefab;
		prefabs[4] = clockPrefab;

		attributes = new GameObject[numberOfAttributes];
		bool isColliding = false;
		int index = 0;

		while (index < numberOfAttributes) {
			isColliding = false;
			randomX = Random.Range (minX, maxX);
			randomZ = Random.Range (minZ, maxZ);
			y = -4;

			// may not generated in the room with the treasure
			if (randomX > 170 && randomZ > 240) { continue; }
			Vector3 attributePos = new Vector3 (randomX, y, randomZ);

			this.prefabs[index].transform.position = new Vector3 (randomX, y, randomZ);
			GameObject attribute = (GameObject)Instantiate (this.prefabs[index], this.prefabs[index].transform.position, Quaternion.identity);

			// check if there is a floor somewhere under the attribute to know if it's placed in the labyrinth it self
			if (Physics.Raycast (attribute.transform.position, -Vector3.up, 4)) {
				Collider[] myarray = Physics.OverlapSphere (attribute.transform.position, 0.5f);
				
				for (int j = 0; j < myarray.Length; j++) {
					// check if the attribute is colliding with a wall of the labyrinth, if it's not it can be added to list of attributes
					if (myarray[j].gameObject.CompareTag ("Labyrinth")) {
						isColliding = true;
						GameObject.Destroy (attribute);
						break;
					}
				}

				if (!isColliding) {
					attributes [index] = attribute;
					index++;
				}
			} else {
				GameObject.Destroy (attribute);
			}
		}

		this.createWinningAttribute ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 randomRotation = new Vector3(0, 1, 0);
		foreach(GameObject attribute in this.attributes) {
			attribute.transform.Rotate(randomRotation);
		}
		this.winningAttribute.transform.Rotate (randomRotation);
	}

	private void createWinningAttribute() {
		this.winningAttribute = (GameObject)Instantiate (this.winningAttributePrefab, new Vector3(210,-5,290), Quaternion.identity);
	}
}
