using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInventory : InventoryBase {

    public Slot finishedItemSlot;
    /// <summary>
    /// 
    /// </summary>
	public override void Initialize()
    {
        Debug.Log("Child class initializing");
    }
}
