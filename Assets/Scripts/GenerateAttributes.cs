using UnityEngine;
using System.Collections;

public class GenerateAttributes : MonoBehaviour {
	private GameObject labyrinth;
	private GameObject[] attributes;
	private int numberOfAttributes = 10;

	// Use this for initialization
	void Start () {
		this.labyrinth = GameObject.FindGameObjectWithTag("Labyrinth");
		float xSize = this.labyrinth.GetComponent<Renderer>().bounds.size.x;
		float maxX = xSize - (xSize / 2);
		float zSize = this.labyrinth.GetComponent<Renderer>().bounds.size.z;
		float maxZ = zSize - (zSize / 2);

		float x = this.labyrinth.transform.position.x - maxX;
		float z = this.labyrinth.transform.position.z - maxZ;
	
		float randomX;
		float randomZ;
		float y;

		attributes = new GameObject[numberOfAttributes];
		
		for (int i = 0; i < numberOfAttributes; i++) {
			randomX = Random.Range(x,maxX);
			randomZ = Random.Range(z,maxZ);
			y = -4;

			Vector3 attributePos = new Vector3(randomX,y,randomZ);

			GameObject attribute = GameObject.CreatePrimitive(PrimitiveType.Cube);
			attribute.transform.position = attributePos;
			// cubes nog vervangen door 3D modellen
			//this.attributePrefab.transform.position = new Vector3(randomX,y,randomZ);
			//GameObject attribute = (GameObject)Instantiate (this.attributePrefab, this.attributePrefab.transform.position, Quaternion.identity);

			// check if there is a floor somewhere under the attribute to know if it's placed in the labyrinth it self
			if (Physics.Raycast(attribute.transform.position, -Vector3.up, 4)) {
				Collider[] myarray = Physics.OverlapSphere(attribute.transform.position,0.5f);
					
				for(int j = 0; j < myarray.Length; j++)
				{
					// check if the attribute is colliding with a wall of the labyrinth, if it's not it can be added to list of attributes
					if(myarray[j].gameObject.CompareTag("Labyrinth")) {
						print(attribute.transform.position.x + ", " + attribute.transform.position.y + ", " + attribute.transform.position.z);
						i--;
						GameObject.Destroy(attribute);
						print ("object vernietigd, want botst met muur");
					} else {
						attributes[i] = attribute;
						print ("object toegevoegd");
					}
				}
			} else {
				i--;
				GameObject.Destroy(attribute);
				print ("object vernietigd want niet in gang");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
