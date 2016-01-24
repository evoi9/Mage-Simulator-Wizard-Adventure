using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private int monstersHit = 0;

	private Vector3 currPos;
	private Vector3 targetPos;
	private bool check = false;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
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
