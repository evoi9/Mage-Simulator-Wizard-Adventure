using UnityEngine;
using System.Collections;

public class MageFireHandController : MageHandController {



	public FireBall fireBallPrefab;
	private FireBall currentFireBall;

	public float fireBallSpawnLocation = 0.15f;

	public float clenchingAngle = 15.0f;

	public float palmFacingUpAngle = 20.0f;

	protected void Start(){

		base.Start ();

	}

	bool IsReadyToCastFireBall(HandModel hand){

		return HandRecog.IsPalmFacingUpwards (hand, palmFacingUpAngle) && HandRecog.IsHandClenchingStrict (hand, clenchingAngle) && hand.GetLeapHand().Confidence >= confidenceLevel;

	}

	Vector3 GetFireBallSpawnPosition(HandModel hand, float distance){

		Vector3 palmPos = hand.GetPalmPosition ();

		Vector3 palmDir = hand.GetPalmNormal ();

		return palmPos + Vector3.Scale(palmDir,new Vector3(distance,distance,distance));
	}


	void CastFireBall(HandModel hand){
		
		if (!IsCastingStarted && hand.GetLeapHand().Confidence >= confidenceLevel && !spellControl.SpellCasting()) {

			spellControl.SpellCasting ();

			IsCastingStarted = true;

			currentFireBall = GameObject.Instantiate (fireBallPrefab, GetFireBallSpawnPosition (hand, fireBallSpawnLocation), hand.gameObject.transform.rotation) as FireBall;

			currentFireBall.transform.SetParent (handController.transform);
		}
			
	}

	void ReleaseFireBall(HandModel hand){


		if (currentFireBall && IsCastingStarted && !HandRecog.IsHandClenchingNonStrict (hand, clenchingAngle) && hand.GetLeapHand().Confidence > confidenceLevel) {

			currentFireBall.Release (hand.GetPalmDirection (), 5.0f);
			IsCastingStarted = false;
			currentFireBall = null;

			spellControl.ReleaseCastingControl ();
		}


	}


	void UpdateFireBall(HandModel hand){

		if (currentFireBall ) {

			currentFireBall.transform.position = GetFireBallSpawnPosition (hand, fireBallSpawnLocation);
			//currentFireBall.transform.rotation = transform.rotation;

			if (IsCastingStarted && HandRecog.IsPalmFacingUpwards(hand,palmFacingUpAngle)) {

				currentFireBall.Grow (Time.deltaTime);
			}
		}
			
	}


		
	// Update is called once per frame
	protected void Update () {


		HandModel[] hands = handController.GetAllGraphicsHands ();
		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);


		if (rightHand) {

			if (IsReadyToCastFireBall (rightHand)) {
				
				CastFireBall (rightHand);

			} else {

				ReleaseFireBall (rightHand);
			}

			UpdateFireBall (rightHand);
		
		} else if (leftHand) {

			if (IsReadyToCastFireBall (leftHand)) {

				CastFireBall (leftHand);

			} else {

				ReleaseFireBall (leftHand);
			}

			UpdateFireBall (leftHand);

		} else {
			
			if(currentFireBall)
				Destroy (currentFireBall.gameObject);
			//currentFireBall = null;
			IsCastingStarted = false;
			spellControl.ReleaseCastingControl ();
			
		}
	}
}
