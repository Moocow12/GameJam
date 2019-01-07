using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior", menuName = "Potion Behaviour/ExplosivePotion")]
public class ExplosivePotion : BreakBehaviour {

    public float blastRadius = 1f;

    public override void Break(Vector2 collisionPosition, GameObject collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionPosition, blastRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.GetComponent<Enemy>())
                col.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
