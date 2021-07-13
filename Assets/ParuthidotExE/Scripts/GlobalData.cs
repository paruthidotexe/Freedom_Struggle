///-----------------------------------------------------------------------------
///
/// GlobalData
/// 
/// Data across scenes / levels
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ParuthidotExE
{
    public class GlobalData
    {
        public static float timePlayed = 0;// seconds
        public static int timePlayedInt = 0;// seconds
        public static int moves = 0;
        public static int score = 0;
        public static int shapesCollected = 0;
        public static int totalShapes = 0;
        public static int shapesMissed = 0;
        public static ThemeData themeData;

        public GlobalData()
        {
            themeData = ScriptableObject.CreateInstance<ThemeData>();
        }


        public static void OnInit()
        {
            timePlayed = 0;
            timePlayedInt = 0;
            moves = 0;
            score = 0;
        }


        public static string GetTimeAsString()
        {
            return "00:00";
        }

    }
}

