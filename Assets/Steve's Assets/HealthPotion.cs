using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior", menuName = "Potion Behaviour/HealthPotion")]
public class HealthPotion : BreakBehaviour
{
    public override void Break(Vector2 collisionPosition, GameObject collision)
    {
        FindObjectOfType<Player>().Heal();
    }
}
