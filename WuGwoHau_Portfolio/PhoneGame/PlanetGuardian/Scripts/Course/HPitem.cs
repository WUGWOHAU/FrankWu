using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPitem : MonoBehaviour {

    GameObject Player;
    public float HPpoint = 50.0f;

	// Use this for initialization
	void Start () {
		this.Player = GameObject.FindWithTag( "Player" );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D( Collision2D collision ) {

        if ( collision.gameObject.CompareTag( "Player" ) ) {
           Player.GetComponent<PlayerController>().Hp += HPpoint;
            Destroy( gameObject );
        }
    }

}
