///-----------------------------------------------------------------------------
///
/// ThemeData
/// 
/// theme
///
///-----------------------------------------------------------------------------

using System;
using UnityEngine;


namespace ParuthidotExE
{
    [CreateAssetMenu(fileName = "NewThemeData", menuName = "VerticalFalling/ThemeData", order = 1)]
    [Serializable]
    public class ThemeData : ScriptableObject
    {
        public Color bgColor = Color.white;
        public Color fgColor = Color.black; // buttons
        public Color fontColor = Color.black;
        public Color iconColor = Color.white;
    }


}
//
// fonts
// icon sets
// button sets
//
