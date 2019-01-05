using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        
        //Goes through each of the inventories within the game
        foreach(InventoryBase inv in inventories)
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
