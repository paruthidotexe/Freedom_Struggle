///-----------------------------------------------------------------------------
///
/// StarsUI
/// 
/// Stars at game over screen
///
///-----------------------------------------------------------------------------

using UnityEngine;


namespace MiniGames
{
    public class StarsUI : MonoBehaviour
    {
        int starsUICount = 0;
        [SerializeField] GameObject[] starsList;


        void Start()
        {
        }


        void Update()
        {
        }


        public void SetStars(int noOfStars)
        {
            starsUICount = noOfStars;
            for (int i = 0; i < starsList.Length; i++)
            {
                if (i <= starsUICount)
                    starsList[i].SetActive(true);
                else
                    starsList[i].SetActive(false);

            }
        }


    }


}

