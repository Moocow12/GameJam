using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {

    Inventory _inv;
    public List<Recipe> recipes;
    public bool found1 = false, found2 = false, found3= false;
	// Use this for initialization
	void Start () {
        Inventory [] inventories=  FindObjectsOfType<Inventory>();
        foreach(Inventory i in inventories)
        {
            if(i.type == InventoryType.Crafter)
            {
                _inv = i;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckForRecipe();
	}

    /// <summary>
    /// Looks through all of the aquired recipes and determines if the items within it are able to produce something new.
    /// </summary>
    public void CheckForRecipe()
    {
        if(_inv.enabled)
        {
            foreach (Recipe recipe in recipes)
            {
                found1 = false;
                found2 = false;
                found3 = false;
                foreach (Slot s in _inv.slots)
                {
                    if (s.IsEmpty())
                    {
                        s.items = new List<Item>();
                    }
                    if (!found1 && recipe.ingredient1 == null && s.IsEmpty())
                    {
                        found1 = true;
                    }
                    else if (recipe.ingredient1 != null && !found1 && !s.IsEmpty() && recipe.ingredient1.name == s.items[0].name)
                    {
                        found1 = true;
                    }
                    else if (!found2 && recipe.ingredient2 == null && s.IsEmpty())
                    {
                        found2 = true;
                    }
                    else if (recipe.ingredient2 != null && !found2 && !s.IsEmpty() && recipe.ingredient2.name == s.items[0].name)
                    {
                        found2 = true;
                    }

                    else if (!found3 && recipe.ingredient3 == null && s.IsEmpty())
                    {
                        found3 = true;
                    }
                    else if (recipe.ingredient3 != null && !found3 && !s.IsEmpty() && recipe.ingredient3.name == s.items[0].name)
                    {
                        found3 = true;
                    }
                }
                if (found1 && found2 && found3)
                {
                    UpdateFinishedItem(recipe.producedItem);
                    return;
                }
                else
                {
                    ClearFinishedSlot();
                }

            }
        }
        
        
    }

    /// <summary>
    /// Changes the output sprite to the recipes output.
    /// </summary>
    /// <param name="item"></param>
    public void UpdateFinishedItem(Item item)
    {
        _inv.finishedItemSlot.ClearSlot();//makes sure the slot is empty
        _inv.finishedItemSlot.AddItem(item);//puts what the item to be produced in the slot.
    }

    public void ClearFinishedSlot()
    {
        _inv.finishedItemSlot.ClearSlot();
    }


    public void TakingFinishedItem()
    {
        foreach(Slot s in _inv.slots)
        {
            if(!s.IsEmpty())
            {
                s.RemoveItem();
            }
        }
    }
}
