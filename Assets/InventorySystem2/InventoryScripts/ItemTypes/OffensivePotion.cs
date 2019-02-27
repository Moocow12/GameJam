using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCS;

public class OffensivePotion : CCSUsable
{

    public int damage;
    public BreakBehaviour breakBehaviour;
    public string description;

    public ItemType itemType;
    public AudioClip breakAudioClip;

    public GameObject objectPrefabOnBreak;
    public float prefabDestructionTime;

    public GameObject PotionPrefab;

    public override CCSItemType ItemType
    {
        get
        {
            return CCSItemType.OffensivePotion;
        }
    }

    public override string DisplayInfo()
    {
        return base.DisplayInfo();
    }

    public override void Use(CCSSlot slot)
    {
        
        if (slot.CurrentInventory.InventoryType == CCSInventoryType.OffensivePotion)
        {

            Debug.Log("ChangingLauncher");
            Launcher.Instance.ChangeProjectile(PotionPrefab);
        }
        else
        {
            Pickup(slot);
        }
    }


    public void Pickup(CCSSlot slot)
    {
        Debug.Log("Pickuping up potions");
        //Call the specialized function that was added to the CCSInventoryManager
        if (CCSInventoryManager.Instance.TryPickup(slot, CCSInventoryType.OffensivePotion))
        {

            slot.RemoveItem();
            if(slot.CurrentInventory.InventoryType == CCSInventoryType.Crafting)
            {
                CraftingManager.Instance.CollectItem();
            }
        }
    }
}
