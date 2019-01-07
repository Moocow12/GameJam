using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Offensive,
    Defensive,
    Crafting

}



[CreateAssetMenu(fileName = "New Item",menuName ="Item/Base")]
public class Item : ScriptableObject {

    public int damage;
    public BreakBehaviour breakBehaviour;
    public string description;
    public int stackSize = 1;
    public Sprite inventoryIcon;

    public ItemType itemType;
    public AudioClip breakAudioClip;
    public virtual bool Use()
    {
        return false;
    }
}
