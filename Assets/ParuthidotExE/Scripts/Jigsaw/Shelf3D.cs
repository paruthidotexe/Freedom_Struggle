///-----------------------------------------------------------------------------
///
/// JigsawShelf
/// 
/// Shelf to hold unised Jigsaw pieces 
///
///-----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ParuthidotExE
{
    public class Shelf3D : MonoBehaviour
    {
        [SerializeField] TMP_Text count;
        [SerializeField] List<Transform> itemList = new List<Transform>();
        int shelfCount = 3;
        float shelfHeight = 2.5f;
        float shelfStartYPos = -2.5f;
        float startYPos = -2.5f;

        [SerializeField] IntChannelSO TapEvent;
        [SerializeField] Collider scrollUpBtn;
        [SerializeField] Collider scrollDownBtn;

        private void OnEnable()
        {
            InputMgr.ClickedAction += OnTapped;
        }

        private void OnDisable()
        {
            InputMgr.ClickedAction -= OnTapped;
        }

        void Start()
        {
            count.text = "Pieces : 0";
        }


        void Update()
        {

        }

        public void ArrangeItems()
        {
            if (itemList.Count <= shelfCount)
                startYPos = shelfStartYPos;
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].position = new Vector3(transform.position.x, startYPos + i * shelfHeight, itemList[i].position.z);
            }
            if (itemList.Count > shelfCount && itemList[itemList.Count - 1].position.y < shelfStartYPos + (shelfCount - 1) * shelfHeight)
            {
                startYPos += shelfHeight;
                for (int i = 0; i < itemList.Count; i++)
                {
                    itemList[i].position = new Vector3(transform.position.x, startYPos + i * shelfHeight, itemList[i].position.z);
                }
            }
        }

        public void AddItem(Transform newItem)
        {
            itemList.Insert(0, newItem);
            newItem.parent = transform;
            ArrangeItems();
            count.text = "Pieces : " + itemList.Count;
        }


        public void RemoveItem(Transform newItem)
        {
            if (itemList.Remove(newItem))
            {
                ArrangeItems();
                count.text = "Pieces : " + itemList.Count;
            }
        }


        public void ScrollUp()
        {
            if (itemList.Count >= 3)
            {
                if (itemList[0].position.y < shelfStartYPos)
                {
                    startYPos += shelfHeight;
                }
            }
            ArrangeItems();
        }


        public void ScrollDown()
        {
            if (itemList.Count >= 3)
            {
                //Debug.LogError(itemList[itemList.Count - 1].position.y + " vs " + shelfStartYPos + shelfCount * shelfHeight);
                if (itemList[itemList.Count - 1].position.y >= shelfStartYPos + shelfCount * shelfHeight)
                {
                    startYPos -= shelfHeight;
                }
            }
            ArrangeItems();
        }


        void OnTapped(Vector3 pos)
        {
            //Debug.Log("OnTapped : " + pos);
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (scrollUpBtn.Raycast(ray, out hitInfo, 50))
            {
                ScrollUp();
            }
            if (scrollDownBtn.Raycast(ray, out hitInfo, 50))
            {
                ScrollDown();
            }
        }

    }


}

