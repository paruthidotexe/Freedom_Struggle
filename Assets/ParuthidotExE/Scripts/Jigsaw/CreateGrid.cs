using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParuthidotExE
{
    public class CreateGrid : MonoBehaviour, IEditorApply
    {
        [SerializeField] Vector2Int gridDimension;
        GridData gridData;
        [SerializeField] GameObject tilePrefab;
        [SerializeField] GameObject[] tileMeshes;

        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {

        }


        public void Apply()
        {
            gridData = new GridData(gridDimension.x, gridDimension.y);
            for (int i = 0; i < gridData.width; i++)
            {
                for (int j = 0; j < gridData.height; j++)
                {
                    GameObject curObj = GameObject.Instantiate(tilePrefab);
                    curObj.transform.position = new Vector3(i + 0.5f, j + 0.5f, -0.05f);
                    curObj.name = "JigTile_5x5_" + i + "_" + j;

                    tileMeshes[i * gridData.height + j].transform.parent = curObj.transform;
                }
            }
        }


    }


}

