using UnityEngine;
using System.Collections;

public class MageThunderHandController : MageHandController {



	//private ThunderBolt boltPrefab;

	// Use this for initialization

	public float palmDirMinAngle = 120.0f;
	public float handClenchingMinAngle = 15.0f;

	public GameObject thunderBoltPrefab;

	protected void Start () {

		base.Start ();
	}

	bool IsReadyToCastBolts(HandModel leftHand, HandModel rightHand){
		
		//Debug.Log (HandRecog.PalmsFacingEachOther (handController, leftHand, rightHand, palmDirMinAngle));

		return HandRecog.PalmsFacingEachOther (handController, leftHand, rightHand, palmDirMinAngle) &&
			HandRecog.IsHandClenchingStrict (leftHand, handClenchingMinAngle) && HandRecog.IsHandClenchingStrict (rightHand, handClenchingMinAngle);
	}

	void CastBolts(HandModel LeftHand, HandModel rightHand ){

		

	}

	
	// Update is called once per frame
	protected void Update () {

		HandModel[] hands = handController.GetAllGraphicsHands ();
		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);

		if (rightHand && leftHand) {

			if (IsReadyToCastBolts (leftHand, rightHand)) {

				//Debug.Log ("yeah");
			}

		}
	
	}
}
