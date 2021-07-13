///-----------------------------------------------------------------------------
///
/// AudioMgr
/// 
/// Main Audio manager
/// Singleton -> change to use scriptable objects "Channels"
/// Load and unload audio based on level
/// 
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public enum SFXValues
    {
        SFX_Click,
        SFX_Ok,
        SFX_Cancel,
        SFX_PopUps,
        SFX_PickUps,
        SFX_Move,
        SFX_Switch,
    }


    public class AudioMgr : GenericSingleton<AudioMgr>
    {
        public List<AudioSource> musicAudioSrcList;
        public List<AudioSource> sfxAudioSrcList;
        int musicChannel = 0;
        int sfxChannel = 0;

        int globalVolume = 100;
        int musicVolume = 100;
        int sfxVolume = 100;

        public AudioClip Clip_Click;
        public AudioClip Clip_Ok;
        public AudioClip Clip_Cancel;
        public AudioClip Clip_PopUps;
        public AudioClip Clip_PickUps;
        public AudioClip Clip_Move;
        public AudioClip Clip_Switch;

        public Dictionary<SFXValues, AudioClip> SFXValueMap;

        void Start()
        {
            SFXValueMap = new Dictionary<SFXValues, AudioClip>();
            SFXValueMap.Add(SFXValues.SFX_Click, Clip_Click);
            SFXValueMap.Add(SFXValues.SFX_Ok, Clip_Ok);
            SFXValueMap.Add(SFXValues.SFX_Cancel, Clip_Cancel);
            SFXValueMap.Add(SFXValues.SFX_PopUps, Clip_PopUps);
            SFXValueMap.Add(SFXValues.SFX_PickUps, Clip_PickUps);
            SFXValueMap.Add(SFXValues.SFX_Move, Clip_Move);
            SFXValueMap.Add(SFXValues.SFX_Switch, Clip_Switch);
            StartCoroutine(OnDelayedPlayMusic(4.0f));
        }

        void Update()
        {
        }


        public void OnPlaySFX(SFXValues curSFXValue)
        {
            sfxChannel++;
            if (sfxChannel >= sfxAudioSrcList.Count)
                sfxChannel = 0;
            //GetAudioClip(curSFXValue);

            sfxAudioSrcList[sfxChannel].clip = SFXValueMap[curSFXValue];
            sfxAudioSrcList[sfxChannel].Play();
        }

        public void OnPlayMusic()
        {
            musicChannel = 0;
            //musicAudioSrcList[musicIndex].clip;
            musicAudioSrcList[musicChannel].Play();
        }


        IEnumerator OnDelayedPlayMusic(float delayedTime)
        {
            yield return new WaitForSeconds(delayedTime);
            OnPlayMusic();
        }


        public void OnMute()
        {
            musicAudioSrcList[0].mute = true;
            for (int i = 0; i < sfxAudioSrcList.Count; i++)
                sfxAudioSrcList[i].mute = true;
        }


        public void OnUnmute()
        {
            musicAudioSrcList[0].mute = false;
            for (int i = 0; i < sfxAudioSrcList.Count; i++)
                sfxAudioSrcList[i].mute = false;
        }


        void GetAudioClip(SFXValues curSFXValue)
        {
            switch (curSFXValue)
            {
                case SFXValues.SFX_Click:
                    sfxAudioSrcList[sfxChannel].clip = Clip_Click;
                    sfxAudioSrcList[sfxChannel].Play();
                    break;
            }
        }


    }


}

