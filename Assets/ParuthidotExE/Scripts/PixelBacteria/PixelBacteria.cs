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
    public class PixelBacteria : MonoBehaviour
    {
        // level
        public LevelMgr levelMgr;
        List<string> commandSequnce = new List<string>();
        List<Vector3> positionStore = new List<Vector3>();
        GameStates gameState;
        GameTimer gameTimer;
        GridData gridData;

        int levelWidth = 12;
        int levelHeight = 6;
        // Level Tiles 0 = hole, 1 = floor, 2,3,4..9 = Wall height, 10 = Door, ?128 = player
        int[,] levelTiles;
        Vector2Int playerPos = Vector2Int.one;// j,i
        Vector2Int doorPos = Vector2Int.one;// j,i

        // Prefabs
        public GameObject Tile_Blue_Prefab;
        public GameObject Tile_Pink_Prefab;

        [SerializeField] private VoidEventChannelSO onGameOver = default;
        [SerializeField] private VoidEventChannelSO RestartEvent = default;

        // LevelRoot
        public GameObject LevelMap;
        public GameObject Blue_LevelMap;

        [SerializeField] GameObject playerGreen;

        private void OnEnable()
        {
            PlayerController.OnMoveAction += OnMoveAction;
            HUDScripts.MoveAction += OnMoveAction;
            HUDScripts.ReverseAction += OnReverse;
            HUDScripts.NextLevelEvent += OnNextLevel;
        }


        private void OnDisable()
        {
            PlayerController.OnMoveAction -= OnMoveAction;
            HUDScripts.MoveAction -= OnMoveAction;
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
            CreateBlueLevel();
            playerGreen.transform.position = new Vector3(0, 0.0f, 0);
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
            Debug.Log(direction);
            GameObject newObj = GameObject.Instantiate(playerGreen);
            newObj.transform.position = playerGreen.transform.position;
            playerGreen.transform.position += direction;
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