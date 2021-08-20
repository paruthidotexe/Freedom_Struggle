using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public enum JigShapeTypes
    {
        None = 0,
        Square,
        Hex,
        Popular, // as in real world board 
        Shape_4,
        Shape_5,
        Tetris,
        Curves
    }


    public class JigLevelData
    {
        public int selectedPic = 0;
        public int pieceShape = (int)JigShapeTypes.Shape_4;
        public int pieceCount = 0;

        public JigLevelData()
        { }
    }


}

