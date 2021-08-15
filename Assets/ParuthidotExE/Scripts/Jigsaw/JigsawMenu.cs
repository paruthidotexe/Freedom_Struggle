///-----------------------------------------------------------------------------
///
/// JigsawMenu
/// 
/// Jigsaw game Menu 
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace ParuthidotExE
{
    public class JigsawMenu : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup choosePhotoGrid;
        [SerializeField] JigsawPicData[] jigsawPicDatas;
        [SerializeField] GameObject gridElementPrefab;


        void Start()
        {
            CreateGridUI();
        }


        void Update()
        {

        }

        void CreateGridUI()
        {
            for (int i = 0; i < jigsawPicDatas.Length; i++)
            {
                GameObject curObj = GameObject.Instantiate(gridElementPrefab);
                curObj.transform.parent = choosePhotoGrid.transform;
                GridElementUI gridElementUI = curObj.GetComponent<GridElementUI>();
                gridElementUI.SetJigsawPicData(jigsawPicDatas[i]);
            }
        }
    }


}

