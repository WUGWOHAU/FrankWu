using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Vector2 Speed = new Vector2( 0.05f, 0.05f );
	public Slider HpBar; //HPGUI Controller

    public float MaxHP;
    public float Hp;

	public bool CourseFlag = true;

    void Start( ) {

		//this.HpBar = GameObject.FindGameObjectWithTag( "HP" );

        Time.timeScale = 1f;


    }
    
    void Update( ) {

        Hp -= Time.deltaTime;

        Move( );
        HpBarController( );
    }

    void Move( ) {

        Vector2 Position = transform.position;

        if (Input.GetKey( KeyCode.LeftArrow ) ) {
            Position.x -= Speed.x;
        }
        if (Input.GetKey( KeyCode.RightArrow ) ) {
            Position.x += Speed.x;
        }
        if ( Input.GetKey( KeyCode.UpArrow ) ) {
            Position.y += Speed.y;
        }
        if ( Input.GetKey( KeyCode.DownArrow ) ) {
            Position.y -= Speed.y;
        }

        transform.position = Position;
    }

    void OnCollisionEnter2D( Collision2D collision ) {

        if ( collision.gameObject.CompareTag( "enemy" ) ) {
            collision.collider.enabled = false;
			CourseFlag = false;
        }

    }

	void HpBarController( ) {
		

		HpBar.value = Hp;

		if ( Hp >= MaxHP ) {

			Hp = MaxHP;

        }

        if ( Hp <= 0f ) {
            Speed = new Vector2( 0, 0 );
            Time.timeScale = 0f;
        }
    }

}
