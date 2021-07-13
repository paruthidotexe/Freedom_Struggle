///-----------------------------------------------------------------------------
///
/// PopupCanvas
/// 
///  Popup canvas UI
///
///-----------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace ParuthidotExE
{
    public class PopupUI : MonoBehaviour
    {
        [SerializeField] GameObject balckBg;
        [SerializeField] GameObject popupBg;
        [SerializeField] GameObject popupTextObj;
        [SerializeField] TMP_Text popupText;
        [SerializeField] GameObject okBtnObj;
        [SerializeField] GameObject cancelBtnObj;

        float popupTime = 2.0f;

        private void OnEnable()
        {
            //HOGManager.ShowPopup += ShowPopup;
        }

        private void OnDisable()
        {
            //HOGManager.ShowPopup -= ShowPopup;
        }


        void Start()
        {
            HidePopup();
        }


        void Update()
        {

        }


        public void ShowPopup()
        {
            balckBg.SetActive(true);
            popupBg.SetActive(true);
            popupTextObj.SetActive(true);
            // based on type
            okBtnObj.SetActive(false);
            cancelBtnObj.SetActive(false);
        }


        public void ShowPopup(string msg, float popupTime)
        {
            popupText.text = msg;
            ShowPopup();
            StartCoroutine(AutoHidePopup(popupTime));
        }


        IEnumerator AutoHidePopup(float popupTime)
        {
            yield return new WaitForSeconds(popupTime);
            HidePopup();
        }


        public void HidePopup()
        {
            balckBg.SetActive(false);
            popupBg.SetActive(false);
            popupTextObj.SetActive(false);
            okBtnObj.SetActive(false);
            cancelBtnObj.SetActive(false);
        }


    }


}

