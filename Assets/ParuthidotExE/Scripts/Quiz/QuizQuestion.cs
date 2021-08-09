///-----------------------------------------------------------------------------
///
/// QuizQuestion
/// 
/// Question
///
///-----------------------------------------------------------------------------

using UnityEngine;
using TMPro;


namespace ParuthidotExE
{

    public class QuizQuestion : MonoBehaviour
    {
        [SerializeField] QuestionData questionData;
        [SerializeField] TMP_Text numberText;
        [SerializeField] TMP_Text questionText;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] GameObject image;

        [SerializeField]
        QuestionDataChannelSO SetQuestionEvent;


        private void OnEnable()
        {
            SetQuestionEvent.OnEventRaised += SetQuestion;
        }


        private void OnDisable()
        {
            SetQuestionEvent.OnEventRaised -= SetQuestion;
        }


        void Start()
        {

        }


        void Update()
        {

        }


        public void SetQuestion(QuestionData newQuestionData)
        {
            questionData = newQuestionData;
            Debug.Log("SetQuestion : ");
            questionData.PrintData();

            numberText.text = "Question #" + questionData.ID.ToString();
            questionText.text = questionData.question;
        }


    }


}

