using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot {

    public List<Item> items;
    


    /// <summary>
    /// Adds the desired to the inventory, Check to see if the inventory is full before using this method or you could lose the item.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if(items.Count > 0)
        {
            if(items[0].stackSize > items.Count)
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }
    }

    /// <summary>
    /// Removes an item from the inventory if possible
    /// </summary>
    public void RemoveItem()
    {
        if(!IsEmpty())
        {
            items.RemoveAt(0);
        }
    }

    /// <summary>
    /// Checks to see if the slot is currently Empty
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        if(items.Count ==0)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Determines if the slot is currently at the stack capacity 
    /// </summary>
    /// <returns></returns>
    public bool IsFull()
    {
        if (IsEmpty())
        {
            return false;
        }
        else if (items[0].stackSize > items.Count)
        {
            return false;
        }
        
        return true;
    }

    public int CurrentCount()
    {
        return items.Count;
    }

}
