using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/// <summary>
/// Transfers items from one inventory to another.
/// </summary>
public class InventoryManager : MonoBehaviour {
    Inventory[] inventories;
	// Use this for initialization
	void Start () {
        inventories = FindObjectsOfType<Inventory>();
	}

    public List<Item> firstItems;
    public List<Item> secondItems;
    public InventoryType firstType;
    public InventoryType secondType;
    bool firstInitialized, secondInitialized;


    public Slot firstSlot, secondSlot;



    public void SlotClick(Slot s)
    {
        //Sets the first slot to the designated spot.
        if(firstSlot == null)
        {
            firstSlot = s;

            //If an empty slot is choosen reset the slot to null
            if(firstSlot.IsEmpty())
            {
                firstSlot = null;
            }
           
        }
        else if(secondSlot == null)
        {
            secondSlot = s;

            if(firstSlot.gameObject.name != "FinishedItem" && secondSlot.gameObject.name != "FinishedItem")
            {
                if (firstSlot.SlotType() == secondSlot.SlotType())
                {
                    SwitchSlots();
                }
                //Adds Crafting Materials into the crafter
                else if (secondSlot.SlotType() == InventoryType.Crafter)
                {
                    //Makes it equal to the same list
                    secondSlot.items = firstSlot.items;
                }
                //Exchanges crafting materials with the new material 
                else if (firstSlot.SlotType() == InventoryType.Crafter)
                {
                    if(firstSlot != secondSlot)
                    {
                        firstSlot.items = secondSlot.items;
                    }
                }else if(secondSlot.SlotType() == InventoryType.CraftingMaterials)
                {
                    if(secondSlot.GetItemType() == firstSlot.GetItemType())
                    {
                        SwitchSlots();
                    }
                    else
                    {
                        if(secondSlot.IsEmpty())
                        {
                            SwitchSlots();
                        }
                        else
                        {
                            MessageDisplay.Instance.DisplayMessage("Can not switch items, Please use an empty Slot", 2f);
                        }
                        
                    }
                }
                    
                //Adding to the offensive Potion Inventory
                else if(!firstSlot.IsEmpty() && (firstSlot.SlotType() == InventoryType.Crafter || firstSlot.SlotType() == InventoryType.CraftingMaterials) && secondSlot.SlotType() == InventoryType.OffensivePotion)
                {
                    if(firstSlot.GetItemType() == ItemType.Offensive)
                    {
                        if(!secondSlot.IsEmpty())
                        {
                            if(firstSlot.items[0].name == secondSlot.items[0].name)
                            {
                                if(!secondSlot.IsFull())
                                {
                                    secondSlot.AddItem(firstSlot.items[0]);
                                    firstSlot.RemoveItem();
                                }
                                else
                                {
                                    MessageDisplay.Instance.DisplayMessage("Cannot stack Items.", 2f);
                                }
                            }
                            else
                            {
                                MessageDisplay.Instance.DisplayMessage("Cannot combine items.", 2f);
                            }
                        }
                        else
                        {
                            SwitchSlots();
                        }
                    }
                }

                firstSlot = null;
                secondSlot = null;
            }
            //Moving the crafted item from the finished slot to the empty inventory slot
            //Can go in any inventory slot because it is a potion
            else if(secondSlot.gameObject.name != "FinishedItem" && secondSlot.SlotType() != InventoryType.Crafter && !firstSlot.IsEmpty()) 
            {
                FindObjectOfType<CraftingManager>().TakingFinishedItem();
                SwitchSlots();
            }
            else
            {
                firstSlot = null;
                secondSlot = null;
            }
            
        }
        

        
    }


    public void SwitchSlots()
    {
        if (!firstSlot.IsEmpty() && !secondSlot.IsEmpty())
        {
            if(firstSlot.items[0].name == secondSlot.items[0].name)
            {
                TryStacking();
            }
        }
        else
        {
            List<Item> temp;
            temp = firstSlot.items;
            firstSlot.items = secondSlot.items;
            secondSlot.items = temp;
        }
        
        firstSlot = null;
        secondSlot = null;
    }

    public void TryStacking()
    {
        int startingValue = firstSlot.items.Count;
        for (int x = 0; x < startingValue; x++)
        {
            Item i = firstSlot.items[0];
            //Checks to see if the destination slot is full
            if (!secondSlot.IsFull())
            {
                secondSlot.AddItem(firstSlot.items[0]);
                firstSlot.RemoveItem();
            }
            else
            {
                return;
            }
           
        }
       
    }

    
	// Update is called once per frame
	void Update () {
		
	}


    public bool AddItem(Item item, InventoryType preferedInventory)
    {
       //Checks the preferedInventory first to see if there is room when picking up the item
        foreach(Inventory inv in inventories)
        {
            if (inv.type == preferedInventory)
            {
                if (inv.AddItem(item))
                {
                    
                    return true;
                }
            }
        }
        //Goes through each of the inventories within the game
        foreach(Inventory inv in inventories)
        {
            if(inv.type == InventoryType.CraftingMaterials)
            {
                if (inv.AddItem(item))
                {

                    return true;
                }
            }
        }
        return false;
    }




}
