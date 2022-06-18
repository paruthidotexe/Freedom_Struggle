using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParuthidotExE
{
    public class JigRandomLevel : MonoBehaviour, IEditorApply
    {
        void Start()
        {
            RandomizeLevel();
        }


        void Update()
        {

        }


        public void Apply()
        {
            RandomizeLevel();
        }


        public void RandomizeLevel()
        {
            JigRandomChildren[] allChildren = gameObject.GetComponentsInChildren<JigRandomChildren>();
            foreach (JigRandomChildren curChild in allChildren)
            {
                curChild.ShowRandom();
            }
        }


    }


}

