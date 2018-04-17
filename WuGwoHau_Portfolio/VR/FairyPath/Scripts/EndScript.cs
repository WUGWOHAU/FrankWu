using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScript : MonoBehaviour {

    [SerializeField]
    GameObject FireWorks;
	[SerializeField]
	GameObject EndTexture;
    [SerializeField]
    float AlphaLevel = 0.0f;
	[SerializeField]
	float TimeEnd = 0.0f;

    bool EndFlag = false;
    SpriteRenderer EndRenderer;

	// Use this for initialization
	void Start () {

        EndRenderer = EndTexture.gameObject.GetComponent<SpriteRenderer>();

    }

    void Update( ) {
        if ( EndFlag == true ) {
            AlphaLevel += 0.01f;
			EndEvent ();
			Debug.Log (TimeEnd);
            TextureAlpha( );
            FireWorks.SetActive(true);
        }
    }

    void TextureAlpha( ) {
        EndRenderer.color = new Color( 255, 255, 255, AlphaLevel);
    }

    private void OnTriggerEnter( Collider col ) {

		if( col.tag == "Player") {
            EndFlag = true;
        }

	}

	void EndEvent(){

		TimeEnd += Time.timeScale;

		if( TimeEnd >= 600 ){
			SceneManager.LoadScene( "Start" );
		}

	}

}
