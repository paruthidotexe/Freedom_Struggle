///-----------------------------------------------------------------------------
///
/// SettingsUIMini
/// 
/// Settings Mini 
///
///-----------------------------------------------------------------------------

using UnityEngine;


namespace ParuthidotExE
{
    public class SettingsUIMini : MonoBehaviour
    {
        [SerializeField] GameObject settingsBtn;
        [SerializeField] GameObject settingsPanel;

        [SerializeField] GameObject musicOnImg;
        [SerializeField] GameObject musicOffImg;
        [SerializeField] GameObject sfxOnImg;
        [SerializeField] GameObject sfxOffImg;
        [SerializeField] GameObject hapticsOnImg;
        [SerializeField] GameObject hapticsOffImg;

        [SerializeField] VoidChannelSO MusicOn;
        [SerializeField] VoidChannelSO MusicOff;
        [SerializeField] VoidChannelSO SFXOn;
        [SerializeField] VoidChannelSO SFXOff;
        [SerializeField] VoidChannelSO HapticsOn;
        [SerializeField] VoidChannelSO HapticsOff;

        bool isMusicOn = true;
        bool isSFXOn = true;
        bool isHapticsOn = true;

        void Start()
        {
            CloseMenu();
        }


        void Update()
        {
        }


        public void OpenMenu()
        {
            settingsBtn.SetActive(false);
            settingsPanel.SetActive(true);
        }


        public void CloseMenu()
        {
            settingsBtn.SetActive(true);
            settingsPanel.SetActive(false);
        }


        public void OnSettingBtn()
        {
            OpenMenu();
        }


        public void OnBackBtn()
        {
            CloseMenu();
        }


        public void OnToggleMusic()
        {
            isMusicOn = !isMusicOn;
            if (isMusicOn)
            {
                MusicOn.RaiseEvent();
                musicOnImg.SetActive(true);
                musicOffImg.SetActive(false);
            }
            else
            {

                MusicOff.RaiseEvent();
                musicOnImg.SetActive(false);
                musicOffImg.SetActive(true);
            }
        }


        public void OnToggleSFX()
        {
            isSFXOn = !isSFXOn;
            if (isSFXOn)
            {
                SFXOn.RaiseEvent();
                sfxOnImg.SetActive(true);
                sfxOffImg.SetActive(false);
            }
            else
            {

                SFXOff.RaiseEvent();
                sfxOnImg.SetActive(false);
                sfxOffImg.SetActive(true);
            }
        }


        public void OnToggleHaptics()
        {
            isHapticsOn = !isHapticsOn;
            if (isHapticsOn)
            {
                HapticsOn.RaiseEvent();
                hapticsOnImg.SetActive(true);
                hapticsOffImg.SetActive(false);
            }
            else
            {

                HapticsOff.RaiseEvent();
                hapticsOnImg.SetActive(false);
                hapticsOffImg.SetActive(true);
            }
        }


        public void OnShareBtn()
        {

        }


        public void OnRateUsBtn()
        {

        }


        public void OnPrivacyBtn()
        {

        }


    }


}

