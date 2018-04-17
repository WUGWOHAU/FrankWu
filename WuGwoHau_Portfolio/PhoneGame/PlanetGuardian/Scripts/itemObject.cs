using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObject : MonoBehaviour {

	[SerializeField] private int TRAP_DAMAGE = 10;
	[SerializeField] private int RECOVERY_POINT = 10;
	[SerializeField] private int RECOVERY_ITEM = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if ( col.gameObject.tag == "Player" ) {
			if ( tag == "trap" ) {
				col.GetComponent<plyaer> ( ).damage( TRAP_DAMAGE );
			}
			if ( tag == "recovery_point" ) {
				col.GetComponent<plyaer> ( ).recovery( RECOVERY_POINT );
			}
			if ( tag == "recovery_item" ) {
				col.GetComponent<plyaer> ( ).recovery( RECOVERY_ITEM );
			}
			Destroy( gameObject );
		}
	}
}
