///-----------------------------------------------------------------------------
///
/// ShapeData
/// 
/// Data of Shape
///
///-----------------------------------------------------------------------------

using System;
using UnityEngine;


namespace ParuthidotExE
{
    [CreateAssetMenu(fileName = "NewShapeData", menuName = "VerticalFalling/ShapeData", order = 1)]
    [Serializable]
    public class ShapeData : ScriptableObject
    {
        public int shapeId;
        public int shapeType;
        public Sprite sprite;
        public Color color;
        public bool tintStatus;
    }


}

