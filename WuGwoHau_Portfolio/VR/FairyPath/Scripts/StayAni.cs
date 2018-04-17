using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayAni : MonoBehaviour {

	[SerializeField]
	Animator Ani;

	float NowTime = 0.0f;

	int RandMax = 300;
	int RandMin = 200;

	bool Flag = false;
	bool Move = true;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		Animation_Event();
		NowTime += Time.timeScale;

		if ( NowTime % Random.Range( RandMin, RandMax) == 0) {
			Move = false;
			if(Flag) {
				Flag = false;
			}else {
				Flag = true;
			}

		} else {
			Move = true;

		}

	}


	void Animation_Event() {
		if( Flag ) {
			Ani.SetBool( "yellowFlag", true);
		} else {			
			Ani.SetBool( "yellowFlag", false);
		}
		if (Move) {
			Ani.SetBool( "Move", true);
		} else {
			Ani.SetBool( "Move", false);
		}


	}
}
