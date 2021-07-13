///-----------------------------------------------------------------------------
///
/// MouseTapEffect
/// 
/// Data of Shape
///
///-----------------------------------------------------------------------------


using UnityEngine;

public class MouseTapEffect : MonoBehaviour
{
    public ParticleSystem ps;
    public ParticleSystem.MainModule psMain;


    void Start()
    {
        Destroy(gameObject, 2.0f);
    }


    void Update()
    {

    }


    public void PlayEffect(Color effectColor)
    {
        ps.gameObject.SetActive(true);
        psMain = ps.main;
        psMain.startColor = effectColor;
    }


}

