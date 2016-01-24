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



	}
		
	public void Release(Vector3 dir, float speed){


	}
}
