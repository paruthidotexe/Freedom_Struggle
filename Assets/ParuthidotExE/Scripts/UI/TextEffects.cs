///-----------------------------------------------------------------------------
///
/// TextEffects
/// 
/// Text mesh pro Effects
///
///-----------------------------------------------------------------------------

using UnityEngine;
using TMPro;


namespace ParuthidotExE
{
    public enum TextEffectTypes
    {
        None,
        SineWave,
        TypeWritter,
        Scale,
        Alternate,
    }


    public class TextEffects : MonoBehaviour
    {
        TMP_Text textComponent;


        void Start()
        {
            textComponent = gameObject.GetComponent<TMP_Text>();
            UpdateEffects();
        }


        void Update()
        {
            UpdateEffects();
        }


        void UpdateEffects()
        {
            textComponent.ForceMeshUpdate();
            TMP_TextInfo textInfo = textComponent.textInfo;
            for (int i = 0; i < textInfo.characterCount; i = i + 2)
            {
                var charInfo = textComponent.textInfo.characterInfo[i];
                Debug.Log(charInfo.character + " - " + charInfo.origin + " - " + charInfo.scale + " - " + charInfo.vertexIndex);
                textComponent.textInfo.characterInfo[i].scale *= 2;
                charInfo.scale *= 2;
                if (!charInfo.isVisible)
                {
                    continue;
                }
                Debug.Log(charInfo.character + " - " + charInfo.origin + " - " + charInfo.scale + " - " + charInfo.vertexIndex + " - " + textComponent.textInfo.characterInfo[i].scale);
                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
                Debug.Log("Verts : " + charInfo.materialReferenceIndex + " - " + verts);
                for (int j = 0; j < 4; j++)
                {
                    var origin = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = origin + new Vector3(0, i, 0);
                }
            }
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }
        }


    }


}

