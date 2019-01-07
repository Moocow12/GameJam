using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior", menuName = "Potion Behaviour/FreezePotion")]
public class FreezePotion : BreakBehaviour
{
    public float freezeRadius = 1f;
    public float duration;

    public override void Break(Vector2 collisionPosition, GameObject collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionPosition, freezeRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.GetComponent<Enemy>())
                col.GetComponent<Enemy>().Freeze(duration);
        }
    }
}
