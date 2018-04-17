using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBatonController : MonoBehaviour {

	[SerializeField]
	GameObject MagicRockCS;

    public bool LeftController = false;
    public bool LeftControllerFlag { get{ return LeftController; } }
    int LeftStoneNum = 0;
    public int LeftMagicStoneNum { get{ return LeftStoneNum; } }

	float TimeCalculate = 0.0f;
	bool TimeFlag = true;

    SteamVR_ControllerManager Player;
    private SteamVR_TrackedObject trackedObject;

    void Start () {
        Player = GameObject.FindObjectOfType<SteamVR_ControllerManager>( );
        trackedObject = Player.left.GetComponent<SteamVR_TrackedObject>( );
    }
	
	void Update () {

        var device = SteamVR_Controller.Input( ( int )trackedObject.index );

        //VRコントローラ処理
        if ( device.GetPress( SteamVR_Controller.ButtonMask.Trigger ) ) {
            LeftController = true;
			//Debug.Log("LEFT TRIGGER");
        } else {
            LeftController = false;
        }

		

    }

   private void OnTriggerEnter( Collider col ) {


		if ( col.gameObject.tag == "MagicStone1" ) {
			MagicRockCS.gameObject.GetComponent<MagicRockManager>().MagicStoneNum = 1;
		}

		if ( col.gameObject.tag == "MagicStone2" ) {
			MagicRockCS.gameObject.GetComponent<MagicRockManager>().MagicStoneNum = 2;
		}

		if ( col.gameObject.tag == "MagicStone3" ) {
			MagicRockCS.gameObject.GetComponent<MagicRockManager>().MagicStoneNum = 3;
		}

		if ( col.gameObject.tag == "MagicStone4" ) {
			MagicRockCS.gameObject.GetComponent<MagicRockManager>().MagicStoneNum = 4;
		}
    }

}
