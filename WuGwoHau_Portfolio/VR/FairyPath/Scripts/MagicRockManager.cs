using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRockManager : MonoBehaviour {

    //MigicStoneの処理
	public int MagicStoneNum = 0;

    //RuneのTexture処理
    [SerializeField]
    GameObject Rune_L;
    [SerializeField]
    GameObject Rune_M;
    [SerializeField]
    GameObject Rune_R;

    [SerializeField]
    Sprite[] RuneTextures;

    SpriteRenderer rune_render_L;
    SpriteRenderer rune_render_M;
    SpriteRenderer rune_render_R;

    int currentTexture = 0; //Texture番号

    //TextureAlpha
    float rune_texture_Alpha = 1.0f;

    //魔石の配列
    int[ ] move = { 0, 0, 2 };
    int[ ] attack = { 3, 3, 1 };
	int[ ] summon = { 0, 3, 2 };

	public int count = 0; //魔石の順番
	public int move_count = 0;
    public int atk_count = 0;
	public int smn_count = 0;

	//Flag処理
	public bool canAttack = false;
	public bool Attacking = false;
    public bool Moving = false;
	public bool isSummoned = false;
    public bool MoveFlag { get { return Moving; } }
    public bool PressKey = true; //キーボード入力FLAG
	public bool ReCol = false;

    void Start() {
        //get魔石のMaterial
        rune_render_L = Rune_L.gameObject.GetComponent<SpriteRenderer>();
        rune_render_M = Rune_M.gameObject.GetComponent<SpriteRenderer>();
        rune_render_R = Rune_R.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {

		if (ReCol == true) {
			//Debug.Log ("Count cleared");
			AlphaTiming ();
		}

		VRControllerMode( );
        TestKeyMode( );
        RuneTexture( );
		//Debug.Log ("Current attack state" + Attacking );
		//Debug.Log ("Current move state" + Moving );
    }

    void Keypress( int k ) {

        count++;

		// MOVE
        if ( move[move_count] == k ) {

            move_count++;

            if ( move_count == 3 ) {
                Moving = true;
				PressKey = false;
				//Debug.Log ("Move command");
            } 
        } else {
            move_count = 0;
			PressKey = true;
			Moving = false;
        }

		//ATTACK
        if ( attack[ atk_count ] == k ) {
            atk_count++;

            if ( atk_count == 3 && canAttack == true ) {

				Attacking = true;
				PressKey = false;
                //Debug.Log( "Attack command" );
            } 
        } else {
            atk_count = 0;
			PressKey = true;
			Attacking = false;
        }

		//SUMMON
		if ( summon[ smn_count ] == k ) {
			smn_count++;

			if ( smn_count == 3 ) {

				isSummoned = true;
				PressKey = false;
				//Debug.Log( "Summon command" );
			} 
		} else {
			smn_count = 0;
			PressKey = true;
		}
    }

    //VRMode
    void VRControllerMode( ) {

		if ( MagicStoneNum == 1 ) {
            Keypress(0);
            currentTexture = 1;

			if( MagicStoneNum == 1 ){
				MagicStoneNum = 0;
			}

        }

		if ( MagicStoneNum == 2 ) {
            Keypress(1);
            currentTexture = 2;

			if( MagicStoneNum == 2 ){
				MagicStoneNum = 0;
			}
        }

		if ( MagicStoneNum == 3 ) {
            Keypress(2);
            currentTexture = 3;

			if( MagicStoneNum == 3 ){
				MagicStoneNum = 0;
			}
        }

		if ( MagicStoneNum == 4 ) {
            Keypress(3);
            currentTexture = 4;

			if( MagicStoneNum == 4 ){
				MagicStoneNum = 0;
			}
        }

    }

    //KeyMode
    void TestKeyMode() {
        if ( PressKey == true ) {

            if (Input.GetKeyDown(KeyCode.A)) {
                Keypress(0);
                currentTexture = 1;
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                Keypress(1);
                currentTexture = 2;
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                Keypress(2);
                currentTexture = 3;
            }
            if (Input.GetKeyDown(KeyCode.F)) {
                Keypress(3);
                currentTexture = 4;
            }
        }
    }

    void RuneTexture( ) {

        if ( count == 1 ) {
            rune_render_L.sprite = RuneTextures[currentTexture];
        }
        if ( count == 2 ) {
            rune_render_M.sprite = RuneTextures[currentTexture];
        }
		if (count == 3) {
			rune_render_R.sprite = RuneTextures [currentTexture];
			ReCol = true;
		}
    }

    //RuneAlpha処理
    void AlphaTiming( ) {
		Initialization ();
		if (rune_render_L.color.a >= 0) {
			rune_texture_Alpha -= 0.003f;
		} else if (rune_render_L.color.a <= 0) {
			Initialization ();
		}

    }

    //初期化
    void Initialization() {
		//Debug.Log ("Initialization");
        count = 0;
		move_count = 0;
		atk_count = 0;
		smn_count = 0;
        PressKey = true;
		ReCol = false;
        rune_render_L.sprite = RuneTextures[0];
        rune_render_M.sprite = RuneTextures[0];
        rune_render_R.sprite = RuneTextures[0];
    }
}
