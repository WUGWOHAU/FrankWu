using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {

	[SerializeField]
	GameObject DialogueBox; //対話BOX
    [SerializeField] Text nameText; //ＮＰＣ NAME
    public Text NameTextGet { get { return nameText; } }
    [SerializeField] Text dialogueText; //ＮＰＣ スクリプト
    public Text DialogueTextGet { get { return dialogueText; } }

	//EVENT物件何番目に執行する
	//移動ーEVENT
	[SerializeField]
	GameObject MovePitcture;
	[SerializeField]
	int MovePitctureDialogueStart = 0;

	//HPーEVENT
	[SerializeField]
	GameObject HpItem;
	[SerializeField]
	GameObject HpTarget;
	[SerializeField]
	int HpItemTargetDialogueStart = 0;

	//敵ーEVENT
	[SerializeField]
	GameObject CourseEnemy;
	[SerializeField]
	GameObject EnemyTarget;
	[SerializeField]
	int EnmeyTargetDialogueStart = 0;

	//罠ーEVENT
	[SerializeField]
	GameObject TropItem;
	[SerializeField]
	GameObject TropTarget;
	[SerializeField]
	int TropTargetDialogueStart = 0;

	GameObject Player  = null;

	private Queue<string> sentences;
	private Queue<string> Healsentences;
	private Queue<string> Attacksentences;
	private Queue<string> Endsentences;

	private CourseScript course_Dialogue;

	Vector2 CourseEnemyPosition;
	Vector2 CourseTrapPosition;
	Vector2 CourseHPItemPosition;

	void Start () {
		this.Player = GameObject.FindWithTag( "Player" );
		sentences = new Queue<string>();
		Healsentences = new Queue<string>();
		Attacksentences = new Queue<string>();
		Endsentences = new Queue<string>();
		course_Dialogue = GetComponent<CourseScript>();
		CourseEnemyPosition = new Vector2 ( 0, 3.0f );
		CourseTrapPosition = new Vector2 ( 0, 0.1f );
		CourseHPItemPosition = new Vector2 ( 2.3f, 2.22f );

		EnemyTarget.SetActive ( false );
		MovePitcture.SetActive ( false );
		TropTarget.SetActive ( false );
		HpTarget.SetActive ( false );

	}

	public void StartDialogue( Dialogue dialogue ) {

		nameText.text = dialogue.name;
		sentences.Clear();
		Healsentences.Clear ();
		Attacksentences.Clear ();
		Endsentences.Clear();
		//MOVEセリフスタート
		if (course_Dialogue.MoveFlag == true ) {
            Time.timeScale = 0; //ゲーム時間止める
			Player.GetComponent<plyaer>( ).stop( );//キャラ止める
			DialogueBox.SetActive(true);
			foreach (string sentence in dialogue.sentences) {
				sentences.Enqueue(sentence);
			}
		}

		//HEALセリフスタート
		if ( course_Dialogue.HealFlag == true ) {
			
			Time.timeScale = 0; //ゲーム時間止める
			Player.GetComponent<plyaer>( ).stop( );//キャラ止める
			DialogueBox.SetActive(true);
			foreach (string Healsentence in dialogue.Healsentences) {
				Healsentences.Enqueue(Healsentence);
			}
		}

		//Attackセリフスタート
		if ( course_Dialogue.AttackFlag == true ) {
			Time.timeScale = 0;
			Player.GetComponent<plyaer>( ).stop( );//キャラ止める
			DialogueBox.SetActive(true);
			foreach (string Attacksentence in dialogue.Attacksentences) {
				Attacksentences.Enqueue(Attacksentence);
			}
		}

		//ENDセリフスタート
		if (course_Dialogue.EndFlag == true) {
			DialogueBox.SetActive(true);
			Player.GetComponent<plyaer>( ).stop( );//キャラ止める
			foreach (string Endsentence in dialogue.Endsentences) {
				Endsentences.Enqueue(Endsentence);
			}
		}
		DisplayNextSentence();

	}

	//NEXT BUTTON
	public void DisplayNextSentence( ) {

		//もしCount = 0 to END
		if ( sentences.Count == 0 && course_Dialogue.MoveFlag == true ) {
			EndDialogue();
			return;
		}
		if ( Healsentences.Count == 0 && course_Dialogue.HealFlag == true ) {
			EndDialogue();
			return;
		}
		if ( Attacksentences.Count == 0 && course_Dialogue.AttackFlag == true ) {
			EndDialogue();
			return;
		}

		if( Endsentences.Count == 0 && course_Dialogue.EndFlag == true) {
			//終わった、マップに移動します
			SceneManager.LoadScene("select");
			EndDialogue();
			return;
		}

		//セリフ処理
		//MOVEセリフ
		if (course_Dialogue.MoveFlag == true) {
			string sentence = sentences.Dequeue();
			dialogueText.text = sentence;
			//セリフの何番目に執行する
			if( sentences.Count == MovePitctureDialogueStart ){
				MovePitcture.SetActive ( true );
			}
		}

		//HPセリフ
		if (course_Dialogue.HealFlag == true) {
			string Healsentence = Healsentences.Dequeue();
			dialogueText.text = Healsentence;
			//セリフの何番目に執行する
			if ( Healsentences.Count == HpItemTargetDialogueStart ) {
				plyaer player = Player.GetComponent<plyaer> ();
				print (player == null);
				player.damage( 40 );
				Instantiate( HpItem, CourseHPItemPosition, new Quaternion( 0, 0, 0, 0 ) );
				HpTarget.transform.position = CourseHPItemPosition;
				HpTarget.SetActive ( true );
			}
		}

		//Enemyセリフ
		if (course_Dialogue.AttackFlag == true) {

			string Attacksentence = Attacksentences.Dequeue();
			dialogueText.text = Attacksentence;
			//セリフの何番目に執行する
			if ( Attacksentences.Count == EnmeyTargetDialogueStart ) {
				Instantiate( CourseEnemy, CourseEnemyPosition, new Quaternion(0, 0, 0, 0));
				EnemyTarget.transform.position = CourseEnemyPosition;
				EnemyTarget.SetActive ( true );
			}
		}

		//Endセリフ
		if ( course_Dialogue.EndFlag == true ) {
			string Endsentence = Endsentences.Dequeue();
			dialogueText.text = Endsentence;
			//セリフの何番目に執行する
			if ( Endsentences.Count == TropTargetDialogueStart ) {
				Instantiate( TropItem, CourseTrapPosition, new Quaternion(0, 0, 0, 0));
				TropTarget.transform.position = CourseTrapPosition;
				TropTarget.SetActive ( true );
			}
		}

	}

	void EndDialogue( ) {

		if ( course_Dialogue.MoveFlag == true ) {
			Time.timeScale = 1; //ゲーム時間スタート
			course_Dialogue.MoveingTime = true;
			DialogueBox.SetActive(false);
			MovePitcture.SetActive ( false );
		}

		if (course_Dialogue.HealFlag == true ) {
			Time.timeScale = 1;//ゲーム時間スタート
			course_Dialogue.HealTime = true;
			DialogueBox.SetActive(false);
			HpTarget.SetActive ( false );
		}

		if (course_Dialogue.AttackFlag == true ) {
			Time.timeScale = 1;//ゲーム時間スタート
			course_Dialogue.AttackTime = true;
			DialogueBox.SetActive(false);
			EnemyTarget.SetActive ( false );
		}

	}
}
