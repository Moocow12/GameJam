using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : Slot {


    public new void AddItem(Item item)
    {
        if(IsEmpty())
        {
            items.Add(item);
        }
        MessageDisplay.Instance.DisplayMessage("Slot is Full", 2f);
    }



}
