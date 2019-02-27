using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CCS
{
    public class CCSMirrorSlot : MonoBehaviour
    {
        [HideInInspector]
        public CCSBaseItem currentItem;

        private int currentItemAmount = 0;
        public TextMeshProUGUI amountOfItemsText;
        public Image icon;
        public Sprite defaultSlotIcon;
        public bool resetOnZeroItems;
        public void FixedUpdate()
        {
            if(currentItem != null)
            {
                
                currentItemAmount = CCSInventoryManager.Instance.GetNumberOfItems(currentItem.itemID);

                if(currentItemAmount == 0)
                {
                    if(resetOnZeroItems)
                    {
                        Reset();
                    }
                    else
                    {
                        icon.color = Color.gray;
                    }
                }
                else
                {
                    icon.color = Color.white;
                }
                UpdateSlotInfo();
            }
        }


        public void Initialize(CCSBaseItem item)     
        {
            if(item != null)
            {
                currentItem = item;
                currentItemAmount = CCSInventoryManager.Instance.GetNumberOfItems(item.itemID);
            }
        }

        public void Reset()
        {
            currentItem = null;
            currentItemAmount = 0;
            UpdateSlotInfo();
        }

        public void UpdateSlotInfo()
        {
            if(currentItem != null)
            {
                if (icon != null)
                {
                    icon.sprite = currentItem.icon;
                }
                if(amountOfItemsText != null)
                {
                    if(currentItemAmount>1)
                    {
                        amountOfItemsText.text = "x" + currentItemAmount;
                    }
                    else
                    {
                        amountOfItemsText.text = "";
                    }
                }
            }
            else
            {
                if (icon != null)
                {
                    icon.sprite = defaultSlotIcon ;
                }
                if(amountOfItemsText != null)
                {
                    amountOfItemsText.text = "";
                }
            }
            
        }

        public void MirrorUse()
        {
            if(currentItem != null)
            {
                CCSInventoryManager.Instance.UseItem(currentItem.itemID);
            }
        }
        public int ItemCount()
        {
            return currentItemAmount;
        }

        public void RemoveItem()
        {
            if(currentItem != null)
            {
                CCSInventoryManager.Instance.RemoveItem(currentItem.itemID);
            }
            
        }
    }
}
