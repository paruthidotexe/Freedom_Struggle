///-----------------------------------------------------------------------------
///
/// QuizChoices
/// 
/// Choices
///
///-----------------------------------------------------------------------------

using UnityEngine;
using TMPro;


namespace ParuthidotExE
{

    public class QuizChoice : MonoBehaviour
    {
        [SerializeField] int choiceIndex = 0;
        [SerializeField] QuestionData questionData;
        [SerializeField] TMP_Text numberText;
        [SerializeField] TMP_Text choiceText;
        [SerializeField]
        QuestionDataChannelSO SetQuestionEvent;
        [SerializeField] IntChannelSO TapEvent;
        Collider btnCollider;


        private void OnEnable()
        {
            SetQuestionEvent.OnEventRaised += SetQuestion;
            InputMgr.ClickedAction += OnTapped;
        }


        private void OnDisable()
        {
            SetQuestionEvent.OnEventRaised -= SetQuestion;
            InputMgr.ClickedAction -= OnTapped;
        }


        void Start()
        {
            btnCollider = gameObject.GetComponent<Collider>();
        }


        void Update()
        {

        }


        public void SetChoice(string val)
        {
            choiceText.text = val;
        }


        public void SetQuestion(QuestionData newQuestionData)
        {
            questionData = newQuestionData;
            if (choiceIndex - 1 >= 0 && choiceIndex - 1 < questionData.choices.Length)
                choiceText.text = questionData.choices[choiceIndex - 1];
        }


        void OnTapped(Vector3 pos)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (btnCollider.Raycast(ray, out hitInfo, 20))
            {
                //Debug.Log("RaiseEvent : " + choiceIndex);
                TapEvent.RaiseEvent(choiceIndex);
            }
        }


    }


}

