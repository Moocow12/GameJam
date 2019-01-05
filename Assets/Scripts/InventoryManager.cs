using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    InventoryBase[] inventories;
	// Use this for initialization
	void Start () {
        inventories = FindObjectsOfType<InventoryBase>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public bool AddItem(Item item)
    {
        Debug.Log("Adding Item, InventoryManager");
        //Goes through each of the inventories within the game
        foreach(InventoryBase inv in inventories)
        {
            //Checks to see if the item can go in that inventory
            if(inv.type == item.inventoryType)
            {
                //Checks to see if the addition was successful - not a full inventory / slot
                if (inv.AddItem(item))
                {
                    return true;
                }
            }
        }


        return false;
    }
}
