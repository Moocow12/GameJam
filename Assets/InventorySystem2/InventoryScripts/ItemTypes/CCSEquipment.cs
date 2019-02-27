using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCS;


public abstract class CCSEquipment : CCSUsable
{
    public CCSAttributes Stats;
    public override CCSItemType ItemType
    {
        get { return CCSItemType.CCSEquipment; }
    }

    public override string DisplayInfo()
    {
        return base.DisplayInfo();
    }

        
    public override void Use(CCSSlot slot)
    {
        //Check inventory type through the slot to allow for multiple functionality within the inventory.
        if(slot.CurrentInventory.InventoryType != CCSInventoryType.Equipment)
        {
            if(CCSInventoryManager.Instance.TryEquipItem(slot)) //Check Level or Gear Requirement
            {
                Debug.Log("Item Equipped");
                //UpdateCharacterInfo();
            }
        }
        else
        {
            //Cast Spell or use ability on target / self.
            Debug.Log("Item Used");
        }


    }

    /**
     * 
     * public virtual void UpdateCharacterInfo()
     * {
     *      Talk to your character manager and implement the necessary functionality to account for the stat information that was added in this class.
     * }
     * 
     **/


}

