///-----------------------------------------------------------------------------
///
/// DecalSplash
/// 
/// Decal Splash
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public class DecalSplash : MonoBehaviour
    {
        [SerializeField] GameObject[] bubbles;

        void Start()
        {

        }


        void Update()
        {

        }


        public void ShowSplash()
        {
            ShowRandomBubbles();
            Destroy(gameObject, 4.0f);
        }


        public void ShowRandomBubbles()
        {
            for (int i = 0; i < bubbles.Length; i++)
            {
                if (Random.Range(0, 100) > 50)
                    bubbles[i].SetActive(false);
            }
        }


    }


}

