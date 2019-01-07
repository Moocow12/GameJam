using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior", menuName = "Potion Behaviour/PoisonPotion")]
public class PoisonPotion : BreakBehaviour {

    public float poisonDuration;
    public float poisonTickRate;

    public override void Break(Vector2 collisionPosition, GameObject collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Poison(damage, poisonDuration, poisonTickRate);
        }
    }
}
