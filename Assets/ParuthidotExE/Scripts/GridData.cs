///-----------------------------------------------------------------------------
///
/// GridData
/// 
/// Grid data for storing as json / scriptable objects
///
///-----------------------------------------------------------------------------

using UnityEngine;

namespace ParuthidotExE
{
    public class GridData
    {
        public int width = 8;
        public int height = 8;
        public int length = 8;
        public float cellSize = 1.0f;

        public int gridAxis = 2;// 1 =  XY; 2 = XZ 
        public int[,] tiles;

        public Vector3 orgin = Vector3.zero;


        public GridData()
        {
            InitGrid();
        }


        public GridData(int newWidth, int newHeight)
        {
            width = newWidth;
            height = newHeight;
            if (gridAxis == 2)
                length = newHeight;
            InitGrid();
        }


        public void InitGrid()
        {
            if (gridAxis == 2)
                height = length;

            tiles = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tiles[i, j] = 0;
                }
            }
            //Debug.Log(GetGridAsString());
        }


        public string GetGridAsString()
        {
            string gridStr = "";
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridStr += tiles[i, j];
                }
                gridStr += "\n";
            }
            Debug.Log(gridStr);
            return gridStr;
        }


        public void SetTileValue(int x, int y, int value)
        {
            tiles[x, y] = value;
        }


        public int GetTileValue(int x, int y)
        {
            return tiles[x, y];
        }


        public void SetTileValue(Vector3 worldPos, int value)
        {

        }


        public int GetTileValue(Vector3 worldPos)
        {
            return 0;
        }


        public Vector3 GetWorldPos()
        {
            return Vector3.zero;
        }


    }


    public class TileData
    {
        public TileData()
        {

        }
    }

}