using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Potion",menuName ="Item/Potion")]
public class Potion : Item {

    [Header("Need a PotionAbility class that keeps what affect the potion has when it breaks.")]
    public int note;
    
}
