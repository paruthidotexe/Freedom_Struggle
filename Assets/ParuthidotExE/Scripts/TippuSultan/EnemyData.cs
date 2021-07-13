///-----------------------------------------------------------------------------
///
/// EnemyData
/// 
/// Data of Enemies
///
///-----------------------------------------------------------------------------

using System;
using UnityEngine;


namespace ParuthidotExE
{
    [CreateAssetMenu(fileName = "NewTippuSultan", menuName = "TippuSultan/EnemyData", order = 1)]
    [Serializable]
    public class EnemyData : ScriptableObject
    {
        public int type = 0;
        public float level = 0;
        public float health = 100;
        public Sprite sprite;
        public GameObject Prefab;// ?
        public float speed = 0.8f;
        public Color color = Color.white;
        public bool isAffectedByTime = true;
    }


}

