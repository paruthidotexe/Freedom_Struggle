///-----------------------------------------------------------------------------
///
/// VoidEventChannelSO
/// 
/// This class is used for Events that have no arguments 
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;


namespace ParuthidotExE
{
    [CreateAssetMenu(menuName = "Events/Void Channel")]
    public class VoidChannelSO : EventChannelBaseSO
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }


    }


}

