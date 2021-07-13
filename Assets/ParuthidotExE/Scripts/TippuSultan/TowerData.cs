///-----------------------------------------------------------------------------
///
/// TowerData
/// 
/// Data of Towers
///
///-----------------------------------------------------------------------------

using System;
using UnityEngine;


namespace ParuthidotExE
{
    [CreateAssetMenu(fileName = "NewTippuSultan", menuName = "TippuSultan/EnemyData", order = 1)]
    [Serializable]
    public class TowerData : ScriptableObject
    {
        public int type = 0;
        public float damage = 25;
        public float health = 100;
        public float range = 3; // 3 tiles
        public float attackSpeed = 1;
        public Sprite sprite;
        public GameObject Prefab;// ?
        public bool isAffectedByTime = true;
    }

}


