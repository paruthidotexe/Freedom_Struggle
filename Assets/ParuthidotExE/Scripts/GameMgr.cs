///-----------------------------------------------------------------------------
///
/// GameMgr
/// 
/// Main game manager
///
///-----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ParuthidotExE
{
    // Game states
    public enum GameStates
    {
        None = 0,
        MainMenu,
        Loading,
        InGame,
        Pause,
        GameOver
    };


    public class GameMgr : MonoBehaviour
    {
        // level
        public LevelMgr levelMgr;
        List<string> commandSequnce = new List<string>();
        List<Vector3> positionStore = new List<Vector3>();

        GameTimer gameTimer;
        GameStates gameState;


        private void OnEnable()
        {
            PlayerController.MoveAction += OnMoveAction;
            HUDScripts.MoveAction += OnMoveAction;
            HUDScripts.ReverseAction += OnReverse;
            HUDScripts.NextLevelEvent += OnNextLevel;
        }


        private void OnDisable()
        {
            PlayerController.MoveAction -= OnMoveAction;
            HUDScripts.MoveAction -= OnMoveAction;
            HUDScripts.ReverseAction -= OnReverse;
            HUDScripts.NextLevelEvent -= OnNextLevel;
        }


        void Start()
        {
            //Random.InitState(128);
            gameState = GameStates.InGame;
            GlobalData.OnInit();
            levelMgr.LoadLevel();
            gameTimer = new GameTimer();
            gameTimer.StartTimer();
            Application.targetFrameRate = 30;
        }


        void Update()
        {
            GlobalData.timePlayed += Time.deltaTime;
            if (gameTimer != null)
                gameTimer.Update();
        }


        public void OnNextLevel()
        {
            commandSequnce.Clear();
            positionStore.Clear();
            levelMgr.LoadNextLevel();
            levelMgr.SaveLevel();
        }


        public void OnReverse()
        {
        }


        public void OnMoveAction(Vector3 direction)
        {
        }


        public void OnUndoAction()
        {
            // Debug.Log("OnUndoAction :" + direction);
        }


        public void OnGameOver()
        {
        }


        public void OnGamePaused()
        {
        }


        public void OnGameResumed()
        {
        }


    }


}

