using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public class JigPiece : MonoBehaviour
    {
        // cell position
        public int x = 0;
        public int y = 0;
        public int ID = 0;// 0 = none
        // not used in the image
        public int state = 0; // 0 : tray ; 16 : mouse; 8 : board; 9 : board correct
        [SerializeField] ParticleSystem ps;

        void Start()
        {
            ps.Stop();
        }


        void Update()
        {

        }


        public void OnSelect()
        {
            ps.Play();
        }


        public void OnDeSelect()
        {
            ps.Stop();
        }


    }


}

