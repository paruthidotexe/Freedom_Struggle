///-----------------------------------------------------------------------------
///
/// QuizGame
/// 
/// Quiz Game Play
///
///-----------------------------------------------------------------------------

using UnityEngine;


namespace ParuthidotExE
{
    public class QuizGame : MonoBehaviour
    {
        [SerializeField] QuestionDB questionDB;
        QuestionData[] questionDatas;
        public int totalQuestions = 0;
        int choicesPerQuestion = 4;
        int attendedQuestionsCount = 0;
        int[,] attendedQuestions;
        float timer = 0;
        float gameTime = 180;
        float questionTime = 10;
        int questionIndex = 0;
        [SerializeField]
        QuestionDataChannelSO SetQuestionEvent;
        [SerializeField] IntChannelSO TappedAnswer;

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
            questionDatas = questionDB.GetQuestionDatas();
            questionDB.PrintData();
            ShuffleQuestionOrder();
            Debug.Log(questionDatas.Length);
            //questionDatas[0].PrintData();
            totalQuestions = questionDatas.Length;
            attendedQuestions = new int[questionDatas.Length, choicesPerQuestion];
            for (int i = 0; i < attendedQuestions.GetLength(0); i++)
            {
                for (int j = 0; j < attendedQuestions.GetLength(1); j++)
                {
                    attendedQuestions[i, j] = 0;
                }
            }
            LoadQuestion();
        }


        void Update()
        {
            timer += Time.deltaTime;
            GlobalData.timePlayed = 10 + 1 - timer;// Time.deltaTime;
            if (timer > questionTime)
            {
                LoadNextQuestion();
            }
        }


        void LoadNextQuestion()
        {
            timer = 0;
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
            questionDatas[questionIndex].ID = questionIndex + 10;
            Debug.Log("LoadNextQuestion : " + questionIndex);
            SetQuestionEvent.RaiseEvent(questionDatas[questionIndex]);
        }


        void ShuffleQuestionOrder()
        {
            //for (int i = 0; i < questionDatas.Length; i++)
            //{
            //    int tmpVal = questionDatas[i].ID;
            //}
            QuestionData tmpVal = questionDatas[0];
            questionDatas[0] = questionDatas[1];
            questionDatas[1] = tmpVal;
        }


        void OnTappedAnswer(int val)
        {
            Debug.Log("Answer : " + val);
            attendedQuestions[questionIndex, val] = 1;
            string answersStr = "";
            for (int i = 0; i < attendedQuestions.GetLength(0); i++)
            {
                answersStr += "\n";
                for (int j = 0; j < attendedQuestions.GetLength(1); j++)
                {
                    answersStr += "," + attendedQuestions[i, j];
                }
            }
            Debug.Log(answersStr);
            LoadNextQuestion();
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