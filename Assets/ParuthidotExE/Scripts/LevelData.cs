///-----------------------------------------------------------------------------
///
/// LevelData
/// 
/// Data across scenes / levels
///
///-----------------------------------------------------------------------------

using System;
using UnityEngine;


namespace ParuthidotExE
{
    [CreateAssetMenu(fileName = "NewLevelData", menuName = "PixelBacteria/LevelData", order = 1)]
    [Serializable]
    public class LevelData : ScriptableObject
    {
        public int levelId;
        public string levelName = "";
        public int time = 120;//seconds
        public int totalShapes = 10;
    }


}

