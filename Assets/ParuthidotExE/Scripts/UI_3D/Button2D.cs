///-----------------------------------------------------------------------------
///
/// Button2D
/// 
/// 2D sprite based Button
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace MiniGames
{
    public class Button2D : MonoBehaviour
    {
        public int buttonNo;
        [SerializeField] Transform progress;
        [SerializeField] TMP_Text text;
        bool isAnimating = false;
        float animationTime = 0.0f;
        float animationLength = 2.0f;


        void Start()
        {
        }


        void Update()
        {
            if (isAnimating)
            {
                animationTime += Time.deltaTime;
                if (animationTime >= animationLength)
                {
                    animationTime = 0.0f;
                    isAnimating = false;
                    progress.gameObject.SetActive(false);
                }
                progress.localScale = Vector3.one * (animationTime / animationLength);
                //Debug.Log(progress.localScale);
            }
        }


        public void ShowProgressAnim()
        {
            progress.gameObject.SetActive(true);
            progress.localScale = Vector3.zero;
            isAnimating = true;
        }


    }


}

