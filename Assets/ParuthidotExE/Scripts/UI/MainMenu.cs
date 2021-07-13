///-----------------------------------------------------------------------------
///
/// MainMenu
/// 
/// MainMenu UI
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;


namespace ParuthidotExE
{
    public class MainMenu : MonoBehaviour
    {
        public string gameSceneName;

        void Start()
        {
        }


        void Update()
        {
        }


        public void OnPlayBtn()
        {
            StartGame();
        }


        void StartGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }


    }


}

