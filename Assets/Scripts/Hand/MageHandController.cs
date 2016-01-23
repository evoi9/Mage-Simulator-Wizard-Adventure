using UnityEngine;
using System.Collections;

public class MageHandController : MonoBehaviour {



	private HandController handController;

	// Use this for initialization
	void Start () {

		handController = GetComponent<HandController> ();

	}

	bool IsGestureInFireBallBegin(HandModel hand){
		float theta = 20.0f;
		bool a = HandRecog.IsIndexFingerTipBent (hand, theta);
		bool b = HandRecog.IsMiddleFingerTipBent (hand, theta);
		//Debug.Log ("Middle Finger Bent: " + b );
		bool c = HandRecog.IsRingFingerTipBent (hand, theta);
		bool d = HandRecog.IsLittleFingerTipBent (hand, theta);

		bool t = HandRecog.IsThumbTipBent (hand, theta);

		return HandRecog.IsPalmFacingUpwards (hand, 20.0f) && a && b && c && t;

	}
		
	// Update is called once per frame
	void Update () {


		HandModel[] hands = handController.GetAllGraphicsHands ();

		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);


		if (rightHand) {

			if (IsGestureInFireBallBegin (rightHand)) {

				Debug.Log ("aaaa");
			}
		
		}

		if (leftHand) {

			if (IsGestureInFireBallBegin (leftHand)) {

				Debug.Log ("bbbb");
			}


		}
	
	}
}
