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
        [SerializeField] bool isHead = false;
        public BacteriaState state = BacteriaState.None;

        [SerializeField] IntChannelSO ChangePlayerStateEvent = default;
        [SerializeField] ParticleSystem ps;
        [SerializeField] GameObject decalTile;


        private void OnEnable()
        {
            if (isHead)
                ChangePlayerStateEvent.OnEventRaised += OnChangeBacteriaState;
        }


        private void OnDisable()
        {
            if (isHead)
                ChangePlayerStateEvent.OnEventRaised -= OnChangeBacteriaState;
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


        void OnChangeBacteriaState(int newState)
        {
            state = (BacteriaState)newState;
            ChangeFaceReaction(state);
        }


        public void OnMove(Vector3 direction, GameObject playerGreenObj, GameObject parentObj, bool isClone)
        {
            //Debug.Log(direction);
            switch (state)
            {
                case BacteriaState.None:
                    //playerGreen.transform.position += direction;
                    break;
                case BacteriaState.Move:
                    transform.position += direction;
                    if (transform.position.y < 1)// && transform.position.y + direction.y < 1)
                    {
                        GameObject curObj = GameObject.Instantiate(decalTile, transform.position, Quaternion.identity);
                        DecalSplash decalSplash = curObj.GetComponent<DecalSplash>();
                        decalSplash.ShowSplash();
                        ps.Play();
                    }
                    break;
                case BacteriaState.Clone:
                    transform.position += direction;
                    if (isClone)
                    {
                        GameObject newObj = GameObject.Instantiate(playerGreenObj, transform.position, Quaternion.identity);
                        newObj.transform.parent = parentObj.transform;
                    }
                    break;
                case BacteriaState.Destruct:
                    //playerGreen.transform.position += direction;
                    break;
            }
        }


        public void OnPrevState()
        {
            int curStateVal = (int)state;
            curStateVal--;
            if (curStateVal < 0)
            {
                curStateVal = (int)(BacteriaState.Last) - 1;
            }
            //OnChangeBacteriaState(curStateVal);
            ChangePlayerStateEvent.RaiseEvent(curStateVal);
        }


        public void OnNextState()
        {
            int curStateVal = (int)state;
            curStateVal++;
            if (curStateVal > (int)(BacteriaState.Last) - 1)
            {
                curStateVal = 0;
            }
            //OnChangeBacteriaState(curStateVal);
            ChangePlayerStateEvent.RaiseEvent(curStateVal);
        }



    }


}

