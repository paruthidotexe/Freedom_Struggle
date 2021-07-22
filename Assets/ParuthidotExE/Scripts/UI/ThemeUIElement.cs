///-----------------------------------------------------------------------------
///
/// ThemeUIElement
/// 
/// UI Theme Element
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;


namespace ParuthidotExE
{
    public class ThemeUIElement : MonoBehaviour
    {
        public VoidChannelSO ChangeTheme;

        private void OnEnable()
        {
            ChangeTheme.OnEventRaised += OnChangeTheme;
        }


        private void OnDisable()
        {
            ChangeTheme.OnEventRaised -= OnChangeTheme;
        }


        void Start()
        {

        }


        void Update()
        {

        }


        void OnChangeTheme()
        {
            Debug.Log(" ThemeUIElement : OnChangeTheme");
            Image sprite = gameObject.GetComponent<Image>();
            Debug.Log(sprite);
            sprite.color = GlobalData.themeData.fgColor;
        }


    }


}

