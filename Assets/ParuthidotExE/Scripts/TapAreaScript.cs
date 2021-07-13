///-----------------------------------------------------------------------------
///
/// TapAreaScript
/// 
/// 2D Tap areas
///
///-----------------------------------------------------------------------------

using System.Collections;
using UnityEngine;

namespace ParuthidotExE
{

    public class TapAreaScript : MonoBehaviour
    {
        public float radius = 0.6f; // for Circle ray cast
        public SpriteRenderer spriteRenderer;
        public SpriteRenderer shadowRenderer;

        void Start()
        {

        }


        void Update()
        {

        }


        public void ChangeColor(Color newColor)
        {
            spriteRenderer.color = newColor;
        }


        public void ApplySpriteData(ShapeData shapeData)
        {
            spriteRenderer.sprite = shapeData.sprite;
            shadowRenderer.sprite = shapeData.sprite;

            if (shapeData.tintStatus)
                spriteRenderer.color = shapeData.color;
        }


        public void OnClicked()
        {
            StartCoroutine(PlayEffect());
        }


        IEnumerator PlayEffect()
        {
            transform.localScale = Vector3.one * 1.2f;
            yield return new WaitForSeconds(2.0f);
            transform.localScale = Vector3.one;
            yield return null;
        }


    }


}