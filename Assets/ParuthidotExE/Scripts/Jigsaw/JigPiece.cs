///-----------------------------------------------------------------------------
///
/// JigPiece
/// 
/// Jigsaw Piece
///
///-----------------------------------------------------------------------------

using UnityEngine;


namespace ParuthidotExE
{
    public class JigPiece : MonoBehaviour
    {
        // cell position
        public int x = 0;
        public int y = 0;
        public int ID = 0;// 0 = none
        // not used in the image
        public int state = 0; // 0 : tray ; 16 : mouse; 8 : board; 9 : board correct
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] GameObject selection;
        [SerializeField] ParticleSystem ps;

        void Start()
        {
            OnDeSelect();
        }


        void Update()
        {

        }


        public void OnSelect()
        {
            if (ps != null)
                ps.Play();
            if (selection != null)
                selection.SetActive(true);
        }


        public void OnDeSelect()
        {
            if (ps != null)
                ps.Stop();
            if (selection != null)
                selection.SetActive(false);
        }


        public void UpdateMaterials(Texture2D texture2D, Vector2 textureOffset, Vector2 textureScale)
        {
            if (meshRenderer != null)
            {
                meshRenderer.materials[0].mainTexture = texture2D;
                meshRenderer.materials[0].mainTextureScale = textureScale;
                meshRenderer.materials[0].mainTextureOffset = textureOffset;

                meshRenderer.materials[1].mainTexture = texture2D;
                meshRenderer.materials[1].mainTextureScale = textureScale;
                meshRenderer.materials[1].mainTextureOffset = textureOffset;
                Debug.Log(textureOffset + " : " + textureScale);
            }
        }

        public void UpdateMaterials(Vector2 textureOffset, Vector2 textureScale)
        {
            if (meshRenderer != null)
            {
                meshRenderer.materials[0].mainTextureScale = textureScale;
                meshRenderer.materials[0].mainTextureOffset = textureOffset;

                meshRenderer.materials[1].mainTextureScale = textureScale;
                meshRenderer.materials[1].mainTextureOffset = textureOffset;
                Debug.Log(textureOffset + " : " + textureScale);
            }
        }

    }


}

