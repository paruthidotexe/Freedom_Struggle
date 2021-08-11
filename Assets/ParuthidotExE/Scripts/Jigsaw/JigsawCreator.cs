///-----------------------------------------------------------------------------
///
/// JigsawCreator
/// 
/// Jigsaw Creator
///
///-----------------------------------------------------------------------------

using UnityEngine;


namespace ParuthidotExE
{
    public class JigsawCreator : MonoBehaviour
    {
        [SerializeField] Vector2Int gridDimension;
        GridData gridData;
        [SerializeField] GameObject tilePrefabEmpty;
        [SerializeField] GameObject tilePrefab;
        [SerializeField] GameObject levelMap;
        [SerializeField] GameObject jigsawBoardBg;
        [SerializeField] GameObject jigsawBoard;
        [SerializeField] GameObject shelfLeft;
        [SerializeField] GameObject shelfRight;
        Shelf3D shelfLeftScript;
        Shelf3D shelfRightScript;

        JigPiece prevPiece;
        GridData solvedData;

        float aspectRatio = 1;
        float aspectWidth = 8;
        float aspectHeight = 8;

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
            gridData = new GridData(gridDimension.x, gridDimension.y);
            solvedData = new GridData(gridDimension.x, gridDimension.y);
            shelfLeftScript = shelfLeft.GetComponent<Shelf3D>();
            shelfRightScript = shelfRight.GetComponent<Shelf3D>();
            CreaeteJigsaw();
        }


        void Update()
        {
            if (IsGameWon())
                return;
        }


        public void CreaeteJigsaw()
        {
            float tileWidth = aspectWidth / gridData.width;
            float tileHeight = aspectHeight / gridData.height;
            gridData.orgin.x = -aspectWidth / 2 + tileWidth / 2.0f;
            gridData.orgin.y = -aspectHeight / 2 + tileHeight / 2.0f;
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
                    if (Random.Range(0, 2) == 1)
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
                    MeshRenderer meshRenderer = curObj.GetComponentInChildren<MeshRenderer>();
                    Debug.Log(meshRenderer);
                    if (meshRenderer != null)
                    {
                        Vector2 textureOffset = new Vector2(i / (float)gridData.width, j / (float)gridData.height);
                        meshRenderer.material.mainTextureScale = new Vector2(1 / (float)gridData.width, 1 / (float)gridData.height);
                        meshRenderer.material.mainTextureOffset = textureOffset;
                        Debug.Log(textureOffset);
                    }
                    curPiece.ID = tileCount + 1;
                    gridData.tiles[i, j] = curPiece.ID;
                    tileCount++;
                    // bg empty tiles
                    curObj = GameObject.Instantiate(tilePrefabEmpty);
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
                        prevPiece.transform.position = hitInfo.point - Vector3.forward * 0.3f;
                        prevPiece.OnDeSelect();
                        shelfLeftScript.AddItem(prevPiece.transform);
                        shelfRightScript.RemoveItem(prevPiece.transform);
                        prevPiece = null;
                    }
                }
                else if (hitInfo.collider.name.Contains("Shelf Right"))
                {
                    if (prevPiece != null)
                    {
                        prevPiece.transform.position = hitInfo.point - Vector3.forward * 0.3f;
                        prevPiece.OnDeSelect();
                        shelfRightScript.AddItem(prevPiece.transform);
                        shelfLeftScript.RemoveItem(prevPiece.transform);
                        prevPiece = null;
                    }
                }
                else if (hitInfo.collider.name.Contains("JigTileBG"))
                {
                    if (prevPiece != null)
                    {
                        solvedData.tiles[prevPiece.x, prevPiece.y] = prevPiece.ID;
                        prevPiece.transform.position = hitInfo.collider.transform.position - Vector3.forward * 0.3f;
                        prevPiece.transform.parent = jigsawBoard.transform;
                        prevPiece.OnDeSelect();
                        shelfLeftScript.RemoveItem(prevPiece.transform);
                        shelfRightScript.RemoveItem(prevPiece.transform);
                        prevPiece = null;
                        gridData.PrintGridAsString();
                        solvedData.PrintGridAsString();
                    }
                }
                else
                {
                    JigPiece curPiece = hitInfo.collider.GetComponent<JigPiece>();
                    if (curPiece != null)
                    {
                        curPiece.OnSelect();
                        if (prevPiece != null)
                        {
                            prevPiece.OnDeSelect();
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
            Debug.Log("Game Won");
            return true;
        }


        #region UI
        public void OnCreaeteBtn()
        {

        }


        public void OnShuffleBtn()
        {

        }


        public void OnRestartBtn()
        {

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
// maintain aspect ratio
// save the cuts or runtime
// save the cuts as seperate mesh using python + Blender
// Aspect ratio solution
// Use image box boundary to slice the image like in fb, youtube 
// 
// Resize Collider
//