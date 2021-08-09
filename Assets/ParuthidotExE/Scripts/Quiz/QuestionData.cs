///-----------------------------------------------------------------------------
///
/// QuestionData
/// 
/// Question Data
///
///-----------------------------------------------------------------------------

using System;
using UnityEngine;


namespace ParuthidotExE
{
    [Serializable]
    [CreateAssetMenu(menuName = "Quiz/QuestionData")]
    public class QuestionData : ScriptableObject
    {
        public int ID = 0;
        public string question = "";
        [NonSerialized]
        public Sprite sprite = null;
        public string imagePath = "";
        public int totalChoices;
        public string[] choices; // answer as strings
        public int totalAnswers;
        public int[] answers; // index


        public QuestionData()
        {
            ID = 0;
            question = "";
            sprite = null;
            imagePath = "";
            totalChoices = 4;
            totalAnswers = 1;
            choices = new string[totalChoices];
            answers = new int[totalAnswers];
        }


        public void PrintData()
        {
            string questionDataStr = "";
            questionDataStr += "\n" + "ID : " + ID;
            questionDataStr += "\n" + "Question : " + question;
            questionDataStr += "\n" + "ImagePath : " + imagePath;
            //Debug.Log(questionDataStr);
            Debug.Log("JSON Data");
            Debug.Log(JsonUtility.ToJson(this));
        }


    }


}

