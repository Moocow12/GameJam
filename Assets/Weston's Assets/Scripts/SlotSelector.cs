using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;






public class SlotSelector : MonoBehaviour {

    public Inventory selectedInventory;

    public Slot slotToUse;
    public Color SelectionIndicatorColor;

 

    private void Start()
    {
        if(selectedInventory != null)
        {
            slotToUse = selectedInventory.slots[0];
            ChangeColor();
        }
    }

    private void Update()
    {
        for(int i = 0;i<selectedInventory.slots.Length;i++)
        {
            //Checking for the alpha values to change the selected Slot
            if(Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha1 + i)))
            {
                ChangeSelection(i);
            }
        }
    }

    public void ChangeSelection(int index)
    {
        slotToUse = selectedInventory.slots[index];
        ResetAllSlotColor();
        ChangeColor();
    }


    public void ChangeColor()
    {
        if(slotToUse != null)
        {
            slotToUse.GetComponent<Image>().color = SelectionIndicatorColor;
        }
        
    }

    public void ResetAllSlotColor()
    {
        foreach(Slot s in selectedInventory.slots)
        {
            s.GetComponent<Image>().color = Color.white;
        }
    }

    public Item GetThrowingItem()
    {
        if(!slotToUse.IsEmpty())
        {
            Item item = slotToUse.items[0];
            slotToUse.RemoveItem();
            return item;

        }
        return null;
    }
}
