using UnityEngine;
using System.Collections;

public class ThunderBolt : MonoBehaviour {


	private ElectricityLine3D electricity;

	private GameObject from;
	private GameObject to;
	// Use this for initialization
	void Start () {

		electricity = GetComponent<ElectricityLine3D> ();
		from = new GameObject ();
		to = new GameObject ();
		electricity.pointsTransform [0] = from.transform;
		electricity.pointsTransform [1] = to.transform;

	}	

	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPosition(Vector3 pole1, Vector3 pole2){

		from.transform.position.x = pole1.x;
		from.transform.position.y = pole1.y;
		from.transform.position.z = pole1.z;


		to.transform.position.x = pole2.x;
		to.transform.position.y = pole2.y;
		to.transform.position.z = pole2.z;

	}
		
	public void Release(Vector3 dir, float speed){


	}
}
