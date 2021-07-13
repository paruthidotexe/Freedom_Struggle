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
    [CreateAssetMenu(fileName = "NewLevelData", menuName = "VerticalFalling/LevelData", order = 1)]
    [Serializable]
    public class LevelData : ScriptableObject
    {
        public int levelId;
        public string levelName = "";
        public int time = 60;//seconds
        public int totalShapes = 10;
        public ShapeData[] shapelDataList;

        public ShapeData GetRandomShapeData()
        {
            if (shapelDataList.Length <= 0)
                return null;
            return shapelDataList[UnityEngine.Random.Range(0, shapelDataList.Length)];
        }
    }


}

