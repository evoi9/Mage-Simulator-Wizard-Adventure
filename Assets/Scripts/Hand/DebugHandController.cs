using UnityEngine;
using System.Collections;

public class DebugHandController : MonoBehaviour {


	private HandController handController;
	// Use this for initialization
	void Start () {

		handController = GetComponent<HandController> ();
	
	}

//	void OnDrawGizmos()
//	{
//		Color color;
//		color = Color.green;
//		// local up
//		DrawHelperAtCenter(this.transform.up, color, 2f);
//
//		color.g -= 0.5f;
//		// global up
//		DrawHelperAtCenter(Vector3.up, color, 1f);
//
//		color = Color.blue;
//		// local forward
//		DrawHelperAtCenter(this.transform.forward, color, 2f);
//
//		color.b -= 0.5f;
//		// global forward
//		DrawHelperAtCenter(Vector3.forward, color, 1f);
//
//		color = Color.red;
//		// local right
//		DrawHelperAtCenter(this.transform.right, color, 2f);
//
//		color.r -= 0.5f;
//		// global right
//		DrawHelperAtCenter(Vector3.right, color, 1f);
//	}
//
//	private void DrawHelperAtCenter(
//		Vector3 direction, Color color, float scale)
//	{
//		Gizmos.color = color;
//		Vector3 destination = transform.position + direction * scale;
//		Gizmos.DrawLine(transform.position, destination);
//	}

	// Update is called once per frame
	void Update () {

		//this.transform.position = this.transform.position;

		HandModel[] hands = handController.GetAllGraphicsHands ();

		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);

		if (rightHand) {
			//float angle = HandRecog.AngleBetweenFingerTipsHorizontal (rightHand, 1, 2);
			//Debug.Log ("RightHand: " + angle);

			//bool result = HandRecog.IsPalmFacingUpwards(rightHand,15);
			//Debug.Log ("RightHand: " + result);

			float theta = 20.0f;
			//float angle = 0.0f;
			bool a = HandRecog.IsIndexFingerTipBent (rightHand, theta);
			bool b = HandRecog.IsMiddleFingerTipBent (rightHand, theta);
			//Debug.Log ("Middle Finger Bent: " + b );
			bool c = HandRecog.IsRingFingerTipBent (rightHand, theta);
			bool d = HandRecog.IsLittleFingerTipBent (rightHand, theta);

			bool t = HandRecog.IsThumbTipBent (rightHand, theta);
			//Debug.Log ("Thumb Bent: " + t);
			//Debug.Log ("Thumb: " + t+ ", Index Finger: " + a + ", Middle Finger: " + b + ", Ring Finger: " + c );
			//Debug.Log ("Thumb: " + t+ ", Index Finger: " + a + ", Middle Finger: " + b + ", Ring Finger: " + c + ", Little Finger: " + d);
		}


		if (leftHand) {
			//float angle = HandRecog.AngleBetweenFingerTipsHorizontal (leftHand, 1, 2);
			//Debug.Log ("LeftHand: " + angle);

			//bool result = HandRecog.IsPalmFacingUpwards(leftHand,15);
			//Debug.Log ("LeftHand: " + result);


//			float theta = 30;
//			bool a = HandRecog.IsIndexFingerTipBent (leftHand, theta);
//			bool b = HandRecog.IsMiddleFingerTipBent (leftHand, theta);
//			bool c = HandRecog.IsRingFingerTipBent (leftHand, theta);
//			bool d = HandRecog.IsLittleFingerTipBent (leftHand, theta);
//
//			bool t = HandRecog.IsThumbTipBent (leftHand, theta);
//
//			Debug.Log ("Thumb: " + t + ", Index Finger: " + a + ", Middle Finger: " + b + ", Ring Finger: " + c + ", Little Finger: " + d);
		}
	
	}
}
