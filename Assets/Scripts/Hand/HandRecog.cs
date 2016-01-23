using UnityEngine;
using System.Collections;
using System;
using Leap;

//Author Zhongshi Xi
//Github: https://github.com/xzs424
//Email: xizhongshiwise@gmail.com.
//If you have questions, please do not hesitate to ask.


public class HandRecog : MonoBehaviour {


//	//Detect if index finger, middle finger, ring finger and little finger are straight (not bent).
//	public static bool FourFingersStraight(HandModel hand){
//
//		return !IsIndexFingerBent (hand, 3) && !IsMiddleFingerBent (hand, 3) && !IsRingFingerBent (hand, 3)
//			&& !IsLittleFingerBent (hand, 3);
//
//	}
//
	public static bool IsPalmFacingUpwards(HandModel hand, float theta ){

		Vector3 dir = hand.GetPalmNormal ();

		//Vector3 worldUp = hand.transform.TransformDirection(hand.transform.up);

		float angle = Vector3.Angle (dir, hand.transform.up);

		//Debug.Log (angle);

		if (Math.Abs (angle) <= theta)
			return true;

		return false;

	}

	public static bool IsPalmFacingDownwards(HandModel hand, float theta){
		Vector3 dir = hand.GetPalmNormal ();

		float angle = Vector3.Angle (dir, -1* hand.transform.up);

		if (Math.Abs (angle) <= theta)
			return true;

		return false;

	}
		
	public static float DistanceBetweenPalms(HandModel handOne, HandModel handTwo){

		Vector3 handOnePalmPos = handOne.GetPalmPosition();
		Vector3 handTwoPalmPos = handTwo.GetPalmPosition ();

		return Math3dExt.Distance (handOnePalmPos, handTwoPalmPos);

	}

	public static Vector3 DirectionBetweenPalm(HandModel fromHand, HandModel toHand){

		Vector3 fromHandPalmPos = fromHand.GetPalmPosition ();
		Vector3 toHandPalmPos = toHand.GetPalmPosition ();

		return Math3dExt.Direction (fromHandPalmPos, toHandPalmPos);

	}

	public static bool IsHandClenching(HandModel hand, float minAngle){

		bool a = HandRecog.IsIndexFingerTipBent (hand, minAngle);
		bool b = HandRecog.IsMiddleFingerTipBent (hand, minAngle);
		//Debug.Log ("Middle Finger Bent: " + b );
		bool c = HandRecog.IsRingFingerTipBent (hand, minAngle);
		//bool d = HandRecog.IsLittleFingerTipBent (hand, minAngle);

		bool t = HandRecog.IsThumbTipBent (hand, minAngle);

		return a && b && c && t;


	}

	public static bool IsFingerBentWithinAngle(HandModel hand, int fingerIndex, int boneIndex, float minAngle, float maxAngle){

		FingerModel finger = hand.fingers [fingerIndex];
		
		Vector3 fingerDir = finger.GetBoneDirection (boneIndex);
		
		Vector3 palmNormal = hand.GetPalmNormal ();
		
		Vector3 palmDir = hand.GetPalmDirection ();
		
		if (fingerIndex == 0) {
			
			float angle = Math3d.SignedVectorAngle (palmDir, fingerDir, palmNormal);
			
			//Debug.Log ("Finger: " + finger.fingerType + ", bone: " + boneIndex + ", angle to palm direction: " + angle);
			
			if (angle >= minAngle && angle <= maxAngle)
				return true;
				
			
		} else if (fingerIndex > 0) {
			
			Vector3 projPlane = Vector3.Cross(palmNormal, palmDir).normalized;
			Vector3 projVector = Math3d.ProjectVectorOnPlane(projPlane,fingerDir).normalized;
			
			
			float angle = Math3d.SignedVectorAngle(palmNormal,projVector,projPlane);
			
			//Debug.Log ("Finger: " + finger.fingerType + ", bone: " + boneIndex + ", angle to palm direction: " + angle);
			
			if (angle >= minAngle && angle <= maxAngle)
				return true;
		}
		
		
		return false;

	}



	public static float AngleBetweenFingerAndVector(HandModel hand,int fingerIndex, int boneIndex, Vector3 referenceVector,Vector3 projectPlaneNormal){

		FingerModel finger = hand.fingers [fingerIndex];
		//Vector3 boneDir = finger.GetBoneDirection (boneIndex);
		Vector3 fingerVector = finger.GetBoneDirection (boneIndex);

		Vector3 fingerVectorProj = Math3d.ProjectVectorOnPlane (projectPlaneNormal, fingerVector).normalized;
		Vector3 referenceVectorProj = Math3d.ProjectVectorOnPlane (projectPlaneNormal, referenceVector).normalized;

		//Debug.Log (referenceVector.normalized + " , " + referenceVectorProj);
		//Vector3 crossNormal = Vector3.Cross (fingerVectorProj, referenceVectorProj).normalized;

		//float angle = Vector3.Angle (fingerVectorProj, referenceVectorProj);

		float angle = Math3d.SignedVectorAngle (referenceVectorProj, fingerVectorProj, projectPlaneNormal);

		//Debug.Log ("Is Right :" + hand.GetLeapHand ().IsRight);

		return angle;
	
			
//		//Debug.Log (crossNormal + ", " + projectPlaneNormal.normalized);
//		if (Math3dExt.VectorEqual (crossNormal, -1 * projectPlaneNormal.normalized)) {
//			Debug.Log ("My Method: " + angle + ", Math3d method: " + otherAngle);
//			return angle;
//
//		} else {
//			Debug.Log ("My Method: " + -1* angle + ", Math3d method: " + otherAngle);
//			return -angle;
//		}
			

	}

	public static float AngleBetweenFingerTips(HandModel hand, int fingerIndexOne, int fingerIndexTwo, Vector3 projectPlaneNormal){

		FingerModel fingerOne = hand.fingers [fingerIndexOne];
		FingerModel fingerTwo = hand.fingers [fingerIndexTwo];

		Vector3 fingerOneDir = fingerOne.GetBoneDirection (3);
		Vector3 fingerTwoDir = fingerTwo.GetBoneDirection (3);

		Vector3 fingerOneDirProj = Math3d.ProjectVectorOnPlane (projectPlaneNormal, fingerOneDir).normalized;
		Vector3 fingerTwoDirProj = Math3d.ProjectVectorOnPlane(projectPlaneNormal,fingerTwoDir).normalized;

		return Math3d.SignedVectorAngle ( fingerOneDirProj, fingerTwoDirProj, projectPlaneNormal);

	}


	public static float AngleBetweenFingerTipsVertical(HandModel hand, int fingerIndexOne, int fingerIndexTwo){
		Vector3 palmVector = hand.GetPalmDirection ();
		Vector3 palmNormal = hand.GetPalmNormal ();
		Vector3 crossVect = Vector3.Cross (palmVector, palmNormal).normalized;
		return AngleBetweenFingerTips (hand, fingerIndexOne, fingerIndexTwo, crossVect);

	}

	public static float AngleBetweenFingerTipsHorizontal(HandModel hand, int fingerIndexOne, int fingerIndexTwo){


		float angle = AngleBetweenFingerTips(hand,fingerIndexOne, fingerIndexTwo,hand.GetPalmNormal());

		if (hand.GetLeapHand ().IsRight)
			return -angle;

		return angle;

	}

	public static float AngleBetweenFingerTipAndPalmDirection(HandModel hand, int fingerIndex){

		return AngleBetweenFingerAndPalmDirection (hand, fingerIndex, 3);
	}

		
	public static float AngleBetweenFingerAndPalmDirection(HandModel hand, int fingerIndex, int boneIndex){

		Vector3 palmVector = hand.GetPalmDirection ();
		Vector3 palmNormal = hand.GetPalmNormal ();
		Vector3 crossVect = Vector3.Cross (palmVector, palmNormal).normalized;
		//Debug.Log (crossVect.normalized);

		float angle =  AngleBetweenFingerAndVector (hand, fingerIndex, boneIndex, palmVector, crossVect);

		return angle;
//		if (fingerIndex == 0) {
//			
//			if (hand.GetLeapHand ().IsRight) {
//				return angle;
//			} else {
//				return -angle;
//			}
//
//		} else {
//			
//			return -angle;
//		}
//			
	}
		
	
	public static float AngleBetweenPalmsNormals(HandModel leftHand, HandModel rightHand, Vector3 projectPlane){

		Vector3 leftPalmNorm = leftHand.GetPalmNormal ();
		Vector3 rightPalmNorm = rightHand.GetPalmNormal ();

		Vector3 leftPalmNormProj = Math3d.ProjectVectorOnPlane (projectPlane,leftPalmNorm).normalized;
		Vector3 rightPalmNormProj = Math3d.ProjectVectorOnPlane (projectPlane,rightPalmNorm).normalized;

		float angle = Math3d.SignedVectorAngle (leftPalmNormProj, rightPalmNormProj,projectPlane);

		return angle;

	}

	public static float AngleBetweenPalmsDirections(HandModel leftHand, HandModel rightHand, Vector3 projectPlane){

		Vector3 leftPalmDir = leftHand.GetPalmDirection ();
		Vector3 rightPalmDir = rightHand.GetPalmDirection ();

		Vector3 leftPalmDirProj = Math3d.ProjectVectorOnPlane (projectPlane, leftPalmDir).normalized;
		Vector3 rightPalmDirProj = Math3d.ProjectVectorOnPlane (projectPlane, rightPalmDir).normalized;

		float angle = Math3d.SignedVectorAngle (leftPalmDirProj, rightPalmDirProj, projectPlane);

		return angle;

	}

	public static HandModel FindFirstLeftHand(HandModel [] hands){

		for (int i = 0; i <hands.Length; i++) {

			HandModel hand = hands[i];
			if (hand.GetLeapHand().IsLeft){

				return hand;
			}

		}

		return null;

	}

	public static HandModel FindFirstRightHand(HandModel [] hands){

		for (int i = 0; i <hands.Length; i++) {
			
			HandModel hand = hands[i];
			if (hand.GetLeapHand().IsRight){
				
				return hand;
			}
			
		}
		
		return null;

	}


	private static bool IsFingerBent(HandModel hand, int fingerIndex, int boneIndex, float theta){

		//FingerModel finger = hand.fingers [fingerIndex];
		float angle = AngleBetweenFingerAndPalmDirection (hand, fingerIndex, boneIndex);
		//Debug.Log (angle);
		if (angle >= theta)
			return true;
		else
			return false;

	}

	private static bool IsFingerBent(HandModel hand, int fingerIndex, int  boneIndex, float minTheta, float maxTheta){
		
		float angle = AngleBetweenFingerAndPalmDirection (hand, fingerIndex, boneIndex);
		//Debug.Log (angle);
		if (angle >=minTheta && angle <= maxTheta)
			return true;
		else
			return false;

	}

	private static bool IsFingerTipBent(HandModel hand, int fingerIndex, float theta){

		return IsFingerBent (hand, fingerIndex, 3,theta);
	}

	public static bool IsThumbTipBent(HandModel hand, float theta){


		return IsFingerTipBent (hand, 0, theta);
	}

	public static bool IsIndexFingerTipBent(HandModel hand, float theta){

		return IsFingerTipBent (hand, 1, theta);
	}

	public static bool IsMiddleFingerTipBent(HandModel hand, float theta){

		return IsFingerTipBent (hand, 2, theta);
	}

	public static bool IsRingFingerTipBent(HandModel hand, float theta){

		return IsFingerTipBent (hand, 3, theta);

	}

	public static bool IsLittleFingerTipBent(HandModel hand, float theta){
		return IsFingerTipBent (hand, 4, theta);
	}
//	public bool IsGestureGrab(HandModel hand){
//
//		FingerModel [] fingers = hand.fingers;
//
//
//	}

//
//	public static bool IsFingerBent(HandModel hand, int fingerIndex, int boneIndex){
//
//		FingerModel finger = hand.fingers [fingerIndex];
//		
//		Vector3 fingerDir = finger.GetBoneDirection (boneIndex);
//		
//		Vector3 palmNormal = hand.GetPalmNormal ();
//		
//		Vector3 palmDir = hand.GetPalmDirection ();
//
//		if (fingerIndex == 0) {
//			 
//			 float angle = Math3d.SignedVectorAngle (palmDir, fingerDir, palmNormal);
//			
//			Debug.Log ("Finger: " + finger.fingerType + ", bone: " + boneIndex + ", angle to palm direction: " + angle);
//			
//			if (angle < 0) {
//				if (hand.GetLeapHand ().IsRight)
//					return true;
//				else
//					return false;
//			}
//				
//
//		} else if (fingerIndex > 0) {
//
//			Vector3 projPlane = Vector3.Cross(palmNormal, palmDir).normalized;
//			Vector3 projVector = Math3d.ProjectVectorOnPlane(projPlane,fingerDir).normalized;
//			
//			
//			 float angle = Math3d.SignedVectorAngle(palmNormal,projVector,projPlane);
//
//			//Debug.Log ("Finger: " + finger.fingerType + ", bone: " + boneIndex + ", angle to palm direction: " + angle);
//
//			if (angle < 0) {
//				
//				if (hand.GetLeapHand ().IsRight)
//					return true;
//				else
//					return false;
//			}
//				
//		}
//
//
//		return false;
//
//
//
//	}
//	
//	public static bool IsThumbBent(HandModel hand, int boneIndex){
//
//		return IsFingerBent (hand,0, boneIndex);
//	
//	}
//	
//	public static bool IsIndexFingerBent(HandModel hand, int boneIndex){
//
//		return IsFingerBent (hand, 1, boneIndex);
//	}
//
//	public static bool IsMiddleFingerBent(HandModel hand, int boneIndex){
//
//		return IsFingerBent (hand, 2, boneIndex);
//
//	}
//
//	public static bool IsRingFingerBent(HandModel hand, int boneIndex){
//
//		return IsFingerBent (hand, 3, boneIndex);
//
//	}
//
//	public static bool IsLittleFingerBent(HandModel hand, int boneIndex){
//
//		return IsFingerBent (hand, 4, boneIndex);
//
//	}
//
//	
//	public static bool IsGestureFist(HandModel hand){
//
//		return IsThumbBent (hand, 3) &&  IsIndexFingerBent (hand, 3) && IsMiddleFingerBent (hand, 3) && IsRingFingerBent (hand, 3)
//			&& IsLittleFingerBent (hand, 3);
//
//	}
//
//	public static bool IsGestureHighFive(HandModel hand){
//
//		return !IsThumbBent (hand, 3) && FourFingersStraight (hand);
//
//	}


}

//
//	protected virtual void HandleGesture(Gesture gesture, HandModel handModel){
//
//		if (gesture.Type == Gesture.GestureType.TYPE_CIRCLE) {
//			
//			if (gesture.State == Gesture.GestureState.STATESTART) {
//
//				
//			} else if (gesture.State == Gesture.GestureState.STATEUPDATE) {
//				
//		
//			} else if (gesture.State == Gesture.GestureState.STATE_STOP) {
//				
//		
//			}
//			
//		} else if (gesture.Type == Gesture.GestureType.TYPESWIPE) {
//			
//			if( gesture.State ==  Gesture.GestureState.STATESTART){
//				
//				
//			}else if (gesture.State == Gesture.GestureState.STATEUPDATE){
//				
//		
//			}else if (gesture.State == Gesture.GestureState.STATESTOP){
//				
//				
//			}
//			
//		} else if (gesture.Type == Gesture.GestureType.TYPEKEYTAP) {
//			
//			//Debug.Log ("Tap");
//			
//		}
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//}
