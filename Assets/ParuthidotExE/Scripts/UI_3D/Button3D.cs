///-----------------------------------------------------------------------------
///
/// Button3D
/// 
/// button with tap event
///
///-----------------------------------------------------------------------------

using UnityEngine;
using TMPro;
using System.Collections;

namespace ParuthidotExE
{
    public enum Button3DStates
    {
        Normal = 0,
        HighLight,
        Pressed,
        Selected,
        Disabled
    }


    public class Button3D : MonoBehaviour
    {
        public Button3DStates state = Button3DStates.Normal;
        public int choiceIndex = 0;
        public TMP_Text textContent;
        public Color normalColor;
        public Color highlightColor;
        public Color pressedColor;
        public Color selectedColor;
        public MeshRenderer meshRenderer;
        public MaterialPropertyBlock propBlock;
        public Collider btnCollider;
        public IntChannelSO TapEvent;
        public Event clickedEvent;

        private void OnEnable()
        {
            InputMgr.ClickedAction += OnTapped;
        }


        private void OnDisable()
        {
            InputMgr.ClickedAction -= OnTapped;
        }


        void Start()
        {
            btnCollider = gameObject.GetComponent<Collider>();
            propBlock = new MaterialPropertyBlock();
            UpdateState();
        }


        void Update()
        {

        }


        public void SetTextContent(string val)
        {
            textContent.text = val;
        }


        void OnTapped(Vector3 pos)
        {
            //Debug.Log("OnTapped : " + pos);
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (btnCollider.Raycast(ray, out hitInfo, 50))
            {
                //Debug.Log("RaiseEvent : " + choiceIndex);
                TapEvent.RaiseEvent(choiceIndex);
                OnButtonPressed();
            }
        }


        void UpdateState()
        {
            switch (state)
            {
                case Button3DStates.Normal:
                    meshRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", normalColor);
                    meshRenderer.SetPropertyBlock(propBlock);
                    //meshRenderer.material.SetColor("_BaseColor", Color.white);
                    break;
                case Button3DStates.HighLight:
                    meshRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", highlightColor);
                    meshRenderer.SetPropertyBlock(propBlock);
                    //meshRenderer.material.SetColor("_BaseColor", Color.green);
                    break;
                case Button3DStates.Pressed:
                    meshRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", pressedColor);
                    meshRenderer.SetPropertyBlock(propBlock);
                    //meshRenderer.material.SetColor("_BaseColor", Color.green);
                    StartCoroutine(ButtonPressedEffect());
                    break;
            }
        }


        public void OnButtonPressed()
        {
            state = Button3DStates.Pressed;
            UpdateState();
        }


        public void OnButtonReleased()
        {
            state = Button3DStates.Normal;
            UpdateState();
        }


        IEnumerator ButtonPressedEffect()
        {
            yield return new WaitForSeconds(0.5f);
            OnButtonReleased();
            yield return null;
        }


    }


}

