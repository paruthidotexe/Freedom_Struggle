///-----------------------------------------------------------------------------
///
/// QuestionData
/// 
/// Question Data
///
///-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    [Serializable]
    [CreateAssetMenu(menuName = "Jigsaw/PicData")]


    public class JigsawPicData : ScriptableObject
    {
        public int ID = 0;
        public Sprite sprite;
        public Texture2D texture2D;
        public float aspectRatio = 1;
        public float aspectWidth = 10;
        public float aspectHeight = 10;
        public string leaderName = "Ponniah";
        public List<string> achivements = new List<string>() { "Awareness Guidance Action" };


        void Start()
        {

        }


        void Update()
        {

        }


    }


}

