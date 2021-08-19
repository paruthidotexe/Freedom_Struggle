///-----------------------------------------------------------------------------
///
/// JigsawCreator
/// 
/// Jigsaw Creator
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


namespace ParuthidotExE
{
    public class JigsawCreator : MonoBehaviour
    {
        [SerializeField] Vector2Int gridDimension;
        GridData gridData;
        [SerializeField] GameObject tilePrefabEmpty;
        [SerializeField] Material tileBgMat;
        [SerializeField] GameObject tilePrefab;
        [SerializeField] GameObject[] jigsawTiles;
        [SerializeField] GameObject levelMap;
        [SerializeField] GameObject jigsawBoardBg;
        [SerializeField] GameObject jigsawBoard;
        [SerializeField] GameObject shelfLeft;
        [SerializeField] GameObject shelfRight;
        Shelf3D shelfLeftScript;
        Shelf3D shelfRightScript;

        [SerializeField] JigsawPicData[] jigsawPicDataArray;
        JigsawPicData jigsawPicData;

        [SerializeField] GameObject gamePanel;
        [SerializeField] GameObject gameOverPanel;

        [SerializeField] GameObject bgTilesOn;
        [SerializeField] GameObject bgTilesOff;
        bool isBgTilesOn = false;

        JigPiece prevPiece;
        GridData solvedData;

        float aspectRatio = 1;
        float aspectWidth = 10;
        float aspectHeight = 10;
        float jigsawWidth = 10;

        public TMP_Text piecesCountText;
        public TMP_Text timerText;
        public TMP_Text movesText;

        public TMP_Text GO_piecesCountText;
        public TMP_Text GO_timerText;
        public TMP_Text GO_movesText;
        TimeSpan timeSpan;
        bool isGameOver = false;

        private void OnEnable()
        {
            InputMgr.ClickedAction += OnClickEvent;
        }


        private void OnDisable()
        {
            InputMgr.ClickedAction -= OnClickEvent;
        }


        void Start()
        {
            InitGame(gridDimension.x, gridDimension.y);
        }


        void Update()
        {
            if (isGameOver)
                return;
            if (IsGameWon())
                return;
            GlobalData.timePlayed += Time.deltaTime;
            if (timerText != null)
                timerText.text = "Time\n" + GetTimeAsString();
            if (movesText != null)
                movesText.text = "Moves\n" + GlobalData.moves;
        }


        void InitGame(int gridX, int gridY)
        {
            AudioMgr.Inst.OnPlayMusic();

            aspectWidth = jigsawWidth;
            aspectHeight = jigsawWidth;
            jigsawPicData = jigsawPicDataArray[0];

            for (int i = 0; i < jigsawPicDataArray.Length; i++)
            {
                if (jigsawPicDataArray[i].ID == GlobalData.selectedPicID)
                    jigsawPicData = jigsawPicDataArray[i];
            }
            if (jigsawPicData.aspectHeight > jigsawPicData.aspectWidth)
            {
                aspectHeight = 10;
                aspectWidth = jigsawPicData.aspectWidth * 10.0f / jigsawPicData.aspectHeight;
            }
            else if (jigsawPicData.aspectHeight < jigsawPicData.aspectWidth)
            {
                aspectWidth = 10;
                aspectHeight = jigsawPicData.aspectHeight * 10.0f / jigsawPicData.aspectWidth;
            }
            else
            {
                aspectHeight = 10;
                aspectWidth = 10;
            }

            gridX = (int)jigsawPicData.aspectWidth;
            gridY = (int)jigsawPicData.aspectHeight;

            // for using same 4x4 for 4x3
            //aspectHeight = 10;
            //aspectWidth = 10;
            //gridX = (int)jigsawPicData.aspectWidth;
            //gridY = (int)jigsawPicData.aspectHeight;

            if (gridX == 1)
            {
                gridX = 4;
                gridY = 4;
            }
            if (gridX < 3)
                gridX = 3;
            if (gridY < 3)
                gridY = 3;

            if(UnityEngine.Random.Range(0,2) == 0)
            {
                gridX = 4;
                gridY = 4;
            }
            else
            {
                gridX = 5;
                gridY = 5;
            }

            gridDimension.x = gridX;
            gridDimension.y = gridY;
            gridData = new GridData(gridDimension.x, gridDimension.y);
            solvedData = new GridData(gridDimension.x, gridDimension.y);
            shelfLeftScript = shelfLeft.GetComponent<Shelf3D>();
            shelfRightScript = shelfRight.GetComponent<Shelf3D>();
            shelfLeftScript.Init();
            shelfRightScript.Init();
            DestroyAllObjects();
            CreaeteJigsaw();

            // GlobalData
            GlobalData.timePlayed = 0;
            GlobalData.moves = 0;
            GlobalData.piecesCount = gridX * gridY;
            // UI
            OnStartGameUI();
            piecesCountText.text = "Pieces\n" + GlobalData.piecesCount;
            timerText.text = "Time\n" + (int)GlobalData.timePlayed;
            movesText.text = "Moves\n" + GlobalData.moves;
            isGameOver = false;
        }


        public void CreaeteJigsaw()
        {
            //if (jigsawPicData.aspectWidth == 1 && jigsawPicData.aspectHeight == 1)
            CreaeteJigsaw_4x4();
            //if (jigsawPicData.aspectWidth == 1 && jigsawPicData.aspectHeight == 1)
            //    CreaeteJigsaw_4x4();
            OnHideBgTiles();
        }


        void CreaeteJigsaw_4x4()
        {
            float tileWidth = aspectWidth / gridData.width;
            float tileHeight = aspectHeight / gridData.height;
            gridData.orgin.x = -aspectWidth / 2 + tileWidth / 2.0f;
            gridData.orgin.y = -aspectHeight / 2 + tileHeight / 2.0f;
            shelfLeftScript.SetRowHeight(tileHeight);
            shelfRightScript.SetRowHeight(tileHeight);
            int tileCount = 0;
            for (int i = 0; i < gridData.width; i++)
            {
                for (int j = 0; j < gridData.height; j++)
                {
                    // bg empty tiles
                    GameObject curBgObj = GameObject.Instantiate(jigsawTiles[i * gridData.height + j]);
                    JigPiece curBgPiece = curBgObj.GetComponent<JigPiece>();
                    curBgPiece.x = i;
                    curBgPiece.y = j;
                    curBgPiece.ID = tileCount + 1;
                    curBgPiece.UpdateMaterials(tileBgMat, jigsawPicData.texture2D, true);
                    curBgObj.transform.position = new Vector3(gridData.orgin.x + i * tileWidth, gridData.orgin.y + j * tileHeight, 0.2f);
                    curBgObj.transform.parent = jigsawBoardBg.transform;
                    curBgObj.transform.localScale = new Vector3(tileWidth, tileHeight, 1);
                    curBgObj.name = "JigTileBG_" + i + "_" + j;

                    // Playable tiles
                    GameObject curObj = jigsawTiles[i * gridData.height + j];
                    // same as in answer grid
                    //curObj.transform.position = new Vector3(curTray.transform.position.x + i * tileWidth + i * 0.1f, curTray.transform.position.y + j * tileHeight + j * 0.1f + 2.0f, 0);
                    curObj.transform.localScale = new Vector3(tileWidth, tileHeight, 1);
                    curObj.name = "JigTile_" + i + "_" + j;
                    if (UnityEngine.Random.Range(0, 2) == 1)
                    {
                        shelfLeftScript.AddItem(curObj.transform);
                    }
                    else
                    {
                        shelfRightScript.AddItem(curObj.transform);
                    }

                    // add values to script
                    JigPiece curPiece = curObj.GetComponent<JigPiece>();
                    curPiece.x = i;
                    curPiece.y = j;
                    curPiece.UpdateMaterials(jigsawPicData.texture2D);
                    //, new Vector2(i / (float)gridData.width, j / (float)gridData.height), new Vector2(1 / (float)gridData.width, 1 / (float)gridData.height));
                    //MeshRenderer meshRenderer = curObj.GetComponentInChildren<MeshRenderer>();
                    //Debug.Log(meshRenderer);
                    //if (meshRenderer != null)
                    //{
                    //    Vector2 textureOffset = new Vector2(i / (float)gridData.width, j / (float)gridData.height);
                    //    meshRenderer.material.mainTextureScale = new Vector2(1 / (float)gridData.width, 1 / (float)gridData.height);
                    //    meshRenderer.material.mainTextureOffset = textureOffset;
                    //    Debug.Log(textureOffset);
                    //}
                    curPiece.ID = tileCount + 1;
                    gridData.tiles[i, j] = curPiece.ID;
                    tileCount++;

                }
            }
        }


        // old code
        void CreaeteJigsaw_3x3()
        {
            float tileWidth = aspectWidth / gridData.width;
            float tileHeight = aspectHeight / gridData.height;
            gridData.orgin.x = -aspectWidth / 2 + tileWidth / 2.0f;
            gridData.orgin.y = -aspectHeight / 2 + tileHeight / 2.0f;
            shelfLeftScript.SetRowHeight(tileHeight);
            shelfRightScript.SetRowHeight(tileHeight);
            int tileCount = 0;
            for (int i = 0; i < gridData.width; i++)
            {
                for (int j = 0; j < gridData.height; j++)
                {
                    GameObject curObj = GameObject.Instantiate(tilePrefab);
                    // same as in answer grid
                    //curObj.transform.position = new Vector3(curTray.transform.position.x + i * tileWidth + i * 0.1f, curTray.transform.position.y + j * tileHeight + j * 0.1f + 2.0f, 0);
                    curObj.transform.localScale = new Vector3(tileWidth, tileHeight, 1);
                    curObj.name = "JigTile_" + i + "_" + j;
                    if (UnityEngine.Random.Range(0, 2) == 1)
                    {
                        shelfLeftScript.AddItem(curObj.transform);
                    }
                    else
                    {
                        shelfRightScript.AddItem(curObj.transform);
                    }

                    // add values to script
                    JigPiece curPiece = curObj.GetComponent<JigPiece>();
                    curPiece.x = i;
                    curPiece.y = j;
                    curPiece.UpdateMaterials(jigsawPicData.texture2D, new Vector2(i / (float)gridData.width, j / (float)gridData.height),
                        new Vector2(1 / (float)gridData.width, 1 / (float)gridData.height));
                    //MeshRenderer meshRenderer = curObj.GetComponentInChildren<MeshRenderer>();
                    //Debug.Log(meshRenderer);
                    //if (meshRenderer != null)
                    //{
                    //    Vector2 textureOffset = new Vector2(i / (float)gridData.width, j / (float)gridData.height);
                    //    meshRenderer.material.mainTextureScale = new Vector2(1 / (float)gridData.width, 1 / (float)gridData.height);
                    //    meshRenderer.material.mainTextureOffset = textureOffset;
                    //    Debug.Log(textureOffset);
                    //}
                    curPiece.ID = tileCount + 1;
                    gridData.tiles[i, j] = curPiece.ID;
                    tileCount++;
                    // bg empty tiles
                    curObj = GameObject.Instantiate(tilePrefabEmpty);
                    curPiece = curObj.GetComponent<JigPiece>();
                    curPiece.x = i;
                    curPiece.y = j;
                    curPiece.ID = tileCount + 1;
                    curObj.transform.position = new Vector3(gridData.orgin.x + i * tileWidth, gridData.orgin.y + j * tileHeight, 0.2f);
                    curObj.transform.parent = jigsawBoardBg.transform;
                    curObj.transform.localScale = new Vector3(tileWidth, tileHeight, 1);
                    curObj.name = "JigTileBG_" + i + "_" + j;
                }
            }
        }


        public void CreaeteJigsawDynamic()
        {
            float tileWidth = aspectWidth / gridData.width;
            float tileHeight = aspectHeight / gridData.height;
            gridData.orgin.x = -aspectWidth / 2 + tileWidth / 2.0f;
            gridData.orgin.y = -aspectHeight / 2 + tileHeight / 2.0f;
            shelfLeftScript.SetRowHeight(tileHeight);
            shelfRightScript.SetRowHeight(tileHeight);
            int tileCount = 0;
            for (int i = 0; i < gridData.width; i++)
            {
                for (int j = 0; j < gridData.height; j++)
                {
                    GameObject curObj = GameObject.Instantiate(tilePrefab);
                    // same as in answer grid
                    //curObj.transform.position = new Vector3(curTray.transform.position.x + i * tileWidth + i * 0.1f, curTray.transform.position.y + j * tileHeight + j * 0.1f + 2.0f, 0);
                    curObj.transform.localScale = new Vector3(tileWidth, tileHeight, 1);
                    curObj.name = "JigTile_" + i + "_" + j;
                    if (UnityEngine.Random.Range(0, 2) == 1)
                    {
                        shelfLeftScript.AddItem(curObj.transform);
                    }
                    else
                    {
                        shelfRightScript.AddItem(curObj.transform);
                    }

                    // add values to script
                    JigPiece curPiece = curObj.GetComponent<JigPiece>();
                    curPiece.x = i;
                    curPiece.y = j;
                    curPiece.UpdateMaterials(jigsawPicData.texture2D, new Vector2(i / (float)gridData.width, j / (float)gridData.height),
                        new Vector2(1 / (float)gridData.width, 1 / (float)gridData.height));
                    //MeshRenderer meshRenderer = curObj.GetComponentInChildren<MeshRenderer>();
                    //Debug.Log(meshRenderer);
                    //if (meshRenderer != null)
                    //{
                    //    Vector2 textureOffset = new Vector2(i / (float)gridData.width, j / (float)gridData.height);
                    //    meshRenderer.material.mainTextureScale = new Vector2(1 / (float)gridData.width, 1 / (float)gridData.height);
                    //    meshRenderer.material.mainTextureOffset = textureOffset;
                    //    Debug.Log(textureOffset);
                    //}
                    curPiece.ID = tileCount + 1;
                    gridData.tiles[i, j] = curPiece.ID;
                    tileCount++;
                    // bg empty tiles
                    curObj = GameObject.Instantiate(tilePrefabEmpty);
                    curPiece = curObj.GetComponent<JigPiece>();
                    curPiece.x = i;
                    curPiece.y = j;
                    curPiece.ID = tileCount + 1;
                    curObj.transform.position = new Vector3(gridData.orgin.x + i * tileWidth, gridData.orgin.y + j * tileHeight, 0.2f);
                    curObj.transform.parent = jigsawBoardBg.transform;
                    curObj.transform.localScale = new Vector3(tileWidth, tileHeight, 1);
                    curObj.name = "JigTileBG_" + i + "_" + j;
                }
            }
        }


        void OnClickEvent(Vector3 pos)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            //Debug.Log(ray.origin + " -- " + ray.direction);
            if (Physics.Raycast(ray, out hitInfo))
            {
                //Debug.Log(hitInfo.collider.name);
                if (hitInfo.collider.name.Contains("Shelf Left"))
                {
                    if (prevPiece != null)
                    {
                        solvedData.tiles[prevPiece.x, prevPiece.y] = 0;
                        prevPiece.transform.position = hitInfo.point - Vector3.forward * 0.3f;
                        prevPiece.OnDeSelect();
                        shelfLeftScript.AddItem(prevPiece.transform);
                        shelfRightScript.RemoveItem(prevPiece.transform);
                        prevPiece = null;
                        GlobalData.moves += 1;
                        gridData.PrintGridAsString();
                        solvedData.PrintGridAsString();
                    }
                }
                else if (hitInfo.collider.name.Contains("Shelf Right"))
                {
                    if (prevPiece != null)
                    {
                        solvedData.tiles[prevPiece.x, prevPiece.y] = 0;
                        prevPiece.transform.position = hitInfo.point - Vector3.forward * 0.3f;
                        prevPiece.OnDeSelect();
                        shelfRightScript.AddItem(prevPiece.transform);
                        shelfLeftScript.RemoveItem(prevPiece.transform);
                        prevPiece = null;
                        GlobalData.moves += 1;
                        gridData.PrintGridAsString();
                        solvedData.PrintGridAsString();
                    }
                }
                else if (hitInfo.collider.name.Contains("JigTileBG"))
                {
                    if (prevPiece != null)
                    {
                        JigPiece curPiece = hitInfo.collider.GetComponent<JigPiece>();
                        if (curPiece != null)
                        {
                            //Debug.LogError(" " + curPiece.ID + " " + curPiece.x + " " + curPiece.y);
                            solvedData.tiles[curPiece.x, curPiece.y] = prevPiece.ID;
                            if (prevPiece.transform.parent == jigsawBoard.transform)
                                solvedData.tiles[prevPiece.x, prevPiece.y] = 0;
                            prevPiece.x = curPiece.x;
                            prevPiece.y = curPiece.y;
                        }
                        else
                            Debug.LogError(hitInfo.collider.name);

                        prevPiece.transform.position = hitInfo.collider.transform.position - Vector3.forward * 0.3f;
                        prevPiece.transform.parent = jigsawBoard.transform;
                        prevPiece.OnDeSelect();
                        shelfLeftScript.RemoveItem(prevPiece.transform);
                        shelfRightScript.RemoveItem(prevPiece.transform);
                        prevPiece = null;
                        gridData.PrintGridAsString();
                        solvedData.PrintGridAsString();
                        GlobalData.moves += 1;
                    }
                }
                else
                {
                    JigPiece curPiece = hitInfo.collider.GetComponent<JigPiece>();
                    if (curPiece != null)
                    {
                        curPiece.OnSelect();
                        if (prevPiece != null && prevPiece.ID != curPiece.ID)
                        {
                            prevPiece.OnDeSelect();
                            GlobalData.moves += 1;
                            //solvedData.tiles[prevPiece.x, prevPiece.y] = 0;
                        }
                        prevPiece = curPiece;
                    }
                }
            }
        }


        bool IsGameWon()
        {
            for (int i = 0; i < gridData.width; i++)
            {
                for (int j = 0; j < gridData.height; j++)
                {
                    if (gridData.tiles[i, j] != solvedData.tiles[i, j])
                        return false;
                }
            }
            OnGameOverUI();
            GO_timerText.text = "Time\n" + GetTimeAsString();
            GO_movesText.text = "Moves\n" + GlobalData.moves;
            Debug.Log("Game Won");
            isGameOver = true;
            return true;
        }


        public void DestroyAllObjects()
        {
            Transform[] allObjects = jigsawBoardBg.GetComponentsInChildren<Transform>();
            foreach (Transform curTransform in allObjects)
            {
                if (curTransform.name != jigsawBoardBg.name)
                    Destroy(curTransform.gameObject);
            }
            allObjects = jigsawBoard.GetComponentsInChildren<Transform>();
            foreach (Transform curTransform in allObjects)
            {
                if (curTransform.name != jigsawBoard.name)
                    Destroy(curTransform.gameObject);
            }
        }


        #region UI
        public void OnBackBtn()
        {
            SceneManager.LoadScene("Jig_Menu");
        }


        public void OnStartGameUI()
        {
            gameOverPanel.SetActive(false);
            gamePanel.SetActive(true);
        }


        public void OnGameOverUI()
        {
            gameOverPanel.SetActive(true);
            gamePanel.SetActive(false);
        }


        public string GetTimeAsString()
        {
            timeSpan = TimeSpan.FromSeconds((int)GlobalData.timePlayed);
            return timeSpan.Minutes.ToString("00") + " : " + timeSpan.Seconds.ToString("00");

            //return ((int)GlobalData.timePlayed).ToString("f2");
        }


        public void OnToggleBgTiles()
        {
            isBgTilesOn = !isBgTilesOn;
            if (isBgTilesOn)
            {
                OnShowBgTiles();
            }
            else
            {
                OnHideBgTiles();
            }
        }


        public void OnShowBgTiles()
        {
            Transform[] boardTiles = jigsawBoardBg.transform.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < boardTiles.Length; i++)
            {
                if (boardTiles[i].gameObject.name.Contains("Plane"))
                    boardTiles[i].gameObject.SetActive(true);
            }
            bgTilesOn.SetActive(true);
            bgTilesOff.SetActive(false);
        }


        public void OnHideBgTiles()
        {
            Transform[] boardTiles = jigsawBoardBg.transform.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < boardTiles.Length; i++)
            {
                if (boardTiles[i].gameObject.name.Contains("Plane"))
                    boardTiles[i].gameObject.SetActive(false);
            }
            bgTilesOn.SetActive(false);
            bgTilesOff.SetActive(true);
        }


        public void OnCreaeteBtn()
        {

        }


        public void OnShuffleBtn()
        {

        }


        public void OnRestartBtn()
        {
            InitGame(gridDimension.x, gridDimension.y);
        }

        public void OnHigherBtn()
        {
            InitGame(gridDimension.x + 1, gridDimension.y + 1);
        }


        public void OnLowerBtn()
        {
            InitGame(gridDimension.x - 1, gridDimension.y - 1);
        }

        public void OnHigherBtnX()
        {
            InitGame(gridDimension.x + 1, gridDimension.y);
        }


        public void OnLowerBtnX()
        {
            InitGame(gridDimension.x - 1, gridDimension.y);
        }

        public void OnHigherBtnY()
        {
            InitGame(gridDimension.x, gridDimension.y + 1);
        }


        public void OnLowerBtnY()
        {
            InitGame(gridDimension.x, gridDimension.y - 1);
        }

        #endregion
    }


}


//2do
// ? Editor scripts(wasy dev) vs runtime(seemes better)
// cut based on grid
// 2D Cuts
// 3D Cuts
// create mesh
// save the cuts or runtime
// save the cuts as seperate mesh using python + Blender
// Aspect ratio solution
// Use image box boundary to slice the image like in fb, youtube 
// 
// on Tap raise the piece
// drag and drop to the bg, tray
// drop position indicator like shadow/laser 
//

// version alpha
// Time as 00;00
// stars
// 
