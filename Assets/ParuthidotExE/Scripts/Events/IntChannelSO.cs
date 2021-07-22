///-----------------------------------------------------------------------------
///
/// IntChannelSO
/// 
/// This class is used for Events that have no arguments 
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;


namespace ParuthidotExE
{
    [CreateAssetMenu(menuName = "Events/Int Channel")]
    public class IntChannelSO : EventChannelBaseSO
    {
        public UnityAction<int> OnEventRaised;

        public void RaiseEvent(int val)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(val);
        }


    }


}

