using UnityEngine;
using System.Collections;

public class TerrainCreator : MonoBehaviour {
	public GameObject wallPrefab;

	// Use this for initialization
	void Start () {
		this.wallPrefab.transform.position = new Vector3(50,5,90);
		this.wallPrefab.transform.localScale = new Vector3(70,10,2);
		GameObject wall = (GameObject)Instantiate (wallPrefab, this.wallPrefab.transform.position, Quaternion.Euler(0,90,0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

