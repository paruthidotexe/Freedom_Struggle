///-----------------------------------------------------------------------------
///
/// PixelBacteria
/// 
/// Gameplay
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ParuthidotExE
{
    public class PixelBacteria : MonoBehaviour
    {
        // level
        public LevelMgr levelMgr;
        List<string> commandSequnce = new List<string>();
        List<Vector3> positionStore = new List<Vector3>();
        GameStates gameState;
        GameTimer gameTimer;
        GridData gridData;
        GridData playerGridData;

        int levelWidth = 10;
        int levelHeight = 6;
        // Level Tiles 0 = hole, 1 = floor, 2,3,4..9 = Wall height, 10 = Door, ?128 = player
        int[,] levelTilesData;
        Vector2Int playerPos = Vector2Int.one;
        bool isGameOver = false;

        // Prefabs
        [SerializeField] GameObject Tile_Blue_Prefab;
        [SerializeField] GameObject Tile_Pink_Prefab;

        [SerializeField] VoidChannelSO GameOverEvent = default;
        [SerializeField] VoidChannelSO RestartEvent = default;
        [SerializeField] IntChannelSO ChangePlayerStateEvent = default;

        // LevelRoot
        [SerializeField] GameObject levelMap;
        [SerializeField] GameObject levelTilesRoot;
        [SerializeField] GameObject playerTilesRoot;

        [SerializeField] GameObject playerGreenObj;
        [SerializeField] Bacteria playerGreen;


        private void OnEnable()
        {
            PlayerController.MoveAction += OnMove;
            PlayerController.ChangeStateAction += OnChangeBacteriaState;
            HUDScripts.ChangeStateAction += OnChangeBacteriaState;
            HUDScripts.MoveAction += OnMove;
            HUDScripts.ReverseAction += OnReverse;
            HUDScripts.NextLevelEvent += OnNextLevel;
        }


        private void OnDisable()
        {
            PlayerController.MoveAction -= OnMove;
            PlayerController.ChangeStateAction -= OnChangeBacteriaState;
            HUDScripts.ChangeStateAction -= OnChangeBacteriaState;
            HUDScripts.MoveAction -= OnMove;
            HUDScripts.ReverseAction -= OnReverse;
            HUDScripts.NextLevelEvent -= OnNextLevel;
        }


        void Start()
        {
            levelTilesData = new int[levelWidth, levelHeight];
            //Random.InitState(128);
            gameState = GameStates.InGame;
            GlobalData.OnInit();
            gameTimer = new GameTimer();
            gameTimer.StartTimer();
            Application.targetFrameRate = 60;
            //levelMgr.LoadLevel();
            gridData = LevelDB.GetGridData(levelWidth, levelHeight);
            levelTilesData = gridData.tiles;
            playerGridData = new GridData(levelWidth, levelHeight);
            playerPos = new Vector2Int(4, 0);
            Debug.Log(gridData.GetGridAsString());
            Debug.Log(playerGridData.GetGridAsString());
            CreateBlueLevel();
            playerGreenObj.transform.position = new Vector3(-1, 0, 0);
            GlobalData.moves = 0;
        }


        void Update()
        {
            if (isGameOver)
            {
                levelTilesRoot.transform.position = new Vector3(-5, 0, 0);
                return;
            }
            GlobalData.timePlayed += Time.deltaTime;
            if (gameTimer != null)
                gameTimer.Update();
            if (GlobalData.timePlayed > 180)
            {
                GlobalData.GameOverState = "Time Up";
                isGameOver = true;
            }
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


        public void OnMove(Vector3 direction)
        {
            if (isGameOver)
            {
                return;
            }
            // input -> save command + time -> command Execute
            // undi -> reverse command -> execute
            if (playerGreen.state == BacteriaState.Move)
            {
                if (playerGridData.IsValidTile(playerPos.x + (int)direction.x, playerPos.y + (int)direction.y))
                {
                    GlobalData.moves++;
                    playerPos.x += (int)direction.x;
                    playerPos.y += (int)direction.y;
                    playerGreen.OnMove(direction, Tile_Blue_Prefab, playerTilesRoot, false);
                }
            }
            else if (playerGreen.state == BacteriaState.Clone)
            {
                if (playerGridData.IsValidTile(playerPos.x + (int)direction.x, playerPos.y + (int)direction.y))
                {
                    int tileVal = playerGridData.GetTileValue(playerPos.x + (int)direction.x, playerPos.y + (int)direction.y);
                    bool isClone = true;
                    if (tileVal == 128)
                    {
                        isClone = false;
                    }
                    playerGridData.SetTileValue(playerPos.x + (int)direction.x, playerPos.y + (int)direction.y, 128);
                    GlobalData.moves++;
                    playerPos.x += (int)direction.x;
                    playerPos.y += (int)direction.y;
                    playerGreen.OnMove(direction, Tile_Blue_Prefab, playerTilesRoot, isClone);
                    Debug.Log(playerGridData.GetGridAsString());
                }
            }
            isGameOver = CheckGameOver();
            if (isGameOver)
            {
                GlobalData.GameOverState = "Success";
                StartCoroutine(ChangeToGOScene());
            }
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


        public void CreateBlueLevel()
        {
            Vector3 offset = new Vector3(-0.5f, 0, 0.5f);
            Vector3 origin = new Vector3(0, 0, 0);

            levelTilesRoot.transform.position = Vector3.zero;
            foreach (Transform child in levelTilesRoot.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < levelTilesData.GetLength(0); i++)
            {
                for (int j = 0; j < levelTilesData.GetLength(1); j++)
                {
                    if (levelTilesData[i, j] == 1)
                    {
                        GameObject curObj = GameObject.Instantiate(Tile_Blue_Prefab);
                        curObj.transform.position = new Vector3(i, j, 0);
                        curObj.transform.parent = levelTilesRoot.transform;
                    }
                    if (levelTilesData[i, j] == 0)
                    {
                        GameObject curObj = GameObject.Instantiate(Tile_Pink_Prefab);
                        curObj.transform.position = new Vector3(i, j, 0);
                        curObj.transform.parent = levelTilesRoot.transform;
                    }
                }
            }
            //Blue_LevelMap.transform.position = new Vector3(-5.5f, -0.2f, -5.5f);
            levelTilesRoot.transform.position = new Vector3(-5f, -0.2f, 15f);
            levelTilesRoot.transform.parent = levelMap.transform;
        }


        // player control
        public void OnChangeBacteriaState(string newStateStr)
        {
            if (newStateStr == "1")
            {
                OnChangeBacteriaState(BacteriaState.Move);
            }
            else if (newStateStr == "2")
            {
                OnChangeBacteriaState(BacteriaState.Clone);
            }
            else if (newStateStr == "3")
            {
                OnChangeBacteriaState(BacteriaState.Destruct);
            }
            else if (newStateStr == "4")
            {
                OnChangeBacteriaState(BacteriaState.None);
            }

            if (newStateStr == "[")
            {
                //OnPrevState(state);
                playerGreen.OnPrevState();
            }
            else if (newStateStr == "]")
            {
                //OnNextState(state);
                playerGreen.OnNextState();
            }
        }


        void OnChangeBacteriaState(BacteriaState newState)
        {
            //state = newState;
            ChangePlayerStateEvent.RaiseEvent((int)newState);
            //ChangeFaceReaction(state);
        }


        bool CheckGameOver()
        {
            for (int i = 0; i < levelTilesData.GetLength(0); i++)
            {
                for (int j = 0; j < levelTilesData.GetLength(1); j++)
                {
                    if (levelTilesData[i, j] == 0)
                    {
                        if (playerGridData.tiles[i, j] != 128)
                            return false;
                    }
                }
            }
            return true;
        }


        IEnumerator ChangeToGOScene()
        {
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene("GameOver");
        }


    }


}


// 2d0
// input -> GameplayScript -> commands player -> player execute -> player anim
// move from bacteria to pixel bacteria
//