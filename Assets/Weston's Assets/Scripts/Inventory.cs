using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Add Addition types for each inventory that you want in the game.
/// </summary>
public enum InventoryType
{
    None,
    CraftingMaterials,
    Crafter,
    DefensivePotion,
    OffensivePotion

}


public class Inventory : MonoBehaviour {

    //LocalVariables
    RectTransform _rect;



    public Slot[] slots;
    public InventoryType type;
    [Header("Use Only When Type 'Crafter'")]
    public Slot finishedItemSlot;
    public ItemType[] acceptedItemTypes;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool AddItem(Item item)
    {
        foreach(Slot s in slots)
        {
            if(!s.IsFull() && s.CurrentItemName() == item.name)
            {
                s.AddItem(item);
                return true;
            }
            else if(s.IsEmpty())
            {
                s.AddItem(item);
                return true;
            }
                     
        }
        return false;
    }

    public bool IsCorrectItemType(ItemType type)
    {
        foreach(ItemType t in acceptedItemTypes)
        {
            if(type == t)
            {
                return true;
            }
        }
        return false;
    }
}
