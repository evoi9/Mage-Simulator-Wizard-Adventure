using UnityEngine;
using System.Collections;
using Leap;

public class FireBall : MonoBehaviour {

	private ParticleSystem ps;
	private GameObject fireBallPrefab;
	private int count;

	public bool IsReleased = false;

	// Use this for initialization
	void Start () {

		ps = gameObject.GetComponent<ParticleSystem>();
		count = 0;

	}

	void FixedUpdate(){

		if (IsReleased) {
			Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
			//rigidBody.AddForce(transform.forward * 5.0f);
			//rigidBody.AddForce(Vector3.forward *5.0f);
		}

	}


	public void Grow(float factor){
		//ps = gameObject.GetComponent<ParticleSystem>();
		//ps.emissionRate.Equals (50f);
		ps = gameObject.GetComponent<ParticleSystem>();

		if (ps) {


			//			ps.startSize = 0.1f;
			//			while (ps.startSize < 1.5*factor) {
			//				ps.startSize += 0.001f;
			//				
			//			}
			//			//Debug.Log (factor);
			//			ps.maxParticles += 100;
			ps.startSize = 1f*factor;
			//ps.maxParticles = (int) 1000*factor;
			gameObject.transform.localScale = new Vector3 (0.25f *factor, 0.25f*factor, 0.25f*factor);
			GetComponent<SphereCollider>().radius = 1.5f *factor;


		}

		//gameObject.transform.localScale += new Vector3 (1*factor,1*factor,1*factor);
		//	Debug.Log ("ps");


		//gameObject.transform.localScale += new Vector3 (1*factor, 1*factor, 1*factor);
	}


	public void Release(Vector3 dir, float magnitude, float gravityAmend){

		gameObject.transform.LookAt (dir);
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();

		rigidBody.AddForce(dir * magnitude);

		//Debug.Log ("pinched");
		//Rigidbody rigidBody = currentFireBall.GetComponent<Rigidbody>();

		//rigidBody.useGravity = true;

		//Vector3 amended = Vector3.Scale (Physics.gravity, new Vector3 (gravityAmend, gravityAmend, gravityAmend));

		rigidBody.AddForce (gravityAmend * Physics.gravity);

		//Vector3 gravityAmend = Vector3.Scale (Physics.gravity, new Vector3 (0.5f, 0.5f, 0.5f));
		//rigidBody.AddForce (gravityAmend, ForceMode.Acceleration); 
		gameObject.AddComponent<TimedDestroy>();

		IsReleased = true;

	}

	// Update is called once per frame
	void Update () {



	}
	void OnCollisionEnter(Collision other)
	{

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
