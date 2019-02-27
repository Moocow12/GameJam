using CCS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {

    public static CraftingManager Instance;

    public void Singleton()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    
    }


    public CCSSlot finishedSlot;
    public CCSMirrorSlot[] craftingSlots;
    public List<Recipe> recipes;
    public bool found1 = false, found2 = false, found3= false;

	// Use this for initialization
	void Awake () {
        Singleton();
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
        if(finishedSlot.gameObject.activeInHierarchy)
        {
            foreach (Recipe recipe in recipes)
            {
                found1 = false;
                found2 = false;
                found3 = false;
                foreach (CCSMirrorSlot s in craftingSlots)
                {
                   
                    if (!found1 && recipe.ingredient1 == null && s.currentItem == null)
                    {
                        found1 = true;
                    }
                    else if (recipe.ingredient1 != null && !found1 && s.currentItem != null && recipe.ingredient1.name == s.currentItem.name)
                    {
                        found1 = true;
                    }
                    else if (!found2 && recipe.ingredient2 == null && s.currentItem == null)
                    {
                        found2 = true;
                    }
                    else if (recipe.ingredient2 != null && !found2 && s.currentItem != null && recipe.ingredient2.name == s.currentItem.name)
                    {
                        found2 = true;
                    }

                    else if (!found3 && recipe.ingredient3 == null && s.currentItem == null)
                    {
                        found3 = true;
                    }
                    else if (recipe.ingredient3 != null && !found3 && s.currentItem != null && recipe.ingredient3.name == s.currentItem.name)
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
    public void UpdateFinishedItem(CCSBaseItem item)
    {
        finishedSlot.ClearSlot();//makes sure the slot is empty
        finishedSlot.AddItem(item);//puts what the item to be produced in the slot.
    }

    public void ClearFinishedSlot()
    {
        finishedSlot.ClearSlot();
    }

    public void CollectItem()
    {
        foreach(CCSMirrorSlot slot in craftingSlots)
        {
            if(slot.currentItem != null)
            {
                slot.RemoveItem(); 
            }
        }
    }
}
