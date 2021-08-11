///-----------------------------------------------------------------------------
///
/// ProgressBar2D
/// 
/// 2D sprite based ProgressBar
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGames
{
    public class ProgressBar2D : MonoBehaviour
    {
        [SerializeField] Transform[] sections;
        [SerializeField] Transform indicator;
        [SerializeField] Transform progressBarTransform;
        float progressBarLength = 0;
        int[] sectionLength; // in percentage
        Vector3 startOffset = Vector3.zero;
        float maxValue = 100;
        float minValue = 0;
        float indicatorValue = 0;
        float indicatorPos = 0;
        Vector3 scaleFactor = Vector3.one;

        [SerializeField] bool isAnimationEnabled;
        bool isAnimating = false;
        float animationTime = 0.0f;
        float animationLength = 2.0f;
        Vector3 animStartPos = Vector3.zero;
        Vector3 animEndPos = Vector3.zero;


        void Start()
        {
            scaleFactor = transform.localScale;
            progressBarLength = progressBarTransform.localScale.x;
            startOffset = progressBarTransform.position;
            //Debug.Log("startOffset : " + startOffset);
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
                    indicator.transform.position = animEndPos;
                }
                else
                {
                    indicator.transform.position = Vector3.Lerp(animStartPos, animEndPos, animationTime / animationLength);
                }
            }
        }


        public void Init(int maxValue, int[] sections, bool isAnimationEnabled = true)
        {
            this.maxValue = maxValue;
            this.sectionLength = sections;
            this.isAnimationEnabled = isAnimationEnabled;
            Vector3 startPos = startOffset;
            for (int i = 0; i < sectionLength.Length; i++)
            {
                float curLength = this.sectionLength[i] * progressBarLength / maxValue;
                this.sections[i].localScale = new Vector3(curLength, this.sections[i].localScale.y, this.sections[i].localScale.z);
                this.sections[i].position = startPos;
                startPos.x += this.sectionLength[i] * progressBarLength * scaleFactor.x / maxValue; ;
                indicator.transform.position = Vector3.zero + startOffset;
            }
        }


        public void Update(int newValue)
        {
            indicatorValue = newValue;
            if (indicatorValue > maxValue)
            {
                indicatorValue = maxValue;
            }
            else if (indicatorValue < 0)
            {
                indicatorValue = 0;
            }
            animStartPos = indicator.transform.position;
            float curLength = indicatorValue * progressBarLength * scaleFactor.x / maxValue;
            animEndPos = startOffset + new Vector3(curLength, 0, 0);
            if (isAnimationEnabled)
            {
                isAnimating = true;
                animationTime = 0;
            }
            else
            {
                indicator.transform.position = animEndPos;
            }
            //Debug.Log(indicatorValue + " = " + curLength + " = " + animStartPos + " = " + animEndPos);
        }


    }


}

