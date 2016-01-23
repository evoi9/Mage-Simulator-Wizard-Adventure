using UnityEngine;
using System.Collections;

public class MageHandController : MonoBehaviour {



	private HandController handController;

	// Use this for initialization
	void Start () {

		handController = GetComponent<HandController> ();

	}

	bool IsGestureInFireBallBegin(HandModel hand){

		return HandRecog.IsPalmFacingUpwards (hand, 20.0f) && HandRecog.IsHandClenching (hand, 20.0f);

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
