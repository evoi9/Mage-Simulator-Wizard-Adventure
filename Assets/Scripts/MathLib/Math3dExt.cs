using UnityEngine;
using System.Collections;
using System;



public class Math3dExt : MonoBehaviour {

	

	public static Vector3 MidPosition(Vector3 pos1, Vector3 pos2){

		Vector3 total = pos1 + pos2;

		Vector3 mid = new Vector3 (total.x / 2, total.y / 2, total.z/2);

		return mid;

	}


	public static float Distance(Vector3 vec1, Vector3 vec2){

		Vector3 diff = vec1 - vec2;

		return diff.magnitude;

	}

	public static Vector3 Direction(Vector3 from, Vector3 to){

		Vector3 diff = to - from;

		return diff.normalized;

	}

	public static bool VectorEqual(Vector3 one, Vector3 two, Vector3 error){

		Vector3 diff = two - one;

		if (Math.Abs (diff.x) > Math.Abs (error.x))
			return false;

		if (Math.Abs (diff.y) > Math.Abs (error.y))
			return false;

		if (Math.Abs (diff.z) > Math.Abs (error.z))
			return false;

		return true;
	}

	public static bool VectorEqual(Vector3 one, Vector3 two, float error){

		return VectorEqual (one, two, new Vector3 (error, error, error));

	}

	public static bool VectorEqual(Vector3 one, Vector3 two){

		return VectorEqual (one, two, 0.01f);
	}

}
