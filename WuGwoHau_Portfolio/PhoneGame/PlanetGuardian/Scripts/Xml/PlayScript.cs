using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour {

	// Use this for initialization
	void Awak () {

		DeserializedLevelsLoader d = new DeserializedLevelsLoader( );

		if( SceneManager.GetActiveScene().name == "stage1" ){
			d.generateItems( "Levels" );
			return;
			//PauseController.RestartScene = 1;
		}
		if( SceneManager.GetActiveScene().name == "stage2" ) {
			d.generateItems( "Levels2" );
			return;
			//PauseController.RestartScene = 2;
		}

	}
}
