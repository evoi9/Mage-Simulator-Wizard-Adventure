using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThunderBolt : MonoBehaviour {


	private ElectricityLine3D electricity;

	private Transform from;
	private Transform to;
	// Use this for initialization
	//private bool IsReleased = false;


	void Awake(){
		
		electricity = GetComponent<ElectricityLine3D> ();
		//from = new GameObject ().transform;
		//to = new GameObject ().transform;

	}

	void Start () {



	}	

	
	// Update is called once per frame
	void Update () {
//		Debug.Log (electricity.GetPoints ().Length);
//		if (electricity.GetPoints ().Length == 0) {
//			Debug.Log ("A");
//			List<Transform> list = new List<Transform> ();
//			list.Add (from);
//			list.Add (to);
//			electricity.AddPoints (list);
//		}
	
	}

	public void SetPosition(Vector3 pole1, Vector3 pole2){

		Vector3[] array = new Vector3[2];
		array [0] = pole1;
		array [1] = pole2;

		electricity.SetPoints (array);

		//from.position.Set (pole1.x, pole1.y, pole1.z);

		//to.position.Set (pole2.x, pole2.y, pole2.z);


		//to.transform.position.x = pole2.x;
		//to.transform.position.y = pole2.y;
		//to.transform.position.z = pole2.z;
	}

		
	public void Release(Vector3 dir, float speed){

		gameObject.transform.LookAt (dir);
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();

		rigidBody.useGravity = false;

		rigidBody.velocity = dir * speed;

		//Vector3 amended = Vector3.Scale (Physics.gravity, new Vector3 (gravityAmend, gravityAmend, gravityAmend));

		//Vector3 gravityAmend = Vector3.Scale (Physics.gravity, new Vector3 (0.5f, 0.5f, 0.5f));
		//rigidBody.AddForce (gravityAmend, ForceMode.Acceleration); 
		gameObject.AddComponent<TimedDestroy>();





	}
}
