using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior", menuName = "Potion Behaviour/VoidPotion")]
public class VoidPotion : BreakBehaviour
{
    public float vacuumRadius;
    public float force;

    public override void Break(Vector2 collisionPosition, GameObject collision)
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionPosition, vacuumRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.GetComponent<Enemy>())
            {
                Vector2 vacuumForce = (Vector2)col.gameObject.transform.position - collisionPosition;
                col.GetComponent<Rigidbody2D>().AddForce(vacuumForce * force * col.GetComponent<Rigidbody2D>().drag);
            }
        }
    }
}
