///-----------------------------------------------------------------------------
///
/// GameListUI
/// 
/// game list menu
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ParuthidotExE
{
    public class GameListUI : MonoBehaviour
    {

        void Start()
        {
        }

        void Update()
        {

        }

        public void OnOpenGame(string sceneNameStr)
        {
            AudioMgr.Inst.OnPlaySFX(SFXValues.SFX_Click);
            SceneManager.LoadScene(sceneNameStr);
        }

    }


}

