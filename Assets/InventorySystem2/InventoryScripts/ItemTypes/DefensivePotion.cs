using System.Collections;
using System.Collections.Generic;
using CCS;
using UnityEngine;

public class DefensivePotion : CCSUsable
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
            return CCSItemType.DefensivePotion;
        }
    }

    public override string DisplayInfo()
    {
        return base.DisplayInfo();
    }

    public override void Use(CCSSlot slot)
    {
        Debug.Log("Using Potion");
        if (slot.CurrentInventory.InventoryType == CCSInventoryType.DefensivePotion)
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
        if (CCSInventoryManager.Instance.TryPickup(slot, CCSInventoryType.DefensivePotion))
        {

            slot.RemoveItem();
            if (slot.CurrentInventory.InventoryType == CCSInventoryType.Crafting)
            {
                CraftingManager.Instance.CollectItem();
            }
        }
    }

}
