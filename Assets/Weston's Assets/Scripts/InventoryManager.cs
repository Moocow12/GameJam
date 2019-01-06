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

    //public void SlotClick(List<Item> items,InventoryType type)
    //{
        
    //    if(firstItems.Count == 0 && items.Count != 0) //Makes sure that the first slot selected is not empty
    //    {
    //        firstType = type;
    //        firstItems = items;
    //        firstInitialized = true;
    //    }
    //    if (firstItems.Count != 0 && secondItems.Count == 0 && items != firstItems) //Selects the second slot if it is empty or not.
    //    {
    //        secondType = type;
    //        secondItems = items;
    //        secondInitialized = true;
    //    }
    //    if(firstInitialized && secondInitialized)
    //    {
    //        if(firstType == secondType)
    //        {
    //            SwitchItems();
    //        }
    //        firstInitialized = false;
    //        secondInitialized = false;
    //        firstItems = new List<Item>() ;
    //        secondItems = new List<Item>();
    //    }
       
    //}

    public Slot firstSlot, secondSlot;

    public void SlotClick(Slot s)
    {
        if(firstSlot == null)
        {
            firstSlot = s;
        }
        else if(secondSlot == null)
        {
            secondSlot = s;

            if(firstSlot.gameObject.name != "FinishedItem")
            {
                if (firstSlot.SlotType() == secondSlot.SlotType())
                {
                    SwitchSlots();
                }
                //Adds Crafting Materials into the crafter
                if (firstSlot.SlotType() == InventoryType.CraftingMaterials && secondSlot.SlotType() == InventoryType.Crafter)
                {
                    //Makes it equal to the same list
                    secondSlot.items = firstSlot.items;
                }
                //Exchanges crafting materials with the new material 
                if (firstSlot.SlotType() == InventoryType.Crafter && secondSlot.SlotType() == InventoryType.CraftingMaterials)
                {
                    if(firstSlot != secondSlot)
                    {
                        firstSlot.items = secondSlot.items;
                    }
                }
                firstSlot = null;
                secondSlot = null;
            }
            
        }
        

        
    }


    public void SwitchSlots()
    {
        List<Item> temp;
        temp = firstSlot.items;
        firstSlot.items = secondSlot.items;
        secondSlot.items = temp;
    }



    //public void SwitchItems()
    //{
    //    List<Item> temp = firstItems;
    //    firstItems = secondItems;
    //    secondItems = temp;
    //    if(Application.isEditor)
    //    {
    //        Debug.Log("Switching Items");
    //    }
        
    //}








	// Update is called once per frame
	void Update () {
		
	}


    public bool AddItem(Item item)
    {
        
        //Goes through each of the inventories within the game
        foreach(Inventory inv in inventories)
        {
            //Checks to see if the item can go in that inventory
            if(inv.type == item.inventoryType)
            {
                //Checks to see if the addition was successful - not a full inventory / slot
                if (inv.AddItem(item))
                {
                   MessageDisplay.Instance.EditorMessage(item.name + " added to inventory.", 2f);
                    
                    return true;
                }
                MessageDisplay.Instance.DisplayMessage("Inventory is Full", 2f);
                return false;
            }
        }

        MessageDisplay.Instance.EditorMessage(item.inventoryType.ToString() +" - No Such Inventory.", 2f);
        return false;
    }
}
