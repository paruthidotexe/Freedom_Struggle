///-----------------------------------------------------------------------------
///
/// LevelMgr
/// 
/// Load level, assets
/// Generate levels
///
///-----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ParuthidotExE
{
    public class LevelMgr : MonoBehaviour
    {
        // LevelRoot
        [SerializeField] GameObject LevelMap;
        [SerializeField] LevelData[] levelData;
        [SerializeField] GameObject[] tapAreas;
        [SerializeField] private VoidEventChannelSO onGameOver = default;
        [SerializeField] private VoidEventChannelSO RestartEvent = default;

        [SerializeField] GameObject shapePrefab;
        List<ShapeObject> shapes = new List<ShapeObject>();
        int shapeObjectIndex = 0;
        float nextShapeTime = 2;
        float lastShapeTime = 0;
        // layer mask for clicking
        int layerMask_Shapes = 0;
        int layerMask_TapArea = 0;

        int levelNo = 0;
        public Paths[] paths;
        public bool[] pathStatus;
        int pathIndex = 0;
        bool isLevelReady = false;


        private void OnEnable()
        {
            PlayerController.OnClickedAction += OnShapeClicked;
            ShapeObject.OnMissed += OnMissed;
            ShapeObject.OnSafeAreaHit += OnSafeAreaHit;
            RestartEvent.OnEventRaised += LoadNextLevel;
        }


        private void OnDisable()
        {
            PlayerController.OnClickedAction -= OnShapeClicked;
            ShapeObject.OnMissed -= OnMissed;
            ShapeObject.OnSafeAreaHit -= OnSafeAreaHit;
            RestartEvent.OnEventRaised -= LoadNextLevel;
        }


        void Start()
        {
            lastShapeTime = Time.time;
            layerMask_Shapes = LayerMask.GetMask("Shape");
            layerMask_TapArea = LayerMask.GetMask("TapArea");
            if (Screen.height / Screen.width > 1.7f)
            {
                Camera.main.orthographicSize = 7;
            }
            else if (Screen.height / Screen.width > 2)
            {
                Camera.main.orthographicSize = 8;
            }
        }


        void Update()
        {
            //Debug.Log(Time.time + "  vs  " + lastShapeTime);
            if (Time.time - lastShapeTime >= nextShapeTime && isLevelReady)
            {
                OnSpawnNextShape();
                lastShapeTime = Time.time;
                if (shapeObjectIndex % 5 == 0)
                    EventMgr.Raise_OnLevelUp();
            }
            if (GlobalData.shapesMissed >= 3)
            {
                SceneManager.LoadScene("GameOver");
            }
        }


        public void InitLevel()
        {
            lastShapeTime = Time.time;
            shapeObjectIndex = 0;
            GlobalData.shapesCollected = 0;
            GlobalData.shapesMissed = 0;
        }


        public void CreateLevel()
        {
            levelNo = Random.Range(0, levelData.Length);
            // creating shapes pool
            for (int i = 0; i < levelData[levelNo].totalShapes; i++)
            {
                GameObject curObj = GameObject.Instantiate(shapePrefab);
                ShapeObject shapeScript = curObj.GetComponent<ShapeObject>();
                if (shapeScript != null)
                {
                    shapeScript.Init(levelData[levelNo].GetRandomShapeData());
                }
                curObj.transform.parent = LevelMap.transform;
                curObj.name = "Shape_" + (i).ToString("00");
                shapes.Add(shapeScript);
            }

            pathStatus = new bool[paths.Length];
            for (int i = 0; i < pathStatus.Length; i++)
            {
                paths[i].SetPathId(i);
                pathStatus[i] = false;
            }
            GlobalData.totalShapes = levelData[levelNo].totalShapes;
            isLevelReady = true;
        }


        public void LoadLevel()
        {
            InitLevel();
            CreateLevel();
        }


        public void LoadNextLevel()
        {
            LoadLevel();
        }


        public void SaveLevel()
        {
        }


        public void LoadLevelFromJson()
        {
        }


        void OnSpawnNextShape()
        {
            if (shapeObjectIndex >= shapes.Count)
            {
                shapeObjectIndex = 0;
            }
            int randomPath = GetTrackNumber();
            //Debug.Log("shapes : " + shapeObjectIndex + " vs " + shapes.Count);
            if (randomPath >= 0)
            {
                TapAreaScript tapAreaScript = tapAreas[randomPath].GetComponent<TapAreaScript>();
                tapAreaScript.ApplySpriteData(shapes[shapeObjectIndex].shapeData);
                shapes[shapeObjectIndex].OnSpawn(paths[randomPath]);
                pathStatus[randomPath] = true;
            }
            shapeObjectIndex++;
        }


        public int GetTrackNumber()
        {
            List<int> emptyPaths = new List<int>();
            for (int i = 0; i < pathStatus.Length; i++)
            {
                if (!pathStatus[i])
                    emptyPaths.Add(i);
            }
            if (emptyPaths.Count > 0)
            {
                return emptyPaths[Random.Range(0, emptyPaths.Count)];
            }
            return -1;
        }


        void OnShapeClicked(Vector3 newPos)
        {
            RaycastHit2D[] hitArray = Physics2D.CircleCastAll(newPos, 0.1f, Vector2.zero, 1.0f, layerMask_TapArea);
            if (hitArray.Length > 0)
            {
                //Debug.Log("Clicked : " + hitArray[0].collider.name);
                TapAreaScript tapAreaScript = hitArray[0].collider.GetComponent<TapAreaScript>();
                tapAreaScript.OnClicked();
                // circle cast from the Tap Area
                newPos = hitArray[0].collider.transform.position;
                hitArray = Physics2D.CircleCastAll(newPos, tapAreaScript.radius, Vector2.zero, 1.0f, layerMask_Shapes);
                if (hitArray.Length > 0)
                {
                    ShapeObject shapeScript = hitArray[0].collider.GetComponent<ShapeObject>();
                    shapeScript.OnClicked();
                    GlobalData.shapesCollected++;
                    AudioMgr.Inst.OnPlaySFX(SFXValues.SFX_Ok);
                }
                else
                {
                    AudioMgr.Inst.OnPlaySFX(SFXValues.SFX_Cancel);
                    //Debug.Log("No Hit");
                }
            }
        }


        void OnMissed(int val)
        {
            pathStatus[val] = false;
            AudioMgr.Inst.OnPlaySFX(SFXValues.SFX_Cancel);
        }


        void OnSafeAreaHit(int val)
        {
            pathStatus[val] = false;
            GlobalData.shapesMissed++;
        }


        public void OnRestartGame()
        {
            SceneManager.LoadScene("InGame");
        }


    }


}

