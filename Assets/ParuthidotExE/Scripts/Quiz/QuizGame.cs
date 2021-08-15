///-----------------------------------------------------------------------------
///
/// QuizGame
/// 
/// Quiz Game Play
///
///-----------------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


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


        bool isGameOver = false;
        [SerializeField] GameObject gamePanel;
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] TMP_Text gameOverText;
        [SerializeField] TMP_Text timerText;
        [SerializeField] StarsUI starsUI;

        int life = 3;

        // debug
        bool isQuestionLoop = false;
        bool isLoadingQuestion = false;


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
            AudioMgr.Inst.OnPlayMusic();
            OnStartGameUI();
            questionSet = questionDB.GetQuestionDatas();
            questionDB.PrintData();
            ShuffleQuestionOrder();
            Debug.Log(questionSet.Length);
            //questionDatas[0].PrintData();
            totalQuestions = questionSet.Length;
            life = 3;
            starsUI.SetStars(life);
            StartCoroutine(WaitForLoading());
        }


        void Update()
        {
            if (isGameOver)
                return;
            timer += Time.deltaTime;
            GlobalData.timePlayed += Time.deltaTime;
            if (timerText != null)
                timerText.text = (int)(questionTime + 1 - timer) + " Sec";
            if (timer > questionTime)
            {
                LoseLife();
                LoadNextQuestion();
            }
        }


        IEnumerator WaitForLoading()
        {
            yield return new WaitForSeconds(1.0f);
            LoadQuestion();
            yield return null;
        }


        void LoadPrevQuestion()
        {
            questionIndex--;
            if (questionIndex < 0)
            {
                questionIndex = totalQuestions - 1;
            }
            LoadQuestion();
        }


        void LoadNextQuestion()
        {
            questionIndex++;
            if (questionIndex >= totalQuestions)
            {
                if (!isQuestionLoop)
                {
                    questionIndex = totalQuestions;
                    GameOver();
                    return;
                }
                questionIndex = 0;
            }
            LoadQuestion();
        }


        IEnumerator LoadNextQuestionDelay()
        {
            isLoadingQuestion = true;
            yield return new WaitForSeconds(1.0f);
            LoadNextQuestion();
            isLoadingQuestion = false;
            yield return null;
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

            timer = 0;
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
            if (isLoadingQuestion)
                return;
            if (val == 127)
            {
                LoadPrevQuestion();
            }
            else if (val == 128)
            {
                LoadNextQuestion();
            }
            //else if (questionSet[questionIndex].selected[val - 1] == 1)
            //{
            //    questionSet[questionIndex].selected[val - 1] = 0;
            //    PrintAttendedQuestions();
            //}
            else if (val == 256)
            {
                OnBackBtn();
            }
            else
            {
                attendedQuestionsCount += 1;
                questionSet[questionIndex].selected[val - 1] = 1;
                Debug.LogWarning((val - 1) + " : " + questionSet[questionIndex].answers[0] + "vs" + questionSet[questionIndex].selected[val - 1]);
                if (questionSet[questionIndex].answers[0] != val - 1)
                {
                    LoseLife();
                }
                StartCoroutine(LoadNextQuestionDelay());
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


        void LoseLife()
        {
            life -= 1;
            attendedQuestionsCount--;
            if (attendedQuestionsCount < 0)
                attendedQuestionsCount = 0;
            starsUI.SetStars(life);
            if (life <= 0)
            {
                GameOver();
            }
        }


        void GameOver()
        {
            isGameOver = true;
            OnGameOverUI();
            gameOverText.text = "Time : " + (int)GlobalData.timePlayed + " Seconds\n" + "Correct : " + (attendedQuestionsCount) + " out of " + totalQuestions;
        }

        public void OnStartGameUI()
        {
            gameOverPanel.SetActive(false);
            gamePanel.SetActive(true);
        }


        public void OnGameOverUI()
        {
            gameOverPanel.SetActive(true);
            gamePanel.SetActive(false);
        }


        public void OnBackBtn()
        {
            SceneManager.LoadScene("Quiz_Menu");
        }

    }


}

// 2do
// multiple choice questions
// 3D buttons
// 