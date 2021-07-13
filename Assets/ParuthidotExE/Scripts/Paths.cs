///-----------------------------------------------------------------------------
///
/// Paths
/// 
/// list of points, transforms
///
///-----------------------------------------------------------------------------

using UnityEngine;


namespace ParuthidotExE
{
    public class Paths : MonoBehaviour
    {
        int pathId = 0;
        public Vector3[] points;
        public Transform[] pointTransforms;


        void Start()
        {

        }


        void Update()
        {

        }


        public void CreatePath(Vector3[] points)
        {
            this.points = points;
        }


        public Vector3 GetStartPoint()
        {
            return pointTransforms[0].position;
        }


        public int GetPathId()
        {
            return pathId;
        }


        public void SetPathId(int newPathId)
        {
            pathId = newPathId;
        }


    }


}

