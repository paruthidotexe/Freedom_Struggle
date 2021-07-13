///-----------------------------------------------------------------------------
///
/// EventMgr
/// 
/// Events
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public class EventMgr
    {
        // delegates
        public delegate void VoidDelegate();

        // events
        public static event VoidDelegate GameStarted;
        public static event VoidDelegate GamePaused;
        public static event VoidDelegate GameResume;
        public static event VoidDelegate GameOver;

        // level
        public static event VoidDelegate OnLevelUp;
        public static event VoidDelegate OnMute;
        public static event VoidDelegate OnUnmute;


        public EventMgr()
        {

        }


        public static void Raise_OnLevelUp()
        {
            if (OnLevelUp != null)
                OnLevelUp();
        }


    }


}

