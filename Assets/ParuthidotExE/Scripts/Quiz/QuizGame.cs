///-----------------------------------------------------------------------------
///
/// QuizGame
/// 
/// Quiz Game Play
///
///-----------------------------------------------------------------------------

using System.Collections;
using UnityEngine;


namespace ParuthidotExE
{
    public class QuizGame : MonoBehaviour
    {
        [SerializeField] QuestionDB questionDB;
        QuestionData[] questionSet;
        public int totalQuestions = 0;
        int choicesPerQuestion = 4;
        float timer = 0;
        float gameTime = 180;
        float questionTime = 10;// testing
        int questionIndex = 0;
        int attendedQuestionsCount = 0;
        [SerializeField]
        QuestionDataChannelSO SetQuestionEvent;
        [SerializeField] IntChannelSO TappedAnswer;

        [SerializeField] QuizQuestion quizQuestion;
        [SerializeField] QuizChoice[] quizChoices;


        // debug
        bool isQuestionLoop = true;


        private void OnEnable()
        {
            TappedAnswer.OnEventRaised += OnTappedAnswer;
        }


        private void OnDisable()
        {
            TappedAnswer.OnEventRaised -= OnTappedAnswer;
        }


        void Start()
        {
            questionSet = questionDB.GetQuestionDatas();
            questionDB.PrintData();
            ShuffleQuestionOrder();
            Debug.Log(questionSet.Length);
            //questionDatas[0].PrintData();
            totalQuestions = questionSet.Length;
            StartCoroutine(WaitForLoading());
        }


        void Update()
        {
            timer += Time.deltaTime;
            GlobalData.timePlayed = timer;// questionTime + 1 - timer
            //if (timer > questionTime)
            //{
            //    attendedQuestionsCount += 1;
            //    LoadNextQuestion();
            //}
        }


        IEnumerator WaitForLoading()
        {
            yield return new WaitForSeconds(2.0f);
            LoadQuestion();
            yield return null;
        }


        void LoadPrevQuestion()
        {
            //timer = 0;
            questionIndex--;
            if (questionIndex < 0)
            {
                questionIndex = totalQuestions - 1;
            }
            LoadQuestion();
        }


        void LoadNextQuestion()
        {
            //timer = 0;
            questionIndex++;
            if (questionIndex >= totalQuestions)
            {
                if (!isQuestionLoop)
                {
                    GameOver();
                    return;
                }
                questionIndex = 0;
            }
            LoadQuestion();
        }


        void LoadQuestion()
        {
            questionSet[questionIndex].ID = questionIndex + 10;
            Debug.Log("LoadNextQuestion : " + questionIndex);
            //SetQuestionEvent.RaiseEvent(questionSet[questionIndex]);

            quizQuestion.SetQuestion(questionSet[questionIndex]);
            for (int i = 0; i < quizChoices.Length; i++)
            {
                quizChoices[i].SetQuestion(questionSet[questionIndex]);
                if (questionSet[questionIndex].selected[i] == 1)
                {
                    quizChoices[i].SetSelection(true);
                }
            }
        }


        void ShuffleQuestionOrder()
        {
            //for (int i = 0; i < questionDatas.Length; i++)
            //{
            //    int tmpVal = questionDatas[i].ID;
            //}
            QuestionData tmpVal = questionSet[0];
            questionSet[0] = questionSet[1];
            questionSet[1] = tmpVal;
        }


        void OnTappedAnswer(int val)
        {
            if (val == 127)
            {
                LoadPrevQuestion();
            }
            else if (val == 128)
            {
                LoadNextQuestion();
            }
            else if (questionSet[questionIndex].selected[val - 1] == 1)
            {
                questionSet[questionIndex].selected[val - 1] = 0;
                PrintAttendedQuestions();
            }
            else
            {
                questionSet[questionIndex].selected[val - 1] = 1;
                PrintAttendedQuestions();
            }
        }


        void PrintAttendedQuestions()
        {
            string answersStr = "";
            for (int i = 0; i < questionSet.Length; i++)
            {
                answersStr += "\n" + i + " > ";
                for (int j = 0; j < questionSet[i].selected.Length; j++)
                {
                    answersStr += questionSet[i].selected[j] + ", ";
                }
            }
            Debug.Log(answersStr);
        }



        void GameOver()
        {

        }


    }


}

// 2do
// multiple choice questions
// 3D buttons
// 