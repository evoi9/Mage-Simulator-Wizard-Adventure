using UnityEngine;
using System.Collections;

public class MageHandController : MonoBehaviour {


	protected HandController handController;

	public float confidenceLevel;

	protected bool IsCastingStarted;

	protected MageSpellControl spellControl;

	// Use this for initialization
	protected void Start () {

		handController = GetComponent<HandController> ();
		spellControl = GetComponent<MageSpellControl> ();

	}
	
	// Update is called once per frame
	protected void Update () {

	}
}
