///-----------------------------------------------------------------------------
///
/// QuizMenu
/// 
/// Quiz Menu
///
///-----------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.SceneManagement;


namespace ParuthidotExE
{
    public class QuizMenu : MonoBehaviour
    {

        void Start()
        {

        }


        void Update()
        {

        }


        public void OnPlayBtn()
        {
            SceneManager.LoadScene("Quiz_InGame");
        }


    }


}

