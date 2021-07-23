///-----------------------------------------------------------------------------
///
/// Bacteria
/// 
/// Bacteria anim
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public class Bacteria : MonoBehaviour
    {
        [SerializeField] GameObject[] faceReactions;

        void Start()
        {
            HideFace();
            faceReactions[0].SetActive(true);
        }

        void Update()
        {

        }


        public void ChangeFaceReaction(BacteriaState bacteriaState)
        {
            HideFace();
            // Face Reaction
            switch (bacteriaState)
            {
                case BacteriaState.None:
                    faceReactions[0].SetActive(true);
                    break;
                case BacteriaState.Move:
                    faceReactions[0].SetActive(true);
                    break;
                case BacteriaState.Clone:
                    faceReactions[1].SetActive(true);
                    break;
                case BacteriaState.Destruct:
                    faceReactions[2].SetActive(true);
                    break;
            }
        }


        void HideFace()
        {
            for (int i = 0; i < faceReactions.Length; i++)
            {
                faceReactions[i].SetActive(false);
            }
        }


    }


}
