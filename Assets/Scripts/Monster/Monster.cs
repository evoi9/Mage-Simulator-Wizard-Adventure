using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	private int monstersHit = 0;
	private bool inState = false;

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

			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("creature1GetHit"))
				inState = true;
			else if (inState) {
				Destroy (gameObject);
				inState = false;
			}
		}
	}
}
