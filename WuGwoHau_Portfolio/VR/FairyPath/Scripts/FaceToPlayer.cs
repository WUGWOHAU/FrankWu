using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToPlayer : MonoBehaviour {

    [SerializeField]
    Transform Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( Player != null ) {
            transform.LookAt( transform.position + Player.transform.rotation * Vector3.left,
                                                   Player.transform.rotation * Vector3.up );
        }
	}
}
