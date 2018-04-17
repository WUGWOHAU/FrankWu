using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour {

	enum STAET {
		STATE_MOVE,
		STATE_SHOT,
		STATE_DETH,
	}
	[SerializeField] private GameObject _item = null;
	[SerializeField] private double _drop_rate = 0.0;
	[SerializeField] private GameObject _bullet = null;
	[SerializeField] private float MOVE_SPEED = 1.0f;
	[SerializeField] private float ATTACK_RANGE_X = 5.0f;
	[SerializeField] private float ATTACK_RANGE_Y = 7.5f;
	private sound _sound = null;
	private float _shot_time;
	private bool _shot;
	private STAET _state;
    private GameObject _player = null;
    private Rigidbody2D _rb;
	private Animator _anim = null;
    private Vector2 _skipped_dir = Vector2.zero;
    
    //private AudioSource SE_e_shooterAttack;

    // Use this for initialization
    void Start () {
		_shot_time = 0.0f;
		_state = STAET.STATE_MOVE;
        _player = GameObject.Find( "player" );
        _rb = GetComponent<Rigidbody2D> ();
		_anim = GetComponent<Animator> ();
		_sound = GameObject.Find ("sound").GetComponent<sound>();
        //SE_e_shooterAttack = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if ( !_player || _player.GetComponent<plyaer>().isDead()) {
			return;
		}

		switch ( _state ) {
			case STAET.STATE_MOVE:
				move( );
				break;
			case STAET.STATE_SHOT:
				shooting( );
				break;
            case STAET.STATE_DETH:
				dead ();
                break;
		}
	}

	void move( ) {
		Vector3 diff = _player.transform.position - transform.position;		//	間合い
		if (((0 <= diff.x & diff.x <= ATTACK_RANGE_X-2) |
			(0 >= diff.x & diff.x >= -ATTACK_RANGE_X + 2)) &
			((0 <= diff.y & diff.y <= ATTACK_RANGE_Y - 2) |
				(0 >= diff.y & diff.y >= -ATTACK_RANGE_Y + 2))) {
			_rb.velocity = -diff.normalized * MOVE_SPEED;
        }
        else if (((0 <= diff.x & diff.x <= ATTACK_RANGE_X) |
		    (0 >= diff.x & diff.x >= -ATTACK_RANGE_X)) &    
		    ((0 <= diff.y & diff.y <= ATTACK_RANGE_Y) |
		    (0 >= diff.y & diff.y >= -ATTACK_RANGE_Y))) {
			_state = STAET.STATE_SHOT;
        } else {
			_rb.velocity = diff.normalized * MOVE_SPEED;
		}

		if (((0 <= diff.x & diff.x <= ATTACK_RANGE_X-2) |
			(0 >= diff.x & diff.x >= -ATTACK_RANGE_X + 2)) &
			((0 <= diff.y & diff.y <= ATTACK_RANGE_Y - 2) |
				(0 >= diff.y & diff.y >= -ATTACK_RANGE_Y + 2))) {
			_rb.velocity = -diff.normalized * MOVE_SPEED;
            shooting2();
        }
    }

	void shooting( ) {
		Vector3 diff = _player.transform.position - transform.position;
		_rb.velocity = diff.normalized * 0;

        _shot_time += Time.deltaTime;

        if ( _shot_time >= 1 && !_shot )
        {
			_sound.playSE ("shoter_attack");
            //SE_e_shooterAttack.Play();
            Instantiate( _bullet, transform.position, Quaternion.identity );
			_shot = true;
        }

        if ( _shot_time >= 2 ) {
			_state = STAET.STATE_MOVE;
			//_anim.SetBool( "walk", true );
            _shot_time = 0;
			_shot = false;
        }
	}
    void shooting2()
    {
        _shot_time += Time.deltaTime;

        if (_shot_time >= 1 && !_shot)
        {
			_sound.playSE ("shoter_attack");
            //SE_e_shooterAttack.Play();
            Instantiate(_bullet, transform.position, Quaternion.identity);
            _shot = true;
        }

        if (_shot_time >= 2)
        {
            _anim.SetBool("walk", true);
            _shot_time = 0;
            _shot = false;
        }
    }

    void dead( ) {
		_rb.velocity = 6 * _skipped_dir;
	}

	void killed( ) {
		_state = STAET.STATE_DETH;
		GetComponent<Collider2D> ( ).isTrigger = true;
		double random = Random.Range( 0, 100 ) / 100.0;
		if ( _item && random < _drop_rate ) {
			Instantiate( _item, transform.position, Quaternion.identity );
		}
		Destroy( gameObject , 2.0f );
        _skipped_dir = transform.position - _player.transform.position;
		_anim.SetBool( "dead", true );
		GameObject boss = GameObject.Find ("BossPop");
		if ( boss ) {
			boss.gameObject.GetComponent<popBoss> ().hate ();
    	}
    }

    void OnCollisionEnter2D( Collision2D col ) {
		if (col.collider.tag == "Player") {
			killed ();
			col.gameObject.GetComponent< plyaer > ().attack ();
		}
		
		if ( col.collider.tag == "wall" ) {
			GetComponent<Collider2D> ( ).isTrigger = true;
		}
	}
	
	void OnTriggerEnter2D( Collider2D col ) {
		if (col.tag == "Player") {
			killed ();
			col.GetComponent< plyaer > ().attack ();
		}
	}

	void OnTriggerStay2D( Collider2D col ) {
		if ( col.tag == "wall" ) {
			GetComponent<Collider2D> ( ).isTrigger = true;
		}
	}
}
