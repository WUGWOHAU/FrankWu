using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class sword : MonoBehaviour {

	enum STATE {
		STATE_MOVE,
		STATE_ATTACK,
		STATE_DETH,
	}
	
	[SerializeField] private GameObject _item = null;
	[SerializeField] private double _drop_rate = 0.0;
	[SerializeField] private int POWER = 3;
	[SerializeField] private float MOVE_SPEED = 4.5f;
	private sound _sound = null;
	private float ATTCK_RANGE = 4;
	private float MOVE_TIME = 2;
	private float _move_count;
	private float _attack_count;
	private STATE _state;
	private Rigidbody2D _rb;
    private GameObject _player = null;
	private Animator _anim = null;
    private Vector2 _skipped_dir = Vector2.zero;
    
    //private AudioSource SE_e_nearAttack;

    // Use this for initialization
    void Start () {
		_rb = GetComponent<Rigidbody2D> ( );
        _player = GameObject.Find( "player" );
		_move_count = 0;
		_attack_count = 0;
		_state = STATE.STATE_MOVE;
		_anim = GetComponent<Animator> ();
		_sound = GameObject.Find ("sound").GetComponent< sound > ();
        //SE_e_nearAttack = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if ( !_player || _player.GetComponent<plyaer>().isDead()) {
			return;
		}
		
		updateState( );
		switch ( _state ) {
			case STATE.STATE_MOVE:
				move( );
				break;
			case STATE.STATE_ATTACK:
				attack( );
				break;
			case STATE.STATE_DETH:
				dead ();
        	    break;
		}
	}

	void updateState( ) {
		if ( _state == STATE.STATE_DETH ) {
			return;
		}

		Vector2 diff = _player.transform.position - transform.position;
		if ( diff.magnitude < ATTCK_RANGE && _move_count > MOVE_TIME ) {
			_state = STATE.STATE_ATTACK;
			_anim.SetBool( "attack", true );
			_move_count = 0f;
		}

		if ( _state == STATE.STATE_ATTACK &&
			 _attack_count > _anim.GetCurrentAnimatorStateInfo(0).length ) {
			_state = STATE.STATE_MOVE;
			_anim.SetBool( "attack", false );
			_attack_count = 0.0f;
		}
	}

	void move( ) {
		_move_count += Time.deltaTime;
		Vector2 diff = _player.transform.position - transform.position;
		if ( diff.magnitude > ATTCK_RANGE ) {
			_rb.velocity = diff.normalized * MOVE_SPEED;
		} else {
			_rb.velocity = Vector2.zero;
		}
	}

	void attack( ) {
		_sound.playSE ("sword_attack");
		//SE_e_nearAttack.Play();

        _attack_count += Time.deltaTime;
		_rb.velocity = Vector2.zero;
		
		if ( _state == STATE.STATE_ATTACK &&
			 _attack_count > _anim.GetCurrentAnimatorStateInfo(0).length ) {
			Vector2 distance = _player.transform.position - transform.position;
			if ( distance.magnitude < ATTCK_RANGE ) {
				_player.GetComponent<plyaer> ().damage( POWER );
			}
			_state = STATE.STATE_MOVE;
			_anim.SetBool( "attack", false );
			_attack_count = 0.0f;
		}
	}

	void dead( ) {
		_rb.velocity = 6 * _skipped_dir;
	}

	void killed( GameObject player ) {
		_state = STATE.STATE_DETH;
		_anim.SetBool( "dead", true );
		GetComponent<Collider2D> ( ).isTrigger = true;
		plyaer script = player.GetComponent< plyaer > ();
		if ( script ) { script.attack( ); }
		double random = Random.Range( 0, 100 ) / 100.0;
		if ( _item && random < _drop_rate ) {
			Instantiate( _item, transform.position + new Vector3(2, 2, 0), Quaternion.identity );
		}
		Destroy( gameObject, 2.0f );
        _skipped_dir = transform.position - _player.transform.position;
		GameObject boss = GameObject.Find ("BossPop");
		if ( boss ) {
			boss.gameObject.GetComponent<popBoss> ().hate ();
    	}
	}

    void OnCollisionEnter2D( Collision2D col ) {
		if ( col.collider.tag == "wall" ) {
			GetComponent<Collider2D> ( ).isTrigger = true;
		}
		if (col.collider.tag == "Player") {
			killed ( col.gameObject );
		}
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if (col.tag == "Player") {
			killed ( col.gameObject );
		}
	}

	void OnCollisionStay2D( Collision2D col ) {
		if ( col.collider.tag == "wall" ) {
			GetComponent<Collider2D> ( ).isTrigger = true;
		}
	}
}
