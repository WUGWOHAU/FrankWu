using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRotation : MonoBehaviour {

	[SerializeField]LeftBatonController leftController;
	[SerializeField]RightBatonController rightController;
	
	Vector3 rotationMaskLeft = new Vector3(0,-1,0);
	Vector3 rotationMaskRight = new Vector3(0,1,0);
	[SerializeField] float rotationSpeed = 5f;
	[SerializeField] Transform rotateAroundObject;


	private void FixedUpdate( ) {
		Rotate();
	}

	void Rotate() {
		if(Input.GetKey(KeyCode.LeftArrow) || leftController.LeftController == true) {
			//Rotate left
			Debug.Log("Left");

			if(rotateAroundObject) {
			transform.RotateAround(rotateAroundObject.transform.position, rotationMaskLeft, rotationSpeed * Time.deltaTime);
			}
			
		}

		if(Input.GetKey(KeyCode.RightArrow) ||  rightController.RightController == true) {
			//Rotate left
			Debug.Log("Left");

			if(rotateAroundObject) {
			transform.RotateAround(rotateAroundObject.transform.position, rotationMaskRight, rotationSpeed * Time.deltaTime);
			}
			
		}
	}
}
