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


    private void OnMouseDown()
    {
        Debug.Log("Clicking DroppedItem");
        if(manager.AddItem(item))
        {
            //destroys the item if it is successfully added.
            MessageDisplay.Instance.DisplayMessage(item.name + " added to inventory.", 2f);
            Destroy(gameObject);
        }
        
    }
}
