///-----------------------------------------------------------------------------
///
/// HUDScripts
/// 
/// Modular Hud as prefab with text + buttons
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace ParuthidotExE
{
    public class HUDScripts : MonoBehaviour
    {
        [SerializeField] VoidEventChannelSO RestartEvent;
        [SerializeField] VoidEventChannelSO ChangeTheme;

        [SerializeField] ThemesDB themesDB;

        // ui input 
        public delegate void OnMove(Vector3 moveDir);
        public static event OnMove MoveAction;
        public delegate void OnReverse();
        public static event OnReverse ReverseAction;
        public static event OnReverse NextLevelEvent;

        // score
        public TMP_Text timeText;
        public TMP_Text scoreText;
        int theme = 0;
        int totalThemes = 2;


        void Start()
        {
            if (scoreText != null)
                scoreText.text = "";
            if (themesDB != null)
                GlobalData.themeData = themesDB.GetRandomTheme();
        }


        void Update()
        {
            if (scoreText != null && timeText != null)
            {
                timeText.text = (int)GlobalData.timePlayed + " Seconds";
                scoreText.text = "" + GlobalData.shapesCollected;
            }
        }


        // UI
        public void OnPauseButton()
        {
            SceneManager.LoadScene("MainMenu");
        }


        public void OnHomeButton()
        {
            SceneManager.LoadScene("MainMenu");
        }


        public void OnRestartButton()
        {
            RestartEvent.RaiseEvent();
        }


        public void OnMoveBtn(int dir)
        {
            Vector3 moveDir = Vector3.zero;
            if (dir == 1)
            {
                moveDir.x = -1;
                moveDir.y = 0;
                moveDir.z = 0;
            }
            if (dir == 2)
            {
                moveDir.x = 1;
                moveDir.y = 0;
                moveDir.z = 0;
            }
            if (dir == 3)
            {
                moveDir.x = 0;
                moveDir.y = 0;
                moveDir.z = 1;
            }
            if (dir == 4)
            {
                moveDir.x = 0;
                moveDir.y = 0;
                moveDir.z = -1;
            }
            Raise_OnMoveAction(moveDir);
        }


        public void OnReverseBtn()
        {
            Raise_OnReverseAction();
        }


        public void OnNextLevelBtn()
        {
            Raise_NextLevelEvent();
        }


        public void OnChangeThemeBtn()
        {
            Debug.Log("HUD : OnChangeTheme");
            GlobalData.themeData = themesDB.GetRandomTheme();
            ChangeTheme.RaiseEvent();
        }


        // Events
        void Raise_OnMoveAction(Vector3 moveDir)
        {
            if (MoveAction != null)
                MoveAction(moveDir);
        }


        void Raise_OnReverseAction()
        {
            if (ReverseAction != null)
                ReverseAction();
        }


        void Raise_NextLevelEvent()
        {
            if (NextLevelEvent != null)
                NextLevelEvent();
        }


        public void OnChangeBg()
        {
            theme++;
            if (theme >= totalThemes)
                theme = 0;
            if (theme == 0)
                Camera.main.backgroundColor = Color.black;
            else
                Camera.main.backgroundColor = new Color(0.9f, 0.9f, 0.9f, 1);
        }


        //public void OnChangeTheme()
        //{
        //    theme++;
        //    if (theme >= totalThemes)
        //        theme = 0;
        //    if (theme == 0)
        //        Camera.main.backgroundColor = Color.black;
        //    else
        //        Camera.main.backgroundColor = new Color(0.9f, 0.9f, 0.9f, 1);
        //}


    }


}
