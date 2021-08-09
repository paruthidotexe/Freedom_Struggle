///-----------------------------------------------------------------------------
///
/// PlayerController
/// 
/// Player Controller with new input systems
///
///-----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace ParuthidotExE
{
    public class InputMgr : MonoBehaviour
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

        public TMP_Text touchPosText;

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

        void Raise_ChangeStateAction(string val)
        {
            if (ChangeStateAction != null)
                ChangeStateAction(val);
        }


        // Input Editor hooks
        public void OnMoveInputAction(InputAction.CallbackContext context)
        {
            //if (context.performed)
            //{
            //    //Debug.Log("OnMoveInputAction : " + context.ReadValue<Vector2>());
            //    //Debug.Log(context.ReadValue<Vector3>());
            //    moveDirInputAction = context.ReadValue<Vector2>();
            //    moveDir.x = moveDirInputAction.x;
            //    moveDir.y = moveDirInputAction.y;
            //    moveDir.z = 0;
            //    Raise_MoveAction(moveDir);
            //}
        }


        public void OnMoveUpAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("OnMoveUp : " + context);
                //Debug.Log(context.ReadValue<Vector3>());
                //moveDirInputAction = context.ReadValue<Vector2>();
                moveDir.x = 0;
                moveDir.y = 1;
                moveDir.z = 0;
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
                moveDir.y = -1;
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


        public void OnTouchPosAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("OnMousePosAction" + context);
                mousePos = context.ReadValue<Vector2>();
                //Debug.Log("OnMousePosAction" + mousePos);
                //Vector3 newPos = Camera.current.ScreenToWorldPoint(mousePos);
            }
        }


        public void OnTouchAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (touchPosText != null)
                    touchPosText.text = "OnTouchAction";
                Debug.Log("OnLeftClickAction" + context);
                //Debug.Log("Tap Pos : " + context.ReadValue<Vector2>());
                Vector3 newPos = mousePos; //Camera.main.ScreenToWorldPoint(mousePos);
                newPos.z = 12;
                newPos = Camera.main.ScreenToWorldPoint(newPos);
                newPos.z = 0.5f;
                GameObject curTapPosObj = GameObject.Instantiate(tapPosObj);
                curTapPosObj.transform.position = newPos;
                Debug.Log(mousePos + " vs " + newPos + "vs" + Camera.main.ScreenPointToRay(mousePos).origin);
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                Raise_ClickedAction(mousePos);
            }
        }


        public void OnTapAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (touchPosText != null)
                    touchPosText.text = "OnTapAction" + mousePos;
                Debug.Log("OnTapAction" + context);
                //Debug.Log("Tap Pos : " + context.ReadValue<Vector2>());
                Vector3 newPos = mousePos; //Camera.main.ScreenToWorldPoint(mousePos);
                newPos.z = 12;
                newPos = Camera.main.ScreenToWorldPoint(newPos);
                newPos.z = 0.5f;
                GameObject curTapPosObj = GameObject.Instantiate(tapPosObj);
                curTapPosObj.transform.position = newPos;
                Debug.Log(mousePos + " vs " + newPos + "vs" + Camera.main.ScreenPointToRay(newPos).origin);
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                Raise_ClickedAction(mousePos);
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
                Raise_ChangeStateAction(context.control.displayName);
            }
        }


        public void OnChangeStateAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //Debug.Log("OnChangeStateAction " + context.control.displayName);
                Raise_ChangeStateAction(context.control.displayName);
            }
        }


    }


}


// 2do
// As Scriptable objects class
// Scriptable events as channel
//