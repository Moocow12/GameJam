using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior", menuName = "Potion Behaviour/RepairPotion")]
public class RepairPotion : BreakBehaviour
{
    public override void Break(Vector2 collisionPosition, GameObject collision)
    {
        FindObjectOfType<Structure>().AddBlock();
    }
}
