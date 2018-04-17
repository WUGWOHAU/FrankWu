using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	[SerializeField] private int DAMAGE = 5;
	[SerializeField] private float SPEED = 8.0f;
	private Vector2 _dir;
	private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
        _dir = ( GameObject.Find( "player" ).transform.position - transform.position ).normalized;
		_rb = GetComponent<Rigidbody2D> ( );
		Destroy( gameObject, 3.0f ); // 撃たれて3秒後に消去
	}
	
	// Update is called once per frame
	void Update () {
		_rb.velocity = _dir * SPEED;
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if ( col.gameObject.tag == "Player" ) {
			col.GetComponent<plyaer> ( ).damage( DAMAGE );
			Destroy (gameObject);
		}
	}
}
