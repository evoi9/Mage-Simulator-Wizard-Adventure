using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private int monstersHit = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
