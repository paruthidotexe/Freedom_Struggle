///-----------------------------------------------------------------------------
///
/// Player
/// 
/// Main Player 
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParuthidotExE
{
    public class Player : MonoBehaviour
    {
        Vector3 startPos = Vector3.zero;
        Vector3 endPos = Vector3.zero;
        bool isMoving = false;
        Vector3 dir = Vector3.zero;
        float speed = 10;
        float lerpTime = 0;
        bool isAnimationOn = false;

        void Start()
        {

        }


        void Update()
        {
            if (isAnimationOn)
            {
                if (isMoving)
                {
                    // transform.position += dir * speed * Time.deltaTime;
                    lerpTime += Time.deltaTime;
                    transform.position = Vector3.Lerp(startPos, endPos, lerpTime);
                    if (lerpTime >= 1)
                    {
                        isMoving = false;
                        lerpTime = 0;
                        transform.position = endPos;
                    }
                }
            }
        }


        public void OnMoveAction(Vector3 direction)
        {
            //Debug.Log("OnMove :" + direction);
            startPos = transform.position;
            if (isAnimationOn)
            {
                if (isMoving)
                    endPos = endPos + direction;
                else
                    endPos = transform.position + direction;
                isMoving = true;
                lerpTime = 0;
            }
            else
            {
                transform.position += direction;
            }
        }

    }
}
