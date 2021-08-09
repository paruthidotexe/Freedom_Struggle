///-----------------------------------------------------------------------------
///
/// QuestionDataChannelSO
/// 
/// This class is used for Events channel 
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;


namespace ParuthidotExE
{
    [CreateAssetMenu(menuName = "Events/QuestionData Channel")]
    public class QuestionDataChannelSO : EventChannelBaseSO
    {
        public UnityAction<QuestionData> OnEventRaised;

        public void RaiseEvent(QuestionData val)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(val);
        }


    }


}

