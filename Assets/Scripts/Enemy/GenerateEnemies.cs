using UnityEngine;
using System.Collections;

public class GenerateEnemies : MonoBehaviour {
	public GameObject enemyPrefab;
	private GameObject[] enemies;
	private GameObject labyrinth;
	private int numberOfEnemies = 100;

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

		enemies = new GameObject[numberOfEnemies];
		bool isColliding = false;
		int index = 0;

		while (index < numberOfEnemies) {
			isColliding = false;
			randomX = Random.Range (minX, maxX);
			randomZ = Random.Range (minZ, maxZ);
			y = -5;

			Vector3 enemyPosition = new Vector3 (randomX, y, randomZ);

			NavMeshHit hit;
			if (NavMesh.SamplePosition(enemyPosition, out hit, 1.0f, NavMesh.AllAreas)) {
				this.enemyPrefab.transform.position = hit.position;
			}
			GameObject enemy = (GameObject)Instantiate (this.enemyPrefab, this.enemyPrefab.transform.position, Quaternion.identity);

			// because the y position of the enemy is already the same coordinate as the possible floor under it
			enemyPosition.y = enemyPosition.y + 1;
			// check if there is a floor somewhere under the attribute to know if it's placed in the labyrinth it self
			if (Physics.Raycast (enemyPosition, -Vector3.up, 4)) {
				if (Physics.Raycast (enemyPosition, Vector3.forward, 2) || Physics.Raycast (enemyPosition, Vector3.back, 2)
					|| Physics.Raycast (enemyPosition, Vector3.left, 2) || Physics.Raycast (enemyPosition, Vector3.right, 2)) {
					GameObject.Destroy (enemy);
				} else {
					enemies[index] = enemy;
					index++;
				}
			} else {
				GameObject.Destroy (enemy);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
