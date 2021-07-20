///-----------------------------------------------------------------------------
///
/// LevelMgr
/// 
/// Load level, assets
/// Generate levels
///
///-----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ParuthidotExE
{
    public class LevelMgr : MonoBehaviour
    {
        // LevelRoot
        [SerializeField] GameObject LevelMap;
        [SerializeField] LevelData[] levelData;


        private void OnEnable()
        {
        }


        private void OnDisable()
        {
        }


        void Start()
        {

        }


        void Update()
        {

        }


        public void InitLevel()
        {
        }


        public void CreateLevel()
        {

        }


        public void LoadLevel()
        {
            InitLevel();
            CreateLevel();
        }


        public void LoadNextLevel()
        {
            LoadLevel();
        }


        public void SaveLevel()
        {
        }


        public void LoadLevelFromJson()
        {
        }


    }


}

