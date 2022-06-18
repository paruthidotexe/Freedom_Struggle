using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParuthidotExE
{
    public class JigRandomChildren : MonoBehaviour
    {
        public int totalVisible = 1;
        public GameObject[] childObjects;


        void Start()
        {
            ShowRandom();
        }


        void Update()
        {

        }

        public void ShowRandom()
        {
            int curVisible = 0;
            for (int i = 0; i < childObjects.Length; i++)
            {
                childObjects[i].SetActive(false);
            }
            while (curVisible < totalVisible)
            {
                int i = Random.Range(0, childObjects.Length);
                if (!childObjects[i].activeSelf)
                {
                    curVisible++;
                    childObjects[i].SetActive(true);
                }
            }
        }

    }


}

