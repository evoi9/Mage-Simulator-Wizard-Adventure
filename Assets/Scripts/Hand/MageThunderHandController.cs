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


		if (!IsCastingStarted && !spellControl.SpellCasting()) {

			Vector3 middle = leftHand.GetPalmPosition () + rightHand.GetPalmPosition ();

			currentThunderBolt = GameObject.Instantiate (thunderBoltPrefab, middle, gameObject.transform.rotation) as ThunderBolt;
			currentThunderBolt.SetPosition (leftHand.GetPalmPosition (), rightHand.GetPalmPosition ());

			IsCastingStarted = true;
			spellControl.GrabCastingControl ();
		}

	

	}

	void UpdateBolts(HandModel leftHand, HandModel rightHand){

		if (currentThunderBolt)
			currentThunderBolt.SetPosition (leftHand.GetPalmPosition (), rightHand.GetPalmPosition ());
	}

	void ReleaseBolts(HandModel leftHand, HandModel rightHand){

		if (currentThunderBolt) {
			currentThunderBolt.Release ((leftHand.GetPalmDirection () + rightHand.GetPalmDirection ()).normalized, 5.0f);
			IsCastingStarted = false;
			spellControl.ReleaseCastingControl ();
		}
	}

	void CancelCasting(){	

		if (currentThunderBolt)
			Destroy (currentThunderBolt.gameObject);

		IsCastingStarted = false;
		spellControl.ReleaseCastingControl ();

	}
	
	// Update is called once per frame
	protected void Update () {

		HandModel[] hands = handController.GetAllGraphicsHands ();
		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);

		if (rightHand && leftHand) {

			if (IsReadyToCastBolts (leftHand, rightHand)) {

				CastBolts (leftHand, rightHand);
				UpdateBolts (leftHand, rightHand);
	
			} else {
				ReleaseBolts (leftHand, rightHand);
			}

		} else {

			CancelCasting ();

		}
	
	}
}
