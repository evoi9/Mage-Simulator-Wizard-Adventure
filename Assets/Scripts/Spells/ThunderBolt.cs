using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThunderBolt : SpellBase {


	private ElectricityLine3D electricity;

	private Transform from;
	private Transform to;
	// Use this for initialization
	//private bool IsReleased = false;

	private Vector3 releaseDirection;
	private float releaseSpeed;

	private CapsuleCollider collider;


	void Awake(){
		electricity = GetComponent<ElectricityLine3D> ();

	}

	void Start () {
		

		collider = gameObject.AddComponent<CapsuleCollider> ();
		collider.center = Vector3.zero;
		collider.height = 0;
		collider.radius = 0.05f;
		collider.direction = 2;


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

		if (IsReleased) {

			Vector3 distanceTraveled= releaseDirection * releaseSpeed * Time.deltaTime;

			Vector3[] array = electricity.GetPoints ();
			array [0] = array [0] + distanceTraveled;
			array [1] = array [1] + distanceTraveled;

			electricity.SetPoints (array);
			collider.transform.LookAt (array [0]);
			collider.height = (array [1] - array [0]).magnitude * 0.95f;
		



		}
	
	}

	public void SetPosition(Vector3 pole1, Vector3 pole2){

		Vector3[] array = new Vector3[2];
		array [0] = pole1;
		array [1] = pole2;

		electricity.SetPoints (array);

	}

		
	public void Release(Vector3 dir, float speed){
//		
		releaseDirection = dir;
		releaseSpeed = speed;

//		gameObject.transform.LookAt (dir);
//		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
//
//		rigidBody.useGravity = false;
//
//		rigidBody.velocity = dir * speed;
//
//		//Vector3 amended = Vector3.Scale (Physics.gravity, new Vector3 (gravityAmend, gravityAmend, gravityAmend));
//
//		//Vector3 gravityAmend = Vector3.Scale (Physics.gravity, new Vector3 (0.5f, 0.5f, 0.5f));
//		//rigidBody.AddForce (gravityAmend, ForceMode.Acceleration); 
		gameObject.AddComponent<TimedDestroy>();
		IsReleased = true;
		//gameObject.AddComponent<Rigidbody> ();


	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("a");
		//		if(other.gameObject.name == "Cylinder")
		//		{
		//			Debug.Log ("fireball hit");
		//			Destroy(other.gameObject);
		//			count = count+1;
		//		}
		//
		//		Destroy (gameObject);
	}
}
