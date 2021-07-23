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
        public delegate void OnStringDelegate(string val);
        public static event OnVector3 ClickedAction;
        public static event OnVector3 MoveAction;
        public static event OnStringDelegate ChangeStateAction;

        public GameObject tapPosObj;
        public Vector2 mousePos = Vector2.zero;

        void Start()
        {
        }


        void Update()
        {
        }


        // Events
        void Raise_MoveAction(Vector3 moveDir)
        {
            if (MoveAction != null)
                MoveAction(moveDir);
        }


        void Raise_ClickedAction(Vector3 moveDir)
        {
            if (ClickedAction != null)
                ClickedAction(moveDir);
        }

        void Raise_CloneAction(string val)
        {
            if (ChangeStateAction != null)
                ChangeStateAction(val);
        }


        // Input Editor hooks
        public void OnMoveInputAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("OnMoveInputAction : " + context.ReadValue<Vector2>());
                //Debug.Log(context.ReadValue<Vector3>());
                moveDirInputAction = context.ReadValue<Vector2>();
                moveDir.x = moveDirInputAction.x;
                moveDir.y = moveDirInputAction.y;
                moveDir.z = 0;
                Raise_MoveAction(moveDir);
            }
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
                Raise_MoveAction(moveDir);
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
                Raise_MoveAction(moveDir);
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
                Raise_MoveAction(moveDir);
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
                Raise_MoveAction(moveDir);
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
                Raise_ClickedAction(newPos);
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


        public void OnPlayerStateAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("a1 " + context);
                //Debug.Log("a2 " + context.valueType);
                ////Debug.Log("a3 " + context.ReadValueAsButton());
                //Debug.Log("a3 " + context.control);
                //Debug.Log("a4 " + context.control.displayName);
                //Debug.Log("a5 " + context.control.name);
                //Debug.Log("a6 " + context.control.path);
                //Debug.Log("a7 " + context.control.variants);
                //Debug.Log("a8 " + context.control.parent);

                //Debug.Log("OnPlayerStateAction " + context.control.displayName);
                Raise_CloneAction(context.control.displayName);
            }
        }


        public void OnChangeStateAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("OnChangeStateAction " + context.control.displayName);
                Raise_CloneAction(context.control.displayName);
            }
        }


    }


}


// 2do
// As Scriptable objects class
// Scriptable events as channel
//