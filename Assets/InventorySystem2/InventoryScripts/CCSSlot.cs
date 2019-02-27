using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace CCS
{


    [DisallowMultipleComponent]
    [CanEditMultipleObjects]
    public class CCSSlot : MonoBehaviour
    {

        //For use within items that you already have created, Make your item base class implement the Interface "ICCSItem"
        public List<CCSBaseItem> currentItems;

        protected CCSInventory currentInventory;
        public Image iconImage;
        public TextMeshProUGUI itemCountText;
        public Sprite emptySlotImage;

        [InformationButton("Click Actions Info", "Click Action", "Click actions are built in functions within the CCSSlot class. " +
            "The first click actions are the functions that are called when the Inventory Manager does not have a slot selected. \n\n" +
            "Common Function(s): \n Hold and Use")]

        public CCSSlotAction RightClickAction;
        public CCSSlotAction LeftClickAction;
        [Space]
        [InformationButton("Destination Click Info", "Destination Click Action", "Destination clicks are designed to function when the Inventory Manager is already holding an item. " +
            "Which means, that you have already clicked a slot with the 'Hold - Click Action' and it has temporarily saved the slot into the Inventory Manager and " +
            "allows for switch and stack functions to be usable.\n\n Common Function(s): Switch")]
        public CCSSlotAction RightDestinationClickAction;
        public CCSSlotAction LeftDestinationClickAction;

        [Space]
        [InformationButton("Alternate Key Click Info", "Alternate Click Actions", "Alternate clicks are designed with the thought that you can add more functionality " +
            "to the slot with the use of a key being held down during the click.\n\n" +
            "Example of this is World of Warcraft.\n" +
            "Holding shift and clicking on a stackable item allows for the item's stack to be split.\n\n" +
            "Common Function(s): Split")]
        public KeyCode AlternateRightHoldKey;
        public CCSSlotAction AltRightClickAction;
        public KeyCode AlternateLeftHoldKey;
        public CCSSlotAction AltLeftClickAction;





        protected UnityEvent rightFirstClick, rightSecondClick, leftFirstClick, leftSecondClick;
        protected UnityEvent altFirstRightClick, altFirstLeftClick;

        [Space]
        [InformationButton("Hotkey Info", "Hotkey Actions", "Allows for the slot to be clicked upon with the use of a keybinding allowing players to activate items with their keyboard."
            + "\n\nCommon Function(s): Use")]
        public KeyCode hotKey;
        public CCSSlotAction hotKeyAction;




        public CCSInventory CurrentInventory
        {
            get { return currentInventory; }
        }



        // Start is called before the first frame update
        void Start()
        {
            currentInventory = GetComponentInParent<CCSInventory>();
            UpdateSlotInfo();
        }

        // Update is called once per frame
        void Update()
        {
            // HotKey 
            if (Input.GetKeyDown(hotKey))
            {
                PerformAction(hotKeyAction);
            }
        }

        public virtual void PerformClick(CCSClickState state)//ClickStatus,Right or Left
        {
            switch (state)
            {
                case CCSClickState.FirstLeft:
                    if (!Input.GetKey(AlternateLeftHoldKey))
                    {
                        PerformAction(LeftClickAction); 
                    }
                    else
                    {
                        PerformAction(AltLeftClickAction);
                    }
                    break;
                case CCSClickState.FirstRight:
                    if (!Input.GetKey(AlternateRightHoldKey))
                    {
                        PerformAction(RightClickAction);
                    }
                    else
                    {
                        PerformAction(AltRightClickAction); 
                    }
                    break;
                case CCSClickState.DestinationLeft:
                    PerformAction(LeftDestinationClickAction);
                    break;
                case CCSClickState.DesitnationRight:
                    PerformAction(RightDestinationClickAction);
                    break;
                default:
                    break;
            }
        }


        public virtual void PerformAction(CCSSlotAction action)
        {
            switch (action)
            {
                case CCSSlotAction.Hold:
                    HoldItem();
                    break;
                case CCSSlotAction.Switch:
                    Switch();
                    break;
                case CCSSlotAction.Use:
                    Use();
                    break;
                case CCSSlotAction.Split:
                    Split();
                    break;
                case CCSSlotAction.Pickup:
                    PickupItem();
                    break;
                default:
                    break;
            }
        }

        public CCSBaseItem CheckItem
        {
            get
            {
                if (IsEmpty)
                {
                    return null;
                }
                return currentItems[0];
            }
        }

        public int ItemCount()
        {
           
            return currentItems.Count;
        }

        /// <summary>
        /// Initializes the slot with all of the information that is needed for it to run.
        /// </summary>
        /// <param name="inventory"></param>
        public void InitializeSlot(CCSInventory inventory)
        {
            currentInventory = inventory;
        }

        #region Default Slot Functions

        /// <summary>
        /// Tries to add 1 additional item to the slot.
        /// </summary>
        /// <param name="item"></param>
        public bool AddItem(CCSBaseItem item)
        {
            if (IsEmpty || (CheckItem.itemID == item.itemID && ItemCount() < CheckItem.stackSize))
            {
                currentItems.Add(item);
                UpdateSlotInfo();
                return true;
            }
            return false;

        }
       

        /// <summary>
        /// Removes 1 item from the slot if possible.
        /// </summary>
        public bool RemoveItem()
        {
            if (!IsEmpty)
            {
                currentItems.RemoveAt(0);
                UpdateSlotInfo();
                return true;
            }
            return false;

        }

        /// <summary>
        /// Use with caution, can remove pointers to the item.
        /// </summary>
        public void ClearSlot()
        {
            int count = ItemCount();
            for (int i = 0; i < count; i++)
            {

                RemoveItem();
            }
        }

        /// <summary>
        /// Returns true if there is the stack size limit of items in the slot.
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (!IsEmpty)
                {
                    Debug.Log(currentItems.Count);
                    return (currentItems[0].stackSize <= currentItems.Count);
                    
                }
                else
                    return true;
            }
        }

        /// <summary>
        /// Returns true if there are currently no items in the slot.
        /// </summary>
        public bool IsEmpty
        {
            get { return (currentItems.Count == 0); }
        }

        /// <summary>
        /// Updates the slots image, text and CD to match that of the item's information.
        /// </summary>
        public void UpdateSlotInfo()
        {
            if(!IsEmpty)
            {
                if(iconImage != null)
                {
                    if(CheckItem != null)
                    {
                        iconImage.sprite = CheckItem.icon;
                    }
                    
                }
                if(itemCountText != null)
                {
                    if (ItemCount() > 1)
                    {
                        itemCountText.text = "x" + ItemCount().ToString();
                    }
                    else
                    {
                        itemCountText.text = "";
                    }
                }
                
            }
            else
            {
                if (iconImage != null)
                {
                    iconImage.sprite = emptySlotImage;
                }
                
                if(itemCountText != null)
                {
                    itemCountText.text = "";
                }
            }
        }



        #endregion

        #region Inventory Manager Interactions
        public void HoldItem()
        {
            //Calls the inventory manager to save the slot as the first slot clicked.
            Debug.Log("Holding Item");
            CCSInventoryManager.Instance.SaveSlot(this);

        }

        public void PickupItem()
        {
            CCSInventoryManager.Instance.TryPickupDefault(this);
        }

        public void Switch()
        {
            Debug.Log("Switching items");
            CCSInventoryManager.Instance.SwitchSlots(this);
        }

        public void Split()
        {
            Debug.Log("Splitting Item");
            CCSInventoryManager.Instance.SaveSlot(this);
            CCSSplitter.Instance.OpenSplitter(this);
        }


       
        #endregion


        //Use for all custom click functionality that you which to have the items perform.
        #region Item Functionality

        


        /// <summary>
        /// Calls up the use function within the current. 
        /// </summary>
        public bool Use()
        {
            Debug.Log("Trying To Use Item");
            if (!IsEmpty)
            {
                
                if (IsItemUsable())
                {
                    
                    //Sending the current slot to the item allow for it to manage interactions of the slot with the inventory manager and 
                    //also allows for more customizeable functions to be used.
                    Debug.Log("Using Item");
                    currentItems[0].Use(this);
                    return true;
                }
                //Added Functionality.
                if (IsItemEquippable())
                {
                    Debug.Log("Trying To Equip Item");
                    currentItems[0].Use(this);
                }
            }
            return false;
        }

        public bool IsItemUsable()
        {
            if (CheckItem != null && currentInventory != null)
            {
                if (CheckItem.GetType() == typeof(CCSArmor) || CheckItem.GetType() == typeof(CCSWeapon))
                {
                    //Debug.Log(CheckItem + " is in Equipment Inventory: " + (currentInventory.InventoryType == CCSInventoryType.Equipment));
                    return currentInventory.InventoryType == CCSInventoryType.Equipment;
                    
                }
                return EnumFlags<CCSItemType>.HasFlag(CheckItem.ItemType, currentInventory.UseableItemTypes);
            }
            else
            {
                return false;
            }
                
        }
       


        /// <summary>
        /// Updates the CDDisplay to match the information sent.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator CoolDownTimer(float time)
        {
            yield return new WaitForSeconds(time);
        }
        #endregion




        #region User Added Functions:
        public bool IsItemEquippable()
        {
            if (CheckItem != null)
            { 
                
                if (CheckItem.GetType() == typeof(CCSWeapon) || CheckItem.GetType() == typeof(CCSArmor))
                {
                    return true;
                }
            }

            return false;

        }

        public void StartCoolDownDisplay(float time)
        {

        }

        #endregion
    }
}
