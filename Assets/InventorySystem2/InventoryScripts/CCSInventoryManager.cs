using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CCS
{
    
    public class CCSInventoryManager : MonoBehaviour
    {
        public static CCSInventoryManager Instance;
        public CCSSlot firstSlotClicked;
        [SerializeField]
        protected List<CCSBaseItem> tempItems = new List<CCSBaseItem>();
        public CCSInventoryType pickupItemInventory;
        //Selections of the slots.
        EventSystem eventSystem;
        GraphicRaycaster raycaster;
        PointerEventData pointerData;
        List<RaycastResult> raycastHits;

        CCSInventory[] inventories;

        /// <summary>
        /// Sets up the manager by finding all of the inventories within the game scene and setting up any interactions that may be needed.
        /// </summary>
        protected void InitializeManager()
        {
            eventSystem = FindObjectOfType<EventSystem>();
            raycaster = FindObjectOfType<GraphicRaycaster>();
            pointerData = new PointerEventData(eventSystem);
            inventories = FindObjectsOfType<CCSInventory>();
        }

        internal void PickupPotion(Slot slot1, CCSSlot slot2)
        {
            throw new NotImplementedException();
        }

        public void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Awake()
        {
            Singleton();
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeManager();
            
        }

        // Update is called once per frame
        void Update()
        {
            RaycastOverUI();
            CheckMouseOver();


            //If other Popup Menus are added, they may need to be checked so mouseclicks do not occur.
            if (!CCSDeleter.Instance.IsEnabled && !CCSSplitter.Instance.IsEnabled)
            {
                //Allowing for Dragging slots.
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    LeftMouseUpClick();
                }
                //LeftClick
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    LeftMouseClick();
                }
                //Right Click
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    RightMouseClick();
                }
            }
        }
        


        /// <summary>
        /// Allows for the use of left mouse up clicks, this is defaulted as using the DistinationLeft function if the first slot is filled.
        /// </summary>
        public void LeftMouseUpClick()
        {
            
            if(raycastHits.Count>0)
            {
                

                foreach (RaycastResult hit in raycastHits)
                {
                    CCSSlot slot = hit.gameObject.GetComponent<CCSSlot>();
                    if (slot != null && slot != firstSlotClicked)
                    {
                        if(HasFirstSlot)
                        {
                            slot.PerformClick(CCSClickState.DestinationLeft);
                        }
                    }



                    /**Add More Functionality here if you wish the click to interact with other objects with the LeftClick
                    * 
                    * Example:
                    * CharacterDisplay char = hit.gameobject.GetComponent<CharacterDisplay>();
                    * if(char != null){
                    *      OpenTradeWindow();
                    * }
                    *  
                    **/
                }

            }
        }




        public void LeftMouseClick()
        {
            
            //Checking if the mouse is currently over the UI
            if (raycastHits.Count > 0)
            {
                //Checks to see if the UI that is clicked on is a mirror slot. 
                
                if(MirrorClick(CCSClickState.FirstLeft))
                {
                    return;
                }

                //Checking for the Slot component to perform mouse operations.
                foreach (RaycastResult hit in raycastHits)
                {
                    CCSSlot slot = hit.gameObject.GetComponent<CCSSlot>();
                    if (slot != null)
                    {
                        if (HasFirstSlot)
                            slot.PerformClick(CCSClickState.DestinationLeft);
                        else
                            slot.PerformClick(CCSClickState.FirstLeft);
                    }


                    /**Add More Functionality here if you wish the click to interact with other objects with the LeftClick
                     * 
                     * Example:
                     * CharacterDisplay char = hit.gameobject.GetComponent<CharacterDisplay>();
                     * if(char != null){
                     *      OpenTradeWindow();
                     * }
                     *  
                     **/


                }
            }
            //Since the click was not over the UI and instead on the game scene. Opens the Delete Function.
            else if (HasFirstSlot)
            {
                CCSDeleter.Instance.OpenDeleter();
            }
        }

        public void RightMouseClick()
        {
            
            if (raycastHits.Count > 0)
            {
                //Checks to see if the UI that is clicked on is a mirror slot. 
                if (MirrorClick(CCSClickState.FirstRight))
                {
                    return;
                }

                foreach (RaycastResult hit in raycastHits)
                {
                    CCSSlot slot = hit.gameObject.GetComponent<CCSSlot>();
                    if (slot != null)
                    {
                        if (HasFirstSlot)
                            slot.PerformClick(CCSClickState.DesitnationRight);
                        else
                            slot.PerformClick(CCSClickState.FirstRight);
                    }
                }

                /**Add More Functionality here if you wish the click to interact with other objects with the RightClick
                    * 
                    * Example:
                    * CharacterDisplay char = hit.gameobject.GetComponent<CharacterDisplay>();
                    * if(char != null){
                    *      OpenTradeWindow();
                    * }
                    *  
                    **/

            }
            else if (HasFirstSlot || tempItems.Count > 0)
            {
                ResetManager();
            }
        }


        /// <summary>
        /// Sets up the raycastHits variable and also allows other classes to retieve the raycast's information.
        /// </summary>
        /// <returns></returns>
        public List<RaycastResult> RaycastOverUI()
        {
            pointerData.position = Input.mousePosition;
            raycastHits = new List<RaycastResult>();
            raycaster.Raycast(pointerData, raycastHits);
            return raycastHits;
        }


        /// <summary>
        /// Checks if the first slot is null or not.
        /// </summary>
        public bool HasFirstSlot
        {
            get { return firstSlotClicked != null; }
        }
        

        /// <summary>
        /// Saves the clicked slot in the inventory manager for future use.
        /// </summary>
        /// <param name="slot"></param>
        public void SaveSlot(CCSSlot slot)
        {
            if (!slot.IsEmpty && firstSlotClicked != slot)
            {
                firstSlotClicked = slot;
                ChangeCursor(slot.CheckItem.CursorIcon,new Vector2(.5f,-.5f));
            }  
        }

        /// <summary>
        /// Tries to switch the slot's items that are currently selected.
        /// </summary>
        /// <param name="slotToSwitch"></param>
        /// <returns></returns>
        public bool SwitchSlots(CCSSlot slotToSwitch)
        {
            //Makins sure the same slot isnt clicked.
            if(slotToSwitch != firstSlotClicked )
            {
                //Checks to see if the first slot can be moved to the second slot.
                if(EnumFlags<CCSItemType>.HasFlag(firstSlotClicked.CheckItem.ItemType, slotToSwitch.CurrentInventory.AcceptedItemTypes))
                {

                    //User Functionality added to Switch for Equipment Items.
                    if(!CheckEquipmentItemStatus(slotToSwitch))
                    {
                        return false;
                    }
                   
                    

                    if (!slotToSwitch.IsEmpty)
                    {
                        //Checks to see if the slot contains an item that it is still able to switch.
                        if(!EnumFlags<CCSItemType>.HasFlag(slotToSwitch.CheckItem.ItemType, firstSlotClicked.CurrentInventory.AcceptedItemTypes))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
                //Check to make sure that both slot's inventories can hold eachothers items.
                if (tempItems.Count == 0)
                {
                    if(!slotToSwitch.IsEmpty && slotToSwitch.CheckItem.itemID == firstSlotClicked.CheckItem.itemID && !slotToSwitch.IsFull)
                    {
                        Debug.Log("Check");
                        if(StackItems(slotToSwitch))
                        {
                            ResetManager();
                            return true;
                        }
                    }
                    else
                    {
                        CCSBaseItem temp = firstSlotClicked.CheckItem; //Saving what item was in the slot.
                        int count = firstSlotClicked.ItemCount();  //Saving the number of items in the slot.

                        firstSlotClicked.ClearSlot();

                        //Adds all of the items from the Destination click to the first click.
                        int secondCount = slotToSwitch.ItemCount();
                        for (int i = 0; i < secondCount; i++)
                        {
                            firstSlotClicked.AddItem(slotToSwitch.CheckItem);
                            slotToSwitch.RemoveItem();
                        }
                        //Adds all of the items from the temporary saves to the second Destination click.
                        for (int i = 0; i < count; i++)
                        {
                            slotToSwitch.AddItem(temp);
                        }
                        ResetManager();
                        return true;
                    }
                }
                else
                {

                    Debug.Log("SplitSwitch");
                    if (slotToSwitch.IsEmpty)
                    {
                        int count = tempItems.Count;
                        for (int i = 0; i < count; i++)
                        {
                            slotToSwitch.AddItem(tempItems[0]);
                            firstSlotClicked.RemoveItem();
                        }
                        ResetManager();
                        return true;
                    }
                    //Checking for similar items to allow stacking.
                    else if(slotToSwitch.CheckItem.itemID == tempItems[0].itemID)
                    {
                        if(!slotToSwitch.IsFull)
                        {
                            StackItems(slotToSwitch);
                            ResetManager();
                            return true;
                        }
                    }
                   
                    
                }
            }
            return false;
            
        }

        
        /// <summary>
        /// Modifies the mouse Cursor to the icon being sent.
        /// </summary>
        /// <param name="icon"></param>
        public void ChangeCursor(Texture2D icon)
        {
            ChangeCursor(icon, Vector2.zero);

        }
        /// <summary>
        /// Modifies the mouse Cursor to the icon being sent, and changes the hotspot location.
        /// </summary>
        /// <param name="icon"></param>
        public void ChangeCursor(Texture2D icon, Vector2 hotspot)
        {
            Cursor.SetCursor(icon, hotspot, CursorMode.Auto);
        }


        /// <summary>
        /// Turns the Inventory Manager and all other required UI elements to be reset to default.
        /// </summary>
        public void ResetManager()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            firstSlotClicked = null;
            tempItems.Clear();
        }

        

        #region Item Delete and Split Popups


        /// <summary>
        /// Destroys all items within the designated slot or the number of items designated by the tempSlot.
        /// </summary>
        public void DeleteItems()
        {
            
            if(firstSlotClicked != null)
            {
                if (tempItems.Count > 0)
                {
                    int count = tempItems.Count;
                    for(int i = 0;i<count;i++)
                    {
                        firstSlotClicked.RemoveItem();
                    }
                    
                }
                else
                {
                    firstSlotClicked.ClearSlot();
                }
               
            }
            ResetManager();
        }


        

        /// <summary>
        /// Splits the item's stack to the number choosen within the CCSSplitter
        /// </summary>
        /// <param name="numberOfItems"></param>
        public void SplitItems(int numberOfItems)
        {

            if(HasFirstSlot)
            {
                tempItems.Clear();
                for (int i = 0; i < numberOfItems; i++)
                {
                    tempItems.Add(firstSlotClicked.CheckItem);
                }
                if (tempItems.Count>0)
                {
                    ChangeCursor(tempItems[0].CursorIcon);
                }
            }
            
                
        }

        /// <summary>
        /// Used in the switch function to see if the items can be pushed into the same slot.
        /// </summary>
        /// <param name="stackingSlot"></param>
        /// <returns></returns>
        public bool StackItems(CCSSlot stackingSlot)
        {
            bool aStackOccured = false;
            //Will default to temp Items if possible
            if(tempItems.Count>0)
            {
                int amountToStack = 0;
                if (tempItems.Count + stackingSlot.ItemCount() > tempItems[0].stackSize)
                {
                    amountToStack = tempItems[0].stackSize - stackingSlot.ItemCount();
                    for (int i = 0; i < amountToStack; i++)
                    {
                        stackingSlot.AddItem(firstSlotClicked.CheckItem);
                        firstSlotClicked.RemoveItem();
                        aStackOccured = true;
                    }
                }
                else
                {
                    amountToStack = tempItems.Count;
                    for (int i = 0; i < amountToStack; i++)
                    {
                        stackingSlot.AddItem(firstSlotClicked.CheckItem);
                        firstSlotClicked.RemoveItem();
                        aStackOccured = true;
                    }
                }
            }
            else
            {
                int amountToStack = 0;
                if(firstSlotClicked.ItemCount() + stackingSlot.ItemCount() > firstSlotClicked.CheckItem.stackSize)
                {
                    amountToStack = firstSlotClicked.CheckItem.stackSize - stackingSlot.ItemCount();
                    for(int i = 0;i<amountToStack;i++)
                    {
                        stackingSlot.AddItem(firstSlotClicked.CheckItem);
                        firstSlotClicked.RemoveItem();
                        aStackOccured = true;
                    }
                }
                else
                {
                    amountToStack = firstSlotClicked.ItemCount();
                    for(int i = 0;i<amountToStack;i++)
                    {
                        stackingSlot.AddItem(firstSlotClicked.CheckItem);
                        firstSlotClicked.RemoveItem();
                        aStackOccured = true;
                    } 
                }
                
            }
            return aStackOccured;
        }



        #endregion


        #region User Functions

        #region Mirror Slots


        /// <summary>
        /// Similar to the slot click but using the CCSMirrorSlot but is more determinate on what functionality is built into the Inventory Manager.
        /// </summary>
        private bool MirrorClick(CCSClickState state)
        {
            //Checking for the a MirrorSlot.
            foreach (RaycastResult hit in raycastHits)
            {
                CCSMirrorSlot mirror = hit.gameObject.GetComponent<CCSMirrorSlot>();
                if (mirror != null)
                {

                    if (HasFirstSlot)
                    {

                        
                        //Checks the item trying to be placed to make sure that it has a an ability that can be used.
                        if (firstSlotClicked.IsItemUsable())
                        {
                            mirror.Initialize(firstSlotClicked.CheckItem);
                            ResetManager();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //Checks to see if there is an item being held.
                        if(tempItems.Count > 0)
                        {
                            if(mirror.currentItem != null)
                            {
                                CCSBaseItem temp = mirror.currentItem;
                                mirror.Initialize(tempItems[0]);
                                tempItems.Clear();
                                tempItems.Add(temp);
                                ChangeCursor(temp.CursorIcon);
                            }
                            else
                            {
                                mirror.Initialize(tempItems[0]);
                                ResetManager();
                            }
                            
                        }
                        else if (Input.GetKey(KeyCode.LeftShift))
                        {
                            tempItems.Add(mirror.currentItem);
                            ChangeCursor(mirror.currentItem.CursorIcon);
                            mirror.Reset();
                        }
                        else
                        {
                            //Tries to use the item in the slot since it is not trying to be switched with another item.
                            MirrorUse(state, mirror.currentItem);
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Uses the item through the mirror slot
        /// </summary>
        /// <param name="state"></param>
        /// <param name="mirrorItem"></param>
        public void MirrorUse(CCSClickState state,CCSBaseItem mirrorItem)
        {
            foreach (CCSInventory inv in inventories)
            {
                foreach (CCSSlot slot in inv.slots)
                {

                    if (!slot.IsEmpty && slot.CheckItem != null && slot.CheckItem == mirrorItem)
                    {
                        if(state == CCSClickState.FirstLeft)
                        {
                            if(slot.IsItemUsable())
                            {
                                UseItem(mirrorItem.itemID);
                                return;
                            }
                        }
                        else
                        {
                            UseItem(mirrorItem.itemID);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Searches through all of the slots in all of the inventories to determine the amount of that item being held.
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public int GetNumberOfItems(int itemID)
        {
            //Calculates the number of items that are useable within the inventory.
            int numberOfItems =0;
            foreach(CCSInventory inv in inventories)
            {
                foreach(CCSSlot slot in inv.slots)
                {

                    if (slot.IsItemUsable() && !slot.IsEmpty && slot.CheckItem.itemID == itemID )
                    {
                        Debug.Log(slot.CheckItem);
                        numberOfItems += slot.ItemCount();
                    }
                }
            }
            Debug.Log(numberOfItems);
            return numberOfItems;
        }

        /// <summary>
        /// Uses an item within the inventory if possible 
        /// </summary>
        /// <param name="itemID"></param>
        public void UseItem(int itemID)
        {
            foreach (CCSInventory inv in inventories)
            {
                foreach (CCSSlot slot in inv.slots)
                {

                    if (!slot.IsEmpty && slot.CheckItem !=null && slot.CheckItem.itemID == itemID)
                    {
                       
                        if (slot.Use())
                        {
                            return;
                        }
                    }
                }
            }
        }


        public void RemoveItem(int itemID)
        {
            foreach (CCSInventory inv in inventories)
            {
                foreach (CCSSlot slot in inv.slots)
                {

                    if (!slot.IsEmpty && slot.CheckItem != null && slot.CheckItem.itemID == itemID)
                    {
                        Debug.Log("RemovingItem : " + itemID);
                        slot.RemoveItem();
                        return;
                    }
                }
            }
        }
        #endregion

 
        /// <summary>
        /// Searches the inventories for an Equipment type inventory and tries to add the item to the designated slot.
        /// returns true if it is successful.
        /// </summary>
        public bool TryEquipItem(CCSSlot slot)
        {
            if(slot.CheckItem == null)
            {
                return false;
            }

            foreach (CCSInventory inventory in inventories)
            {
                if(inventory.InventoryType == CCSInventoryType.Equipment)
                {
                    foreach(CCSSlot s in inventory.slots)
                    {
                        
                        firstSlotClicked = slot;
                        if (firstSlotClicked.CheckItem.GetType() == typeof(CCSArmor))
                        {
                            CCSArmor tempItem = (CCSArmor)firstSlotClicked.CheckItem;
                            if (s.GetType() == typeof(CCSArmorSlot))
                            {
                                CCSArmorSlot tempSlot = (CCSArmorSlot)s;
                                if (EnumFlags<CCSArmorType>.HasFlag(tempItem.armorType, tempSlot.acceptedArmorTypes))
                                {
                                    if (SwitchSlots(s))
                                        return true;
                                }
                            }

                        }
                        else if (firstSlotClicked.CheckItem.GetType() == typeof(CCSWeapon))
                        {
                            CCSWeapon tempItem = (CCSWeapon)firstSlotClicked.CheckItem;
                            if (s.GetType() == typeof(CCSWeaponSlot))
                            {
                                CCSWeaponSlot tempSlot = (CCSWeaponSlot)s;
                                if (EnumFlags<CCSWeaponType>.HasFlag(tempItem.weaponType, tempSlot.acceptedWeaponTypes))
                                {
                                    if (SwitchSlots(s))
                                        return true;
                                }
                            }

                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the item that is currently being designated to move will be allowed in the slot that it is going to.
        /// Also checks the switching slot if not empty to determine if the item being switched with is acceptable.
        /// </summary>
        /// <param name="slotToSwitch"></param>
        /// <returns></returns>
        public bool CheckEquipmentItemStatus(CCSSlot slotToSwitch)
        {
            if (firstSlotClicked.GetType() == typeof(CCSArmorSlot) || firstSlotClicked.GetType() == typeof(CCSWeaponSlot) ||
                        slotToSwitch.GetType() == typeof(CCSArmorSlot) || slotToSwitch.GetType() == typeof(CCSWeaponSlot))
            {

                if (slotToSwitch.GetType() == typeof(CCSArmorSlot))
                {
                    CCSArmorSlot tempSlot = (CCSArmorSlot)slotToSwitch;
                    if (firstSlotClicked.CheckItem.ItemType == CCSItemType.CCSEquipment)
                    {
                        CCSArmor tempItem = (CCSArmor)firstSlotClicked.CheckItem;
                        if (!EnumFlags<CCSArmorType>.HasFlag(tempItem.armorType, tempSlot.acceptedArmorTypes))
                        {
                            return false;
                        }
                    }

                }
                else if (slotToSwitch.GetType() == typeof(CCSWeaponSlot))
                {
                    CCSWeaponSlot tempSlot = (CCSWeaponSlot)slotToSwitch;
                    if (firstSlotClicked.CheckItem.ItemType == CCSItemType.CCSEquipment)
                    {
                        CCSWeapon tempItem = (CCSWeapon)firstSlotClicked.CheckItem;
                        if (!EnumFlags<CCSWeaponType>.HasFlag(tempItem.weaponType, tempSlot.acceptedWeaponTypes))
                        {
                            return false;
                        }
                    }
                }
                if (slotToSwitch.IsEmpty)
                {
                    //Check to see if the slot To switch can hold equipement.
                    if (!EnumFlags<CCSItemType>.HasFlag(firstSlotClicked.CheckItem.ItemType, slotToSwitch.CurrentInventory.AcceptedItemTypes))
                    {
                        return false;
                    }
                }
                else
                {
                    if (firstSlotClicked.GetType() == typeof(CCSArmorSlot))
                    {
                        CCSArmorSlot tempSlot = (CCSArmorSlot)firstSlotClicked;
                        if (slotToSwitch.CheckItem.ItemType == CCSItemType.CCSEquipment)
                        {
                            CCSArmor tempItem = (CCSArmor)slotToSwitch.CheckItem;
                            if (!EnumFlags<CCSArmorType>.HasFlag(tempItem.armorType, tempSlot.acceptedArmorTypes))
                            {
                                return false;
                            }
                        }

                    }
                    else if (firstSlotClicked.GetType() == typeof(CCSWeaponSlot))
                    {
                        CCSWeaponSlot tempSlot = (CCSWeaponSlot)firstSlotClicked;
                        if (slotToSwitch.CheckItem.ItemType == CCSItemType.CCSEquipment)
                        {
                            CCSWeapon tempItem = (CCSWeapon)slotToSwitch.CheckItem;
                            if (!EnumFlags<CCSWeaponType>.HasFlag(tempItem.weaponType, tempSlot.acceptedWeaponTypes))
                            {
                                return false;
                            }
                        }
                    }
                }

                if (slotToSwitch.IsEmpty)
                {
                    //Check to see if the slot To switch can hold equipement.
                    if (!EnumFlags<CCSItemType>.HasFlag(firstSlotClicked.CheckItem.ItemType, slotToSwitch.CurrentInventory.AcceptedItemTypes))
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        public bool TryPickupDefault(CCSBaseItem item)
        {
            return TryPickup(item, pickupItemInventory);
        }
        /// <summary>
        /// Allows for items to be sent to the inventory manager and will be automatically placed in an inventory that can hold it.
        /// Stacking will also be tried if a similar item is already there.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool TryPickupDefault(CCSSlot slot)
        {
            return TryPickup(slot, pickupItemInventory);
        }

        /// <summary> 
        /// Communicates with the Mouse Over Display to show the information of the item that the mouse is hovering over.
        /// </summary>
        public void CheckMouseOver()
        {
            foreach(RaycastResult hit in raycastHits)
            {
                CCSSlot slotHit = hit.gameObject.GetComponent<CCSSlot>();
                if (slotHit != null)
                {
                    //CCSMouseOverDisplay.Instance.Open(slotHit);
                    return;
                }
            }
        }
        public bool TryPickup(CCSBaseItem item, CCSInventoryType typeToEnter)
        {
            List<CCSInventory> foundInventories = new List<CCSInventory>();
            foreach (CCSInventory inv in inventories)
            {
                if (inv.InventoryType == typeToEnter)
                {
                    foundInventories.Add(inv);
                }
            }
             //Checking each slot that are possible to add to.
                foreach (CCSInventory inv in foundInventories)
                {
                    foreach (CCSSlot s in inv.slots)
                    {
                        if (!s.IsEmpty  && s.CheckItem.itemID == item.itemID)
                        {

                            if (!s.IsFull)
                            {
                                s.AddItem(item);
                                return true;
                            }

                        }
                    }
                }
            
            foreach (CCSInventory inv in foundInventories)
            {
                foreach (CCSSlot s in inv.slots)
                {
                    Debug.Log("Checking for EmptySlots");
                    if (s.IsEmpty)
                    {
                        s.AddItem(item);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool TryPickup(CCSSlot slot, CCSInventoryType typeToEnter)
        {
            List<CCSInventory> foundInventories = new List<CCSInventory>();
            foreach(CCSInventory inv in inventories)
            {
                if(inv.InventoryType == typeToEnter)
                {
                    foundInventories.Add(inv);
                }
            }
            if (slot != null)
            {
                //Checking each slot that are possible to add to.
                foreach (CCSInventory inv in foundInventories)
                {
                    foreach (CCSSlot s in inv.slots)
                    {
                        if (!s.IsEmpty && !slot.IsEmpty && s.CheckItem.itemID == slot.CheckItem.itemID)
                        {

                            if (!s.IsFull)
                            {
                                s.AddItem(slot.CheckItem);
                                return true;
                            }

                        }
                    }
                }
            }
            foreach(CCSInventory inv in foundInventories)
            {
                foreach (CCSSlot s in inv.slots)
                {
                    Debug.Log("Checking for EmptySlots");
                    if (s.IsEmpty)
                    {
                        s.AddItem(slot.CheckItem);
                        return true;
                    }
                }
            }
            return false;
        }




        #endregion





        // Functions associated with the CCSMessenger and will be used if the messenger is available.
        #region Messenger

        #endregion
    }
}