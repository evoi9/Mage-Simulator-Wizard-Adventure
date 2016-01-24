using UnityEngine;
using System.Collections;

public class MageThunderHandController : MageHandController {



	//private ThunderBolt boltPrefab;

	// Use this for initialization

	public float palmDirMinAngle = 120.0f;
	public float handClenchingMinAngle = 15.0f;

	public ThunderBolt thunderBoltPrefab;

	private ThunderBolt currentThunderBolt;

	private ThunderBolt [] bolts;


	protected void Start () {

		base.Start ();

		bolts = new ThunderBolt[5];
	}

	bool IsReadyToCastBolts(HandModel leftHand, HandModel rightHand){
		
		//Debug.Log (HandRecog.PalmsFacingEachOther (handController, leftHand, rightHand, palmDirMinAngle));

		return HandRecog.PalmsFacingEachOther (handController, leftHand, rightHand, palmDirMinAngle) &&
			HandRecog.IsHandClenchingStrict (leftHand, handClenchingMinAngle) && HandRecog.IsHandClenchingStrict (rightHand, handClenchingMinAngle);
	}

	void CastBolts(HandModel leftHand, HandModel rightHand ){


		if (!IsCastingStarted) {

			Vector3 middle = leftHand.GetPalmPosition () + rightHand.GetPalmPosition ();

			currentThunderBolt = GameObject.Instantiate (thunderBoltPrefab, middle, gameObject.transform.rotation) as ThunderBolt;
			currentThunderBolt.SetPosition (leftHand.GetPalmPosition (), rightHand.GetPalmPosition ());

			IsCastingStarted = true;
		}

		if (currentThunderBolt)
			currentThunderBolt.SetPosition (leftHand.GetPalmPosition (), rightHand.GetPalmPosition ());

	}

	
	// Update is called once per frame
	protected void Update () {

		HandModel[] hands = handController.GetAllGraphicsHands ();
		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);

		if (rightHand && leftHand) {

			if (IsReadyToCastBolts (leftHand, rightHand)) {

				CastBolts (leftHand, rightHand);
				//Debug.Log ("yeah");
			}

		}
	
	}
}
