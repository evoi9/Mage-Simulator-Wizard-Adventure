using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private int monstersHit = 0;
	//public GameObject monsterPrefab;
	public Vector3 initSpawn;
	float time = 1000f;
	float speed, speedX, speedZ, distance;
	//GameObject monster;

	private Vector3 currPos;
	private Vector3 targetPos;
	private bool check = false;

	private Rigidbody r;

	// Use this for initialization
	void Start () {

		//initSpawn = new Vector3 (240, 0, 40); //(0, 0, 2.6f);
		//System.Random rand = new System.Random ();
		//float z = rand.Next ((int)initSpawn.z - 10, (int)initSpawn.z + 11);
		//float x = rand.Next ((int)initSpawn.x - 25, (int)initSpawn.x + 26);
		//Vector3 spawnPt = new Vector3 (x, 0, z);
		//Debug.Log ("spawnPt" + spawnPt);
		gameObject.transform.position = initSpawn;
		//Debug.Log ("gameObject.transform.position" + gameObject.transform.position);
		r = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () {
		CloseToPlayer ();
		//check = false; // reset check for the next wave of monsters
	}

	public void CloseToPlayer () {
		
		currPos = transform.position;
		targetPos = GameObject.Find("FPSController").transform.position;
		transform.LookAt (targetPos);	// make sure monster looks at player before attacking
		r.velocity = transform.forward*2.0f;

		if (currPos.x <= (targetPos.x + 2.0f) && currPos.x >= (targetPos.x - 2.0f)
			&& currPos.z <= (targetPos.z + 2.0f) && currPos.z >= (targetPos.z - 2.0f)) {
			r.velocity = Vector3.zero;
			Animator animator = GetComponent<Animator>();
			animator.SetTrigger ("CloseToPlayer"); 
		
			if (currPos.x <= (targetPos.x + 0.75f) && currPos.x >= (targetPos.x - 0.75f)
				&& currPos.z <= (targetPos.z + 0.75f) && currPos.z >= (targetPos.z - 0.75f) && !animator.IsInTransition(0)) {
 				
			
					/* invoke some C# file that has to do with game fucking over */

			}
		}else {
			

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
