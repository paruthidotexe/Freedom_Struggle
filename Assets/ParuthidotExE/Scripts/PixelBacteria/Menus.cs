using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace ParuthidotExE
{
    public class Menus : MonoBehaviour
    {
        public TMP_Text gameOverStateTxt;
        public TMP_Text detailsTxt;

        void Start()
        {
            if (detailsTxt != null)
            {
                detailsTxt.text = "Time : " + (int)GlobalData.timePlayed + " Sec \n\nMoves : " + GlobalData.moves;
            }
            if (gameOverStateTxt != null)
            {
                gameOverStateTxt.text = GlobalData.GameOverState;
            }

        }

        void Update()
        {

        }


        public void OnPlayBtn()
        {
            SceneManager.LoadScene("PB_InGame");
        }

        public void OnNextLevelBtn()
        {
            GlobalData.levelNo++;
            if (GlobalData.levelNo >= GlobalData.TotalLevels)
            {
                GlobalData.levelNo = 1;
            }
            SceneManager.LoadScene("PB_InGame");
        }


    }


}