using UnityEngine;
using System.Collections;

public class MageFireHandController : MonoBehaviour {


	private HandController handController;

	public FireBall fireBallPrefab;

	public float fireBallSpawnLocation = 0.15f;

	public float clenchingAngle = 15.0f;

	public float palmFacingUpAngle = 20.0f;

	public float confidenceLevel;

	private bool IsCastingFireBallStarted;

	private FireBall currentFireBall;




	// Use this for initialization
	void Start () {

		handController = GetComponent<HandController> ();


	}

	bool IsGestureInFireBallBegin(HandModel hand){

		return HandRecog.IsPalmFacingUpwards (hand, palmFacingUpAngle) && HandRecog.IsHandClenching (hand, clenchingAngle) && hand.GetLeapHand().Confidence >= confidenceLevel;

	}

	Vector3 GetFireBallSpawnPosition(HandModel hand, float distance){

		Vector3 palmPos = hand.GetPalmPosition ();

		Vector3 palmDir = hand.GetPalmNormal ();

		return palmPos + Vector3.Scale(palmDir,new Vector3(distance,distance,distance));
	}


	void CastFireBall(HandModel hand){
		
		if (!IsCastingFireBallStarted && hand.GetLeapHand().Confidence >= confidenceLevel) {

			IsCastingFireBallStarted = true;

			currentFireBall = GameObject.Instantiate (fireBallPrefab, GetFireBallSpawnPosition (hand, fireBallSpawnLocation), hand.gameObject.transform.rotation) as FireBall;

			currentFireBall.transform.SetParent (handController.transform);
		}
			
	}

	void ReleaseFireBall(HandModel hand){


		if (currentFireBall && IsCastingFireBallStarted && !HandRecog.IsHandClenching (hand, clenchingAngle) && hand.GetLeapHand().Confidence > confidenceLevel) {

			currentFireBall.Release (hand.GetPalmDirection (), 5.0f);
			IsCastingFireBallStarted = false;
			currentFireBall = null;
		}


	}


	void UpdateFireBall(HandModel hand){

		if (currentFireBall ) {

			currentFireBall.transform.position = GetFireBallSpawnPosition (hand, fireBallSpawnLocation);
			//currentFireBall.transform.rotation = transform.rotation;

			if (IsCastingFireBallStarted && HandRecog.IsPalmFacingUpwards(hand,palmFacingUpAngle)) {

				currentFireBall.Grow (Time.deltaTime);
			}
		}
			

	}
		
	// Update is called once per frame
	void Update () {


		HandModel[] hands = handController.GetAllGraphicsHands ();

		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);


		if (rightHand) {

			if (IsGestureInFireBallBegin (rightHand)) {

				CastFireBall (rightHand);
				//Debug.Log ("aaaa");

			} else {

				ReleaseFireBall (rightHand);
			}

			UpdateFireBall (rightHand);
		
		} else if (leftHand) {

			if (IsGestureInFireBallBegin (leftHand)) {

				//Debug.Log ("bbbb");
			}
		}
			
	
	}
}
