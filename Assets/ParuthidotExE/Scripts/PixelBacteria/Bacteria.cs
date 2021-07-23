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
    public enum BacteriaState
    {
        None = 0,
        Move,
        Clone,
        Destruct,
        Last
    }


    public class Bacteria : MonoBehaviour
    {
        [SerializeField] GameObject[] faceReactions;
        public BacteriaState state = BacteriaState.None;

        [SerializeField] IntChannelSO ChangePlayerStateEvent = default;
        [SerializeField] ParticleSystem ps;


        private void OnEnable()
        {
            PlayerController.ChangeStateAction += OnChangeBacteriaState;
        }


        private void OnDisable()
        {
            PlayerController.ChangeStateAction -= OnChangeBacteriaState;
        }


        void Start()
        {
            HideFace();
            faceReactions[0].SetActive(true);
            ps.Stop();
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
                    ps.Stop();
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


        public void OnChangeBacteriaState(string newStateStr)
        {
            if (newStateStr == "1")
            {
                OnChangeBacteriaState(BacteriaState.Move);
            }
            else if (newStateStr == "2")
            {
                OnChangeBacteriaState(BacteriaState.Clone);
            }
            else if (newStateStr == "3")
            {
                OnChangeBacteriaState(BacteriaState.Destruct);
            }
            else if (newStateStr == "4")
            {
                OnChangeBacteriaState(BacteriaState.None);
            }

            if (newStateStr == "[")
            {
                OnPrevState(state);
            }
            else if (newStateStr == "]")
            {
                OnNextState(state);
            }
        }



        void OnChangeBacteriaState(BacteriaState newState)
        {
            state = newState;
            ChangePlayerStateEvent.RaiseEvent((int)state);
            ChangeFaceReaction(state);
        }


        void OnPrevState(BacteriaState newState)
        {
            int curStateVal = (int)newState;
            curStateVal--;
            if (curStateVal < 0)
                curStateVal = (int)(BacteriaState.Last) - 1;
            newState = (BacteriaState)curStateVal;
            Debug.Log(newState);
            OnChangeBacteriaState(newState);
        }


        void OnNextState(BacteriaState newState)
        {
            int curStateVal = (int)newState;
            curStateVal++;
            if (curStateVal > (int)(BacteriaState.Last) - 1)
            {
                curStateVal = 0;
            }
            newState = (BacteriaState)curStateVal;
            OnChangeBacteriaState(newState);
        }


        public void OnMove(Vector3 direction, GameObject playerGreenObj)
        {
            //Debug.Log(direction);
            switch (state)
            {
                case BacteriaState.None:
                    //playerGreen.transform.position += direction;
                    break;
                case BacteriaState.Move:
                    transform.position += direction;
                    ps.Play();
                    break;
                case BacteriaState.Clone:
                    GameObject newObj = GameObject.Instantiate(playerGreenObj);
                    transform.position += direction;
                    break;
                case BacteriaState.Destruct:
                    //playerGreen.transform.position += direction;
                    break;
            }
        }


    }


}

