///-----------------------------------------------------------------------------
///
/// ThemesDB
/// 
/// Themes list
///
///-----------------------------------------------------------------------------

using System;
using UnityEngine;


namespace ParuthidotExE
{
    [CreateAssetMenu(fileName = "NewThemesDB", menuName = "VerticalFalling/ThemesDB", order = 1)]
    [Serializable]
    public class ThemesDB : ScriptableObject
    {
        public Color[] colors;
        public ThemeData[] themeList;

        public VoidChannelSO ChangeTheme;

        public ThemeData GetRandomTheme()
        {
            if (themeList.Length <= 0)
            {
                ThemeData themeData = ScriptableObject.CreateInstance<ThemeData>();
                return themeData;
            }
            return themeList[UnityEngine.Random.Range(0, themeList.Length)];
        }


        public Color GetRandomColor()
        {
            if (colors.Length <= 0)
            {
                return Color.white;
            }
            return colors[UnityEngine.Random.Range(0, colors.Length)];
        }


    }


}
//Complementary Color 
//Analogous Color 
//Split Complementary Color 
//Triadic Color 
//Tetradic Color 
//Monochromatic Color

//Tint Color Variation
//Shade Color Variation

// Pastel Colors
// f8dadc
// e6cafb
// f3edd6
// f6d7e7
// b8dbf4
// c5f5d4

