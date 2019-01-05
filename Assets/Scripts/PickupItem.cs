using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Comunicates with the inventoryManager to decide what inventory system will be used to hold the item.
/// </summary>
public class PickupItem : MonoBehaviour {

    InventoryManager manager;
    public Item item;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<InventoryManager>();
	}


    /// <summary>
    /// Tries to pick up the item within the inventory system.
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log("Clicking DroppedItem");
        if(manager.AddItem(item))
        {
            //destroys the item if it is successfully added.
            Destroy(gameObject);
        }
        
    }
}
