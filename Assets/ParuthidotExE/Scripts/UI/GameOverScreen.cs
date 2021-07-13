///-----------------------------------------------------------------------------
///
/// GameOverScreen
/// 
/// GameOverScreen UI
///
///-----------------------------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ParuthidotExE
{
    public class GameOverScreen : MonoBehaviour
    {
        public TMP_Text scoreText;

        void Start()
        {
            scoreText.text = "Collected : " + GlobalData.shapesCollected +
                "\n Survived : " + (int)GlobalData.timePlayed + " Sec" +
                "\n Score : " + (GlobalData.shapesCollected * (int)GlobalData.timePlayed * 3.0f);
        }


        void Update()
        {

        }


        public void OnMenuButton()
        {
            SceneManager.LoadScene("MainMenu");
        }


        public void OnTryAgainButton()
        {
            SceneManager.LoadScene("RainingCookies");
        }


    }


}


