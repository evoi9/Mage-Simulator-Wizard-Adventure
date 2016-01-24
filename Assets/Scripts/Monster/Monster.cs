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

	private Vector3 currPos;
	private Vector3 targetPos;
	private bool check = false;

	// Use this for initialization
	void Start () {

<<<<<<< HEAD

=======
		initSpawn = new Vector3 (240, 0, 40); //(0, 0, 2.6f);
		System.Random rand = new System.Random ();
		float z = rand.Next ((int)initSpawn.z - 10, (int)initSpawn.z + 11);
		float x = rand.Next ((int)initSpawn.x - 25, (int)initSpawn.x + 26);
		Vector3 spawnPt = new Vector3 (x, 0, z);

		gameObject.transform.position = spawnPt;

		currPos = transform.position;
		targetPos = GameObject.Find("FPSController").transform.position;

		//GameObject monster = Instantiate (monsterPrefab, initSpawn, Quaternion.Euler(0, 180, 0)) as GameObject;
		//cube = GameObject.Instantiate(monsterPrefab, initSpawn, Quaternion.identity) as GameObject;
		distance = Mathf.Sqrt(Mathf.Pow((currPos.x-targetPos.x),2.0f)+Mathf.Pow((currPos.z-targetPos.z),2.0f));
		speed = distance / time;

		speedX = speed * (targetPos.x - currPos.x) / distance;
		speedZ = speed * (targetPos.z - currPos.z) / distance;
>>>>>>> f18773cda4df7b245429b1adb703c97d166ee8f1
	}

	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		CloseToPlayer ();
		check = false; // reset check for the next wave of monsters
	}

	public void CloseToPlayer () {
		currPos = transform.position;
		targetPos = GameObject.Find("FPSController").transform.position;

		if (currPos.x <= (targetPos.x + 2.0f) && currPos.x >= (targetPos.x - 2.0f)
			&& currPos.z <= (targetPos.z + 2.0f) && currPos.z >= (targetPos.z - 2.0f)) {
			check = true;
			transform.LookAt (targetPos);	// make sure monster looks at player before attacking
			Animator animator = GetComponent<Animator>();
			animator.SetTrigger ("CloseToPlayer"); 
		
			if (currPos.x <= (targetPos.x + 0.75f) && currPos.x >= (targetPos.x - 0.75f)
				&& currPos.z <= (targetPos.z + 0.75f) && currPos.z >= (targetPos.z - 0.75f) && !animator.IsInTransition(0)) {
 				
					/* invoke some C# file that has to do with game fucking over */

			}
		}

		if (check != true) {
			//transform.position += new Vector3 (speedX, 0, speedZ);
=======
		currPos = gameObject.transform.position;
		if (currPos.x <= (targetPos.x + 2.0f) && currPos.x >= (targetPos.x - 2.0f)
			&& currPos.z <= (targetPos.z + 2.0f) && currPos.z >= (targetPos.z - 2.0f)) {
			check = true;
		}

		if (check != true) {
			transform.position += new Vector3 (speedX, 0, speedZ);
>>>>>>> f18773cda4df7b245429b1adb703c97d166ee8f1
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
