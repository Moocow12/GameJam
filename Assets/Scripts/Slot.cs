using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot  {

    public List<Item> items;

    public void AddItem(Item item)
    {
        if(items.Count > 0)
        {

        }
        else
        {
            items.Add(item);
        }
    }

    public bool IsEmpty()
    {
        if(items.Count ==0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
