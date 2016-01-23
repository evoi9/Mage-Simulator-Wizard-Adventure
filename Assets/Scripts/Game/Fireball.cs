using UnityEngine;
using System.Collections;
using Leap;

public class FireBall : MonoBehaviour {

	private ParticleSystem ps;
	private GameObject fireBallPrefab;
	private int count;

	private bool IsReleased = false;

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


	public void Grow(float deltaTime){
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

			if(ps.startSize <= 0.35f)
				ps.startSize += deltaTime * 0.06f;
			//ps.maxParticles = (int) 1000*factor;

			if (gameObject.transform.localScale.magnitude <= 0.3f) {

				gameObject.transform.localScale += new Vector3 ( deltaTime * 0.04f,  deltaTime *0.04f,  deltaTime*0.04f);
				GetComponent<SphereCollider>().radius = gameObject.transform.localScale.magnitude;

			}
		
		
		}

		//gameObject.transform.localScale += new Vector3 (1*factor,1*factor,1*factor);
		//	Debug.Log ("ps");


		//gameObject.transform.localScale += new Vector3 (1*factor, 1*factor, 1*factor);
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
