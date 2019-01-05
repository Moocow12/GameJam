using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add Addition types for each inventory that you want in the game.
/// </summary>
public enum InventoryType
{
    CraftingMaterials,
    Potions

}




public class InventoryBase : MonoBehaviour {

    //LocalVariables
    RectTransform _rect;



    private Slot[] slots;
    public int numberOfSlots;
    public InventoryType type;

    public GameObject slotPrefab;



	// Use this for initialization
	void Start () {
        _rect = GetComponent<RectTransform>();
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Camera.main.pixelWidth);
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Camera.main.pixelHeight);
        slots = new Slot[numberOfSlots];
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {

            //Adds the Prefab item to the 
            GameObject obj = Instantiate(slotPrefab, transform);
            slots[i] = obj.GetComponent<Slot>();
        }

    }

    public bool AddItem(Item item)
    {
        foreach(Slot s in slots)
        {
            if(!s.IsFull() && s.CurrentItemName() == item.name)
            {
                s.AddItem(item);
                return true;
                
                
            }
            else if(s.IsEmpty())
            {
                s.AddItem(item);
                return true;
            }
                     
        }
        return false;
    }
}
