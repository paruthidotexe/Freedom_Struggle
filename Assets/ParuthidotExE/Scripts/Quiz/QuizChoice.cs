///-----------------------------------------------------------------------------
///
/// QuizChoices
/// 
/// Choices
///
///-----------------------------------------------------------------------------

using UnityEngine;
using TMPro;


namespace ParuthidotExE
{
    public class QuizChoice : MonoBehaviour
    {
        [SerializeField] int choiceIndex = 0;
        [SerializeField] TMP_Text numberText;
        [SerializeField] TMP_Text choiceText;
        [SerializeField]
        QuestionDataChannelSO SetQuestionEvent;
        [SerializeField] IntChannelSO TapEvent;
        Collider btnCollider;
        [SerializeField] Color normalColor;
        [SerializeField] Color highlightColor;
        [SerializeField] Color selectedColor;
        [SerializeField] MeshRenderer meshRenderer;
        MaterialPropertyBlock propBlock;
        bool isSelected = false;


        private void OnEnable()
        {
            SetQuestionEvent.OnEventRaised += SetQuestion;
            InputMgr.ClickedAction += OnTapped;
        }


        private void OnDisable()
        {
            SetQuestionEvent.OnEventRaised -= SetQuestion;
            InputMgr.ClickedAction -= OnTapped;
        }


        void Start()
        {
            btnCollider = gameObject.GetComponent<Collider>();
            propBlock = new MaterialPropertyBlock();
            UpdateColors();
        }


        void Update()
        {

        }


        public void SetChoice(string val)
        {
            choiceText.text = val;
        }


        public void SetQuestion(QuestionData newQuestionData)
        {
            isSelected = false;
            UpdateColors();
            if (choiceIndex - 1 >= 0 && choiceIndex - 1 < newQuestionData.choices.Length)
                choiceText.text = newQuestionData.choices[choiceIndex - 1];
        }


        void OnTapped(Vector3 pos)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (btnCollider.Raycast(ray, out hitInfo, 20))
            {
                isSelected = !isSelected;
                //Debug.Log("RaiseEvent : " + choiceIndex);
                TapEvent.RaiseEvent(choiceIndex);
                UpdateColors();
            }
        }


        void UpdateColors()
        {
            if (isSelected)
            {
                meshRenderer.GetPropertyBlock(propBlock);
                propBlock.SetColor("_BaseColor", selectedColor);
                meshRenderer.SetPropertyBlock(propBlock);
                //meshRenderer.material.SetColor("_BaseColor", Color.green);
            }
            else
            {
                meshRenderer.GetPropertyBlock(propBlock);
                propBlock.SetColor("_BaseColor", normalColor);
                meshRenderer.SetPropertyBlock(propBlock);
                //meshRenderer.material.SetColor("_BaseColor", Color.white);
            }
        }


        public void SetSelection(bool val)
        {
            isSelected = val;
            UpdateColors();
        }

    }


}

