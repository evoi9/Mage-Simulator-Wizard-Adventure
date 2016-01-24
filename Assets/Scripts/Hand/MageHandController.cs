using UnityEngine;
using System.Collections;

public class MageHandController : MonoBehaviour {


	protected HandController handController;

	public float confidenceLevel;

	protected bool IsCastingStarted;

	protected MageSpellControl spellControl;

//	private GameObject leftPalmObject;
//	private GameObject rightPalmObject;

	// Use this for initialization
	protected void Start () {

		handController = GetComponent<HandController> ();
		spellControl = GetComponent<MageSpellControl> ();
//		leftPalmObject = new GameObject ();
//		rightPalmObject = new GameObject ();

	}
	
	// Update is called once per frame
	protected void Update () {

//		HandModel[] hands = handController.GetAllGraphicsHands ();
//		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
//		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);
//
//		if (rightHand) {
//
//			rightPalmObject.transform.position = rightHand.GetPalmPosition ();
//			rightPalmObject.transform.rotation = rightHand.transform.rotation;
//		}
//
//		if (leftHand)

	}
}
