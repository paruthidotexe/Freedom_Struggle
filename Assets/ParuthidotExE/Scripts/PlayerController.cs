///-----------------------------------------------------------------------------
///
/// PlayerController
/// 
/// Player Controller with new input systems
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;

namespace ParuthidotExE
{
    public class PlayerController : MonoBehaviour
    {
        Vector2 moveDirInputAction = Vector2.zero;
        Vector3 moveDir = Vector3.zero;

        public delegate void OnVector3(Vector3 moveDir);
        public static event OnVector3 OnMoveAction;
        public static event OnVector3 OnClickedAction;

        public GameObject tapPosObj;
        public Vector2 mousePos = Vector2.zero;

        void Start()
        {
        }


        void Update()
        {

        }


        // Events
        void Raise_OnMoveAction(Vector3 moveDir)
        {
            if (OnMoveAction != null)
                OnMoveAction(moveDir);
        }


        void Raise_OnClickedAction(Vector3 moveDir)
        {
            if (OnClickedAction != null)
                OnClickedAction(moveDir);
        }


        public void OnMoveInputAction(InputAction.CallbackContext context)
        {
            //if(context.performed)
            //{
            //    //Debug.Log("OnMoveInputAction : " + context.ReadValue<Vector2>());
            //    //Debug.Log(context.ReadValue<Vector3>());
            //    //moveDirInputAction = context.ReadValue<Vector2>();
            //    moveDir.x = moveDirInputAction.x;
            //    moveDir.y = 0;
            //    moveDir.z = moveDirInputAction.y;
            //    Raise_OnMoveAction(moveDir);
            //}
        }


        public void OnMoveLeftAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("OnMoveLeft : " + context);
                //Debug.Log(context.ReadValue<Vector3>());
                //moveDirInputAction = context.ReadValue<Vector2>();
                moveDir.x = -1;
                moveDir.y = 0;
                moveDir.z = 0;
                Raise_OnMoveAction(moveDir);
            }
        }


        public void OnMoveRightAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("OnMoveRight : " + context);
                //Debug.Log(context.ReadValue<Vector3>());
                //moveDirInputAction = context.ReadValue<Vector2>();
                moveDir.x = 1;
                moveDir.y = 0;
                moveDir.z = 0;
                Raise_OnMoveAction(moveDir);
            }
        }


        public void OnMoveUpAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("OnMoveUp : " + context);
                //Debug.Log(context.ReadValue<Vector3>());
                //moveDirInputAction = context.ReadValue<Vector2>();
                moveDir.x = 0;
                moveDir.y = 0;
                moveDir.z = 1;
                Raise_OnMoveAction(moveDir);
            }
        }


        public void OnMoveDownAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("OnMoveDown : " + context);
                //Debug.Log(context.ReadValue<Vector3>());
                //moveDirInputAction = context.ReadValue<Vector2>();
                moveDir.x = 0;
                moveDir.y = 0;
                moveDir.z = -1;
                Raise_OnMoveAction(moveDir);
            }
        }


        public void OnLeftClickAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("OnLeftClickAction" + context);
                //Debug.Log("Tap Pos : " + context.ReadValue<Vector2>());
                Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
                newPos.z = 0.5f;
                GameObject curTapPosObj = GameObject.Instantiate(tapPosObj);
                curTapPosObj.transform.position = newPos;
                //Debug.Log(mousePos + " vs " + newPos);
                Raise_OnClickedAction(newPos);
            }
        }


        public void OnTapAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("OnTapAction" + context);
                Debug.Log("Tap Pos : " + context.ReadValue<Vector2>());
            }
        }


        public void OnMousePosAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("OnMousePosAction" + context);
                mousePos = context.ReadValue<Vector2>();
                //Debug.Log("OnMousePosAction" + mousePos);
                //Vector3 newPos = Camera.current.ScreenToWorldPoint(mousePos);
            }
        }


        public void OnSwipeAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("OnSwipeAction" + context);
                //tapPosObj.transform.position = 
            }
        }
    }

    // 2do
    // As Scriptable objects class
    // Scriptable events as channel
    //
}


