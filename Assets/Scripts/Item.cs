using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item",menuName ="Item/Base")]
public class Item : ScriptableObject {

    public int stackSize = 1;
    public Sprite inventoryIcon;
    public Sprite combatImage;

    

}
