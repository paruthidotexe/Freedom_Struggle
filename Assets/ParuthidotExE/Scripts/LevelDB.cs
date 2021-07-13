///-----------------------------------------------------------------------------
///
/// LevelDB
/// 
/// Saved levels
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ParuthidotExE
{
    public class LevelDB
    {
        public LevelDB()
        {
            // should it be initialized here
            //gridData = new GridData();
        }


        // Pregenerated Levels
        public static GridData GetGridData(int width, int height)
        {
            GridData gridData = new GridData(width, height);
            int randomVal = Random.Range(0, 5);

            gridData.tiles = new int[,]
            {
                { 1, 1, 1, 1, 1, 1 },
                { 1, 128, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 10, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
            };

            if (randomVal == 0)
            {
                gridData.tiles = new int[,]
                {
                { 1, 1, 1, 1, 1, 1 },
                { 1, 128, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 10, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                };
            }
            if (randomVal == 1)
            {
                gridData.tiles = new int[,]
                {
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 128, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 10, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                };
            }

            if (randomVal == 2)
            {
                gridData.tiles = new int[,]
                {
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 128, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 2, 1, 1 },
                { 1, 1, 1, 2, 1, 1},
                { 1, 1, 1, 2, 1, 1},
                { 1, 1, 1, 2, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 10, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                };
            }

            if (randomVal == 3)
            {
                gridData.tiles = new int[,]
                {
                { 1, 1, 1, 2, 2, 2 },
                { 1, 1, 1, 1, 128, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 10, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                };
            }
            if (randomVal == 4)
            {
                gridData.tiles = new int[,]
                {
                { 2, 2, 2, 2, 2, 2 },
                { 1, 1, 1, 1, 128, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 2},
                { 1, 10, 1, 1, 1, 2 },
                { 1, 1, 1, 1, 1, 2 },
                };
            }
            return gridData;
        }


        public static GridData GetRandomGridData(int width, int height)
        {
            GridData gridData = new GridData(width, height);
            for (int i = 0; i < gridData.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < gridData.tiles.GetLength(1); j++)
                {
                    gridData.tiles[i, j] = Random.Range(0, 3);
                }
            }
            gridData.tiles[Random.Range(0, gridData.tiles.GetLength(0)), Random.Range(0, gridData.tiles.GetLength(1))] = 128;
            gridData.tiles[Random.Range(0, gridData.tiles.GetLength(0)), Random.Range(0, gridData.tiles.GetLength(1))] = 10;
            return gridData;
        }


    }

}
