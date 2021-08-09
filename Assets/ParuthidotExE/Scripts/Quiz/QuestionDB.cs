///-----------------------------------------------------------------------------
///
/// QuestionDB
/// 
/// Question DB
///
///-----------------------------------------------------------------------------


using System;
using UnityEngine;


namespace ParuthidotExE
{
    [Serializable]
    [CreateAssetMenu(menuName = "Quiz/QuestionDB")]
    public class QuestionDB : ScriptableObject
    {
        public int ID = 0;
        public string dbName = "";
        public string genre = ""; // FS, Maths
        public QuestionData[] questionData;


        public QuestionDB()
        {
            ID = 0;
            dbName = "default";
            genre = "default";
        }


        public void PrintData()
        {
            string str = "";
            str += "\n" + "ID : " + ID;
            str += "\n" + "dbName : " + dbName;
            str += "\n" + "genre : " + genre;
            str += "\n" + "questions : " + questionData.Length;
            for (int i = 0; i < questionData.Length; i++)
            {
                questionData[i].PrintData();
            }
            Debug.Log(str);
            Debug.Log(JsonUtility.ToJson(this));
        }


        // 2chk: doing this changes script data values in the quiz game play class
        public QuestionData[] GetQuestionDatas()
        {
            QuestionData[] retVal = new QuestionData[questionData.Length];
            for (int i = 0; i < questionData.Length; i++)
            {
                retVal[i] = ScriptableObject.Instantiate(questionData[i]);
            }
            return retVal;
        }


        public QuestionData[] GetQuestionDatas(int count)
        {
            QuestionData[] retVal = new QuestionData[count];
            return questionData;
        }

    }


}

