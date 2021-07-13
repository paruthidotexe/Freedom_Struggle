///-----------------------------------------------------------------------------
///
/// ScoreMgr
/// 
/// Calucation of score based on game
/// 
///-----------------------------------------------------------------------------

using UnityEngine;
namespace ParuthidotExE
{
    public class ScoreMgr
    {
        ScoreData scoreData;

        public ScoreMgr()
        {
        }


        // End of level
        public void OnCalculateScore()
        {
            scoreData.score = (int)scoreData.timePlayed * 10 + scoreData.moves * 100;
            if (scoreData.score > 100)
            {
                scoreData.stars = 3;
            }
        }
    }


    ///-----------------------------------------------------------------------------
    ///
    /// ScoreData
    /// 
    /// ScoreData for saving 
    ///
    ///-----------------------------------------------------------------------------
    public class ScoreData
    {
        public float timePlayed = 0;// seconds
        public int moves = 0;
        public int score = 0;
        public int stars = 1;


        public ScoreData()
        {
        }


    }

}
