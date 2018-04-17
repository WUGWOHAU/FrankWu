﻿ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public enum WAND_STATE {
        IDEL,
        WARP,
        LAND,
        STATUS_MAX
    }

    public enum PLAYER_STATE {
        IDEL,
        WARP,
        DAMAGE
    }

    //Gemの数、種類が欲しい時にintではなくEnumに書き換える
    private int m_gem_num = 0;

    [SerializeField] private GameObject m_Wand;
    [SerializeField] private GemController m_GemController;

    private LineRendererController m_Line_render_contro;
    private bool m_GameIsStart = false;

    public WAND_STATE WandState { get { return m_Wand.GetComponent<WandController>().WandState; } }
    public PLAYER_STATE PlayerState { get { return m_Wand.GetComponent<WandController>().PlayerState; } }
    public int GEM_NUM { get { return m_gem_num; } }

    private void Start() {
        //初期状態で動作しないものをセットをする
        m_GemController.SetGameStart(false);
        m_Wand.GetComponent<WandController>().SetBehaviorActive(false);
        m_Line_render_contro = GameObject.Find( "Wand" ).GetComponent<LineRendererController>( );
        m_Line_render_contro.ColorControllerOFF( );
        m_GameIsStart = false;

}

// Update is called once per frame
void Update ( ) {
        MainManager.GameState main_state = MainManager.CurrentState;
        switch (main_state) {
            case MainManager.GameState.GAME_START:
                //ゲームスタート時に動作するものをセットする

                m_Wand.GetComponent<WandController>().SetBehaviorActive(true);
                m_GemController.SetGameStart(true);
                m_Line_render_contro.ColorControllerON();
                m_GameIsStart = true;
                break;
            case MainManager.GameState.GAME_PLAYING:
                //ゲーム中のアップデート
                if (m_GemController.IsGetGem) {
                    GetGemAction();
                }
                if(!m_GameIsStart)
                {
                    m_Wand.GetComponent<WandController>().SetBehaviorActive(true);
                    m_GemController.SetGameStart(true);
                    m_Line_render_contro.ColorControllerON();
                    m_GameIsStart = true;
                }

                break;
            case MainManager.GameState.GAME_FINISH:
                //ゲーム終了時に消すものはここで消す。
                break;
            case MainManager.GameState.GAME_TIMEUP:
                //リザルト時に消すものはここで消す。
                m_Wand.GetComponent<WandController>().SetBehaviorActive(false);
                m_GemController.SetGameStart(false);
                m_Line_render_contro.ColorControllerOFF();
                break;
        }
    }

    private void DebugCode() {
        if (Input.GetKeyDown(KeyCode.S)) {
            m_Wand.GetComponent<WandController>().SetBehaviorActive(true);
            m_GemController.SetGameStart(true);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            m_Wand.GetComponent<WandController>().SetBehaviorActive(false);
            m_GemController.SetGameStart(false);
        }
        if (m_GemController.IsGetGem) {
            GetGemAction();
        }
    }

    //GemをGetしたときに呼ばれる関数
    private void GetGemAction ( ) {
        m_gem_num++;
        m_GemController.ResetHitState( );
        EventData eventData;
        eventData.gameEvent = GameEvent.EVENT_GEM;
        //need know whether remote wand
        eventData.eventObject = m_GemController.gameObject;
        MainManager.EventTriggered(eventData);
      
    }
}
