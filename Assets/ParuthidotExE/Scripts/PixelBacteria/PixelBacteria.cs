///-----------------------------------------------------------------------------
///
/// LevelMgr
/// 
/// Load level, assets
/// Generate levels
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public enum BacteriaState
    {
        None = 0,
        Move,
        Clone,
        Destruct,
        Last
    }


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

        int levelWidth = 12;
        int levelHeight = 6;
        // Level Tiles 0 = hole, 1 = floor, 2,3,4..9 = Wall height, 10 = Door, ?128 = player
        int[,] levelTiles;
        Vector2Int playerPos = Vector2Int.one;// j,i
        Vector2Int doorPos = Vector2Int.one;// j,i

        // Prefabs
        public GameObject Tile_Blue_Prefab;
        public GameObject Tile_Pink_Prefab;

        [SerializeField] VoidChannelSO GameOverEvent = default;
        [SerializeField] VoidChannelSO RestartEvent = default;
        [SerializeField] IntChannelSO ChangePlayerStateEvent = default;

        // LevelRoot
        public GameObject LevelMap;
        public GameObject Blue_LevelMap;

        [SerializeField] GameObject playerGreen;
        BacteriaState bacteriaState = BacteriaState.None;

        private void OnEnable()
        {
            PlayerController.MoveAction += OnMove;
            PlayerController.ChangeStateAction += OnChangeBacteriaState;
            HUDScripts.MoveAction += OnMove;
            HUDScripts.ReverseAction += OnReverse;
            HUDScripts.NextLevelEvent += OnNextLevel;
        }


        private void OnDisable()
        {
            PlayerController.MoveAction -= OnMove;
            PlayerController.ChangeStateAction -= OnChangeBacteriaState;
            HUDScripts.MoveAction -= OnMove;
            HUDScripts.ReverseAction -= OnReverse;
            HUDScripts.NextLevelEvent -= OnNextLevel;
        }


        void Start()
        {
            levelTiles = new int[levelWidth, levelHeight];
            //Random.InitState(128);
            gameState = GameStates.InGame;
            GlobalData.OnInit();
            gameTimer = new GameTimer();
            gameTimer.StartTimer();
            Application.targetFrameRate = 30;
            //levelMgr.LoadLevel();
            gridData = LevelDB.GetGridData(levelWidth, levelHeight);
            levelTiles = gridData.tiles;
            playerGridData = new GridData();
            CreateBlueLevel();
            playerGreen.transform.position = new Vector3(0, 0, 0);
            bacteriaState = BacteriaState.Move;
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


        public void OnMove(Vector3 direction)
        {
            //Debug.Log(direction);
            switch (bacteriaState)
            {
                case BacteriaState.None:
                    //playerGreen.transform.position += direction;
                    break;
                case BacteriaState.Move:
                    playerGreen.transform.position += direction;
                    break;
                case BacteriaState.Clone:
                    GameObject newObj = GameObject.Instantiate(playerGreen);
                    newObj.transform.position = playerGreen.transform.position;
                    playerGreen.transform.position += direction;
                    break;
                case BacteriaState.Destruct:
                    //playerGreen.transform.position += direction;
                    break;
            }
        }


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
                OnPrevState(bacteriaState);
            }
            else if (newStateStr == "]")
            {
                OnNextState(bacteriaState);
            }
        }



        void OnChangeBacteriaState(BacteriaState newState)
        {
            bacteriaState = newState;
            ChangePlayerStateEvent.RaiseEvent((int)bacteriaState);
            Bacteria bacteriaScript = playerGreen.GetComponent<Bacteria>();
            bacteriaScript.ChangeFaceReaction(bacteriaState);
        }


        void OnPrevState(BacteriaState newState)
        {
            int curStateVal = (int)newState;
            curStateVal--;
            if (curStateVal < 0)
                curStateVal = (int)(BacteriaState.Last) - 1;
            newState = (BacteriaState)curStateVal;
            Debug.Log(newState);
            OnChangeBacteriaState(newState);
        }


        void OnNextState(BacteriaState newState)
        {
            int curStateVal = (int)newState;
            curStateVal++;
            if (curStateVal > (int)(BacteriaState.Last) - 1)
            {
                curStateVal = 0;
            }
            newState = (BacteriaState)curStateVal;
            OnChangeBacteriaState(newState);
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

            Blue_LevelMap.transform.position = Vector3.zero;
            foreach (Transform child in Blue_LevelMap.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < levelTiles.GetLength(0); i++)
            {
                for (int j = 0; j < levelTiles.GetLength(1); j++)
                {
                    if (levelTiles[i, j] == 1)
                    {
                        GameObject curObj = GameObject.Instantiate(Tile_Blue_Prefab);
                        curObj.transform.position = new Vector3(i, j, 0);
                        curObj.transform.parent = Blue_LevelMap.transform;
                    }
                    if (levelTiles[i, j] == 0)
                    {
                        GameObject curObj = GameObject.Instantiate(Tile_Pink_Prefab);
                        curObj.transform.position = new Vector3(i, j, 0);
                        curObj.transform.parent = Blue_LevelMap.transform;
                    }
                }
            }
            //Blue_LevelMap.transform.position = new Vector3(-5.5f, -0.2f, -5.5f);
            Blue_LevelMap.transform.position = new Vector3(-5f, -0.2f, 15f);
            Blue_LevelMap.transform.parent = LevelMap.transform;
        }


    }


}

