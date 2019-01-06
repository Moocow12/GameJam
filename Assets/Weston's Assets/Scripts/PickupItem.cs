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
        switch (item.itemType)
        {
            case ItemType.Offensive:
                if (manager.AddItem(item,InventoryType.OffensivePotion))
                {
                    //destroys the item if it is successfully added.
                    Destroy(gameObject);
                }
                return;
            case ItemType.Defensive:
                if (manager.AddItem(item,InventoryType.DefensivePotion))
                {
                    //destroys the item if it is successfully added.
                    Destroy(gameObject);
                }
                return;
        }
        if (manager.AddItem(item, InventoryType.CraftingMaterials))
        {
            //destroys the item if it is successfully added.
            Destroy(gameObject);
        }

    }
}
