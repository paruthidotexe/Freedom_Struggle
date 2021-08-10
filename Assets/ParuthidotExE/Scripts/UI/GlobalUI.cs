///-----------------------------------------------------------------------------
///
/// GlobalUI
/// 
/// GlobalUI : to go to game list menu
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ParuthidotExE
{
    public class GlobalUI : MonoBehaviour
    {
        public string sceneNameStr;

        void Start()
        {

        }


        void Update()
        {

        }


        public void OnGameListBtn()
        {
            SceneManager.LoadScene(sceneNameStr);
        }


    }


}


