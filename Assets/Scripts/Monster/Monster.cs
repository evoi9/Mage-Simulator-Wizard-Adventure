using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private int monstersHit = 0;

	//public GameObject monsterPrefab;
	public Vector3 initSpawn;
	Vector3 currPos;
	Vector3 targetPos;
	float time = 1000f;
	float speed, speedX, speedZ, distance;
	bool check = false;
	//GameObject monster;

	// Use this for initialization
	void Start () {

		initSpawn = new Vector3 (240, 0, 40); //(0, 0, 2.6f);
		Vector3 currPos = transform.position;
		targetPos = GameObject.Find("FPSController").transform.position;
		System.Random rand = new System.Random ();
		float z = rand.Next ((int)initSpawn.z - 10, (int)initSpawn.z + 11);
		float x = rand.Next ((int)initSpawn.x - 25, (int)initSpawn.x + 26);
		Vector3 spawnPt = new Vector3 (x, 0, z);

		//GameObject monster = Instantiate (monsterPrefab, initSpawn, Quaternion.Euler(0, 180, 0)) as GameObject;
		//cube = GameObject.Instantiate(monsterPrefab, initSpawn, Quaternion.identity) as GameObject;
		distance = Mathf.Sqrt(Mathf.Pow((currPos.x-targetPos.x),2.0f)+Mathf.Pow((currPos.z-targetPos.z),2.0f));
		speed = distance / time;

		speedX = speed * (targetPos.x - currPos.x) / distance;
		speedZ = speed * (targetPos.z - currPos.z) / distance;
	}

	// Update is called once per frame
	void Update () {
		currPos = gameObject.transform.position;
		if (currPos.x <= (targetPos.x + 2.0f) && currPos.x >= (targetPos.x - 2.0f)
			&& currPos.z <= (targetPos.z + 2.0f) && currPos.z >= (targetPos.z - 2.0f)) {
			check = true;
		}

		if (check != true) {
			transform.position += new Vector3 (speedX, 0, speedZ);
		}
	}

	void OnCollisionEnter(Collision other) {

		if(other.gameObject.tag == "Spell") {
			Animator animator = GetComponent<Animator>();
			animator.SetTrigger("HitByPlayer");
			Debug.Log ("hit by player!");
			Destroy(other.gameObject);
			monstersHit++;
			gameObject.AddComponent<TimedDestroy>();
			
		}
	}
}
