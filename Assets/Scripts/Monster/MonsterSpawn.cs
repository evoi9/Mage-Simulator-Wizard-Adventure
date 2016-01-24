using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour {

	public GameObject monsterPrefab;

	// Use this for initialization
	void Start () {
		GameObject monster = Instantiate (monsterPrefab, new Vector3 (0, 0, 2.6f), Quaternion.Euler(0, 180, 0)) as GameObject;
	}

	// Update is called once per frame
	void Update () {

	}


}
