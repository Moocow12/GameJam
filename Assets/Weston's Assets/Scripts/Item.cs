using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item",menuName ="Item/Base")]
public class Item : ScriptableObject {

    public int stackSize = 1;
    public Sprite inventoryIcon;
    public Sprite combatImage;

    public InventoryType inventoryType;

    public virtual bool Use()
    {
       
        return true;
    }
}
