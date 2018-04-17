using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToStart : MonoBehaviour {

    [SerializeField]
    Transform LookToPlayer;

    private void Start()
    {
        LookToPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update( ) {
        if ( LookToPlayer != null ) {
            transform.LookAt( transform.position + LookToPlayer.transform.rotation * Vector3.left,
                                                   LookToPlayer.transform.rotation * Vector3.up );
        }

		if( Input.GetKey( KeyCode.Space) ) {

			SceneManager.LoadScene( "MainGameScene" );

		}

    }

	/*private void OnTriggerEnter( Collider col ) {

        if ( col.gameObject.tag == "Player" ) {
			//Debug.Log("Start!!");
            SceneManager.LoadScene( "MainGameScene" );
        }

    }*/


}
