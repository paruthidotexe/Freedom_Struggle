///-----------------------------------------------------------------------------
///
/// ToggleButton3D
/// 
/// checkbox, toggle, radio
///
///-----------------------------------------------------------------------------

using UnityEngine;
using TMPro;


namespace ParuthidotExE
{

    public class ToggleButton3D : Button3D
    {
        [SerializeField] bool isSelected = false;

        void UpdateState()
        {
            switch (state)
            {
                case Button3DStates.Normal:
                    meshRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", normalColor);
                    meshRenderer.SetPropertyBlock(propBlock);
                    break;
                case Button3DStates.HighLight:
                    meshRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", highlightColor);
                    meshRenderer.SetPropertyBlock(propBlock);
                    break;
                case Button3DStates.Pressed:
                    meshRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", pressedColor);
                    meshRenderer.SetPropertyBlock(propBlock);
                    break;
                case Button3DStates.Selected:
                    meshRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", selectedColor);
                    meshRenderer.SetPropertyBlock(propBlock);
                    break;
            }
        }


    }


}

// 2do
// need to implement
//