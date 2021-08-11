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
        int rowCount = 3;
        float shelfHeight = 12;
        float rowHeight = 2.5f;
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
            //shelfCount = (int)(shelfHeight / rowHeight);
            count.text = "Pieces : 0";
        }


        void Update()
        {

        }


        public void SetRowHeight(float newHeight)
        {
            Debug.LogError(newHeight);
            rowHeight = newHeight + 0.4f;
            ArrangeItems();
        }


        public void ArrangeItems()
        {
            if (itemList.Count <= rowCount)
                startYPos = shelfStartYPos;
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].position = new Vector3(transform.position.x, startYPos + i * rowHeight, itemList[i].position.z);
            }
            if (itemList.Count > rowCount && itemList[itemList.Count - 1].position.y < shelfStartYPos + (rowCount - 1) * rowHeight)
            {
                startYPos += rowHeight;
                for (int i = 0; i < itemList.Count; i++)
                {
                    itemList[i].position = new Vector3(transform.position.x, startYPos + i * rowHeight, itemList[i].position.z);
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
            if (itemList.Count >= rowCount)
            {
                if (itemList[0].position.y < shelfStartYPos)
                {
                    startYPos += rowHeight;
                }
            }
            ArrangeItems();
        }


        public void ScrollDown()
        {
            if (itemList.Count >= rowCount)
            {
                //Debug.LogError(itemList[itemList.Count - 1].position.y + " vs " + shelfStartYPos + shelfCount * shelfHeight);
                if (itemList[itemList.Count - 1].position.y >= shelfStartYPos + rowCount * rowHeight)
                {
                    startYPos -= rowHeight;
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

