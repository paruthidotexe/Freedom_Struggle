///-----------------------------------------------------------------------------
///
/// JigsawMenu
/// 
/// Jigsaw game Menu 
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace ParuthidotExE
{
    public class GridElementUI : MonoBehaviour
    {
        [SerializeField] Image spriteRenderer;
        [SerializeField] JigsawPicData jigsawPicData;

        void Start()
        {
        }


        void Update()
        {

        }


        public void SetSprite(Sprite newSprite)
        {
            spriteRenderer.sprite = newSprite;
        }



        public void OnBtnClick()
        {
            GlobalData.selectedPicID = jigsawPicData.ID;
            SceneManager.LoadScene("Jig_Creator");
        }


        public void SetJigsawPicData(JigsawPicData newJigsawPicData)
        {
            jigsawPicData = newJigsawPicData;
            SetSprite(jigsawPicData.sprite);
        }
    }


}

