using UnityEngine;
using System.Collections;

public class GenerateAttributes : MonoBehaviour {
    [SerializeField]
    private GameObject keyPrefab;

    private GameObject labyrinth;
	private GameObject[] attributes;
	private int numberOfAttributes = 10;

	// Use this for initialization
	void Start () {
		this.labyrinth = GameObject.FindGameObjectWithTag("Labyrinth");

        // get min/max x & z
        float xSize = this.labyrinth.GetComponent<Renderer>().bounds.size.x;
		float maxX = xSize - (xSize / 2);
		float zSize = this.labyrinth.GetComponent<Renderer>().bounds.size.z;
		float maxZ = zSize - (zSize / 2);
		float minX = this.labyrinth.transform.position.x - maxX;
		float minZ = this.labyrinth.transform.position.z - maxZ;

		this.attributes = new GameObject[numberOfAttributes];
        float randomX;
        float randomZ;
        float y;
        bool isColliding = false;
        int index = 0;

        while (index < numberOfAttributes) {
			randomX = Random.Range(minX, maxX);
			randomZ = Random.Range(minZ, maxZ);
			y = -4;

			Vector3 attributePos = new Vector3 (randomX, y, randomZ);

			// cubes nog vervangen door 3D modellen
			this.keyPrefab.transform.position = new Vector3 (randomX, y, randomZ);
			GameObject key = (GameObject)Instantiate (this.keyPrefab, this.keyPrefab.transform.position, Quaternion.identity);

			// check if there is a floor somewhere under the attribute to know if it's placed in the labyrinth it self
			if (Physics.Raycast (key.transform.position, -Vector3.up, 4)) {
				Collider[] myarray = Physics.OverlapSphere (key.transform.position, 0.5f);
				
				for (int j = 0; j < myarray.Length; j++) {
					print ("iteratie" + j);
					// check if the attribute is colliding with a wall of the labyrinth, if it's not it can be added to list of attributes
					if (myarray [j].gameObject.CompareTag ("Labyrinth")) {
						isColliding = true;
						GameObject.Destroy (key);
						print ("vernietigd");
						break;
					}
				}

				if (!isColliding) {
					print ("toegevoegd");
					attributes [index] = key;
					index++;
				}
			} else {
				GameObject.Destroy (key);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 randomRotation = new Vector3(0, 1, 0);
		foreach(GameObject attribute in this.attributes) {
			attribute.transform.Rotate(randomRotation);
		}
	}
}
